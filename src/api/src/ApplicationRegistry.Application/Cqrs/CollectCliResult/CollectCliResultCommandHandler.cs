using ApplicationRegistry.Application.AsyncJobs;
using ApplicationRegistry.Application.Commands;
using ApplicationRegistry.Database;
using ApplicationRegistry.Database.Entities;
using ApplicationRegistry.Domain.Entities.Applications;
using ApplicationRegistry.Infrastructure.Abstractions.Scheduler;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationRegistry.Application.CommandHandlers
{
    public class CommandHandlerResult
    {
        public bool IsSuccess { get; set; }

        public static CommandHandlerResult Success()
        {
            return new CommandHandlerResult { IsSuccess = true };
        }

        public static CommandHandlerResult Fail()
        {
            return new CommandHandlerResult { IsSuccess = false };
        }
    }

    public class CollectCliResultCommandHandler
    {
        private readonly IScheduler _scheduler;
        private IUnitOfWork _context;

        public CollectCliResultCommandHandler(IUnitOfWork context, IScheduler scheduler)
        {
            _context = context;
            _scheduler = scheduler;
        }

        public CommandHandlerResult Handle(CollectCliResultCommand command)
        {
            CommandHandlerResult result;

            var application = _context.Applications.FirstOrDefault(a => a.Code == command.ApplicationCode);

            if (application == null)
            {
                application = CreateNewApplication(command);
            }

            application.RepositoryUrl = command.RepositoryUrl;

            application.Framework = !string.IsNullOrWhiteSpace(command.Framework) ? command.Framework : null;

            var entity = GetVersionFromDatabase(application.Id, command.IdEnvironment, command.Version);

            if (entity == null)
            {
                result = InsertVersion(command, application);
            }
            else
            {
                return CommandHandlerResult.Fail(); // no version update allowed
            }

            _context.FixApplicationVersion(application.Id);

            return CommandHandlerResult.Success();
        }


        void ProcessNugetPackage(ApplicationVersionEntity entity, CollectCliResultCommand.ApplicationVersionDependency dependency)
        {
            var nuget = _context.NugetPackageRepository.EnsureExists(dependency.Name, dependency.Version);

            var nugetDependency = new ApplicationVersionNugetPackageDependency { ApplicationVersion = entity, NugetPackage = nuget };

            entity.ApplicationVersionNugetPackageDependencies.Add(nugetDependency);

            _context.Add(nugetDependency);
        }
        internal CommandHandlerResult InsertVersion(CollectCliResultCommand command, Database.Entities.ApplicationEntity application)
        {
            var swaggerSpecifications = new List<Guid>();

            var entity = new ApplicationVersionEntity
            {
                IdApplication = application.Id,
                IdCommit = command.IdCommit,
                IdEnvironment = command.IdEnvironment,
                IsArchived = false,
                Version = command.Version,
                Dependencies = new List<ApplicationVersionDependencyEntity>(),
                SwaggerSpecifications = new List<ApplicationVersionSwaggerSpecificationEntity>(),
                ToolsVersion = command.ToolsVersion,
                CollectorBatchStatuses = JsonConvert.SerializeObject(command.BatchStatuses, Formatting.Indented),
                CollectorExecutionDuration = command.ExecutionDuration,
                CollectorExecutionSucceeded = command.ExecutionSucceeded,
                FrameworkVersion = command.FrameworkVersion,
            };

            _context.ApplicationVersions.Add(entity);

            if (command.Specifications != null)
            {
                for (int i = 0; i < command.Specifications.Count; i++)
                {
                    var specification = command.Specifications[i];
                    if (specification.SpecificationType == SpecificationTypes.Swagger)
                    {
                        var id = Guid.NewGuid();
                        var specyfication = new ApplicationVersionSwaggerSpecificationEntity
                        {
                            Id = id,
                            ContentType = specification.ContentType,
                            Specification = specification.Specification,
                            SpecificationTextHash = specification.Specification.CalculateSHA256(),
                            Code = specification.Code
                        };

                        entity.SwaggerSpecifications.Add(specyfication);

                        swaggerSpecifications.Add(id);

                        _context.ApplicationVersionSwaggerSpecifications.Add(specyfication);
                    }
                }
            }

            if (command.Dependencies != null)
            {
                foreach (var dependency in command.Dependencies)
                {
                    if (dependency.DependencyType == DependencyTypes.Nuget)
                    {
                        ProcessNugetPackage(entity, dependency);
                        continue;
                    }
                }
            }

            _context.SaveChanges();

            StartProcessSwaggerSpecificationChangedJob(swaggerSpecifications);

            return CommandHandlerResult.Success();
        }

        private Database.Entities.ApplicationEntity CreateNewApplication(CollectCliResultCommand command)
        {
            var application = new ApplicationEntity(id: Guid.NewGuid(), code: command.ApplicationCode, name: command.ApplicationCode)
            {
                RepositoryUrl = command.RepositoryUrl
            };

            _context.Applications.Add(application);

            _context.Dependencies.Add(new ApplicationDependencyEntity(application.Name, application.Id));

            return application;
        }

        private ApplicationVersionEntity GetVersionFromDatabase(Guid idApplication, string idEnvironment, string version)
        {
            var entity = _context.ApplicationVersions
                .Include(e => e.Dependencies)
                .Include(e => e.SwaggerSpecifications)
                .FirstOrDefault(v => v.IdApplication == idApplication && v.IdEnvironment == idEnvironment && v.Version == version);

            return entity;
        }

        private void StartProcessSwaggerSpecificationChangedJob(List<Guid> swaggerSpecifications)
        {
            for (int i = 0; i < swaggerSpecifications.Count; i++)
            {
                var swagger = swaggerSpecifications[i];

                _scheduler.Enqueue<ProcessSpecifiacationTextChangedJob>(e => e.Handle(swagger));
            }
        }
    }
}
