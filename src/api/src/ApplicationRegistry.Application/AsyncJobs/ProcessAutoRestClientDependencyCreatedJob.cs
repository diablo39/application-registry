using ApplicationRegistry.Application.Services;
using ApplicationRegistry.Database;
using ApplicationRegistry.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationRegistry.Application.AsyncJobs
{
    public class ProcessAutoRestClientDependencyCreatedJob
    {
        private readonly IUnitOfWork _context;
        private readonly ILogger<ProcessAutoRestClientDependencyCreatedJob> _logger;

        public ProcessAutoRestClientDependencyCreatedJob(
            IUnitOfWork context,
            ILogger<ProcessAutoRestClientDependencyCreatedJob> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Handle(Guid id)
        {
            var applicationDependency = _context.ApplicationVersionDependencies
                                            .Include(e => e.ApplicationVersion).ThenInclude(e => e.Environment)
                                            .Include(e => e.Dependency)
                                            .FirstOrDefault(e => e.Id == id);

            if (applicationDependency == null)
            {
                _logger.LogWarning("ApplicationVersionDependecy: {0} was not found in database. Processing aborted", id);
                return;
            }
            var applicationVesion = applicationDependency.ApplicationVersion;
            var application = _context.Applications.Find(applicationDependency.ApplicationVersion.IdApplication);

            // 1. Check in knowledge base
            var knowledgebaseresult = _context.CollectorKnowledgeBase.Where(e => applicationDependency.Dependency.Name == e.ClientType && applicationDependency.ApplicationVersion.IdApplication == e.IdApplicationClient).ToList();
            if (knowledgebaseresult.Count > 0)
            {
                // we have rows in knowledge base - one server can be used by multiple clients
                for (int i = 0; i < knowledgebaseresult.Count; i++)
                {
                    var knowledgebaseItem = knowledgebaseresult[i];
                    var childIdDependency = ApplicationDependencyEntity.GetApplicationDependencyId(knowledgebaseItem.IdApplicationServer);

                    if (_context.ApplicationVersionDependencies.Any(e => e.IdDependency == childIdDependency && e.IdParent == id && e.IdApplicationVersion == applicationVesion.Id))
                        continue;

                    //_taskService.DoneTaskByIdApplicationVersionDependency(id);

                    applicationDependency.ApplicationVersion.Dependencies.Add(new ApplicationVersionDependencyEntity
                    {
                        IdDependency = childIdDependency,
                        IdParent = id
                    });
                }
                _context.SaveChanges();
                return;
            }

            // 2.a Check with client name
            {
                var clientClass = applicationDependency.Dependency.Name.Split('.').LastOrDefault()?.ToLower();

                var appsMatchingClassName = _context.Applications.Where(a => a.Name.ToLower().Replace(".", "") == clientClass).Take(2).ToList();

                if (appsMatchingClassName.Count == 1)
                {
                    var appMatchingClassName = appsMatchingClassName[0];

                    var childIdDependency = ApplicationDependencyEntity.GetApplicationDependencyId(appMatchingClassName.Id);
                    if (!_context.ApplicationVersionDependencies.Any(e => e.IdDependency == childIdDependency && e.IdParent == id && e.IdApplicationVersion == applicationVesion.Id))
                    {
                        applicationDependency.ApplicationVersion.Dependencies.Add(new ApplicationVersionDependencyEntity
                        {
                            IdDependency = childIdDependency,
                            IdParent = id
                        });

                        _context.SaveChanges();
                        return;
                    }
                }
            }

            // 2.b Check operations in swagger specifications [global] - operationId
            var extraProperties = JsonConvert.DeserializeObject<Dictionary<string, JToken>>(applicationDependency.ExtraProperties);

            var operations = extraProperties["Operations"].ToObject<List<Operation>>();

            var operationIdsStringified = string.Concat("|", string.Join("|", operations.Select(e => e.OperationId).OrderBy(e => e)), "|");

            var query = _context.ApplicationVersionSpecifications
                .OfType<SwaggerApplicationVersionSpecificationEntity>()
                .Where(e => e.OperationsStringified == operationIdsStringified);

            var queryResult = query.Include(e => e.ApplicationVersion).ToList();

            var applicationCounts = query.Select(e => e.ApplicationVersion.IdApplication).Distinct().Count();

            switch (applicationCounts)
            {
                case 0:
                    //SuitableApplicationNotFound(applicationDependency, application);
                    break;
                case 1:
                    SuitableApplicationFound(id, applicationDependency, application, queryResult);
                    break;
                default:
                    //SuitableApplicationMoreThanOne(applicationDependency, application, queryResult);
                    break;
            }

            _context.SaveChanges();
            Console.WriteLine("Haha");
        }

        private void SuitableApplicationFound(Guid id, ApplicationVersionDependencyEntity applicationDependency, Database.Entities.ApplicationEntity application, List<SwaggerApplicationVersionSpecificationEntity> queryResult)
        {
            var childIdDependency = ApplicationDependencyEntity.GetApplicationDependencyId(queryResult[0].ApplicationVersion.IdApplication);

            //var tasks = _context.Tasks.OfType<MapAutorestClientTaskEntity>().Where(e => e.IdApplicationVersionDependency == id).ToList();
            //_context.Tasks.RemoveRange(tasks);

            if (_context.ApplicationVersionDependencies.Any(e => e.IdDependency == childIdDependency && e.IdParent == id && e.IdApplicationVersion == applicationDependency.IdApplicationVersion))
                return; // child dependency already exists

            var childApplicationDependency = new ApplicationVersionDependencyEntity
            {
                Id = Guid.NewGuid(),
                IdDependency = childIdDependency,
                IdParent = id
            };

            //_taskService.DoneTaskByIdApplicationVersionDependency(id);

            applicationDependency.ApplicationVersion.Dependencies.Add(childApplicationDependency);

            if (!_context.CollectorKnowledgeBase.Any(e => e.IdApplicationClient == application.Id
                    && e.IdApplicationServer == queryResult[0].ApplicationVersion.IdApplication
                    && e.ClientType == applicationDependency.Dependency.Name))
            {
                var newKnowledge = new CollectorKnowledgeBaseEntity(
                        applicationDependency.Dependency.Name,
                        application.Id,
                        queryResult[0].ApplicationVersion.IdApplication,
                        childApplicationDependency.Id,
                        id
                );
                _context.CollectorKnowledgeBase.Add(newKnowledge);
            }
        }

        //private void SuitableApplicationMoreThanOne(ApplicationVersionDependencyEntity applicationDependency, Database.Entities.ApplicationEntity application, List<SwaggerApplicationVersionSpecificationEntity> queryResult)
        //{
        //    var task = new MapAutorestClientTaskEntity(applicationDependency.ApplicationVersion.Environment.Id, application.Name, application.Id, applicationDependency.Dependency.Name, applicationDependency.Id, applicationDependency.IdDependency);

        //    _taskService.AddTask(task);

        //}

        //private void SuitableApplicationNotFound(ApplicationVersionDependencyEntity applicationDependency, Database.Entities.ApplicationEntity application)
        //{
        //    var task = new MapAutorestClientTaskEntity(applicationDependency.ApplicationVersion.Environment.Id, application.Name, application.Id, applicationDependency.Dependency.Name, applicationDependency.Id, applicationDependency.IdDependency);

        //    _taskService.AddTask(task);

        //}

        private class Operation
        {
            public bool IsInUse { get; set; }

            public string OperationId { get; set; }

            public string Path { get; set; }
        }
    }
}
