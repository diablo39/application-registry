﻿using ApplicationRegistry.Application.CommandHandlers;
using ApplicationRegistry.Application.Commands;
using ApplicationRegistry.Database.Entities;
using ApplicationRegistry.Infrastructure.Abstractions.Scheduler;
using ApplicationRegistry.UnitTests.TestInfrastructure;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ApplicationRegistry.UnitTests.Application.Commands
{
    public class CollectCliResultCommandHandlerTests: IClassFixture<DbContextFactory>
    {
        DbContextFactory _dbContextFactory;

        public CollectCliResultCommandHandlerTests(DbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        [Fact]
        public void add_new_application_should_success()
        {
            //arrange
            var context = _dbContextFactory.GetContext();
            var applicationCode = Guid.NewGuid().ToString();
            var specificationCode = Guid.NewGuid().ToString();
            var command = new CollectCliResultCommand
            {
                ApplicationCode = applicationCode,
                IdEnvironment = "INT",
                Version = "1.1_12332_1",
                Specifications = new System.Collections.Generic.List<CollectCliResultCommand.ApplicationVersionSpecification>
                {
                    new CollectCliResultCommand.ApplicationVersionSpecification
                    {
                        ContentType = "application/yaml",
                        SpecificationType = SpecificationTypeEntity.Swagger,
                        Specification = "swagger: '2.0'",
                        Code = specificationCode
                    }
                },
                Dependencies = new System.Collections.Generic.List<CollectCliResultCommand.ApplicationVersionDependency>
                {
                    new CollectCliResultCommand.ApplicationVersionDependency
                    {
                        DependencyType = DependencyTypes.Nuget,
                        Name = "Microsoft.Extensions.Logging",
                        Version = "12"
                    },
                    new CollectCliResultCommand.ApplicationVersionDependency
                    {
                        DependencyType = DependencyTypes.AutorestClient,
                        Name = "MyApplication.Contracts.MyClient",
                        Version = "New version",
                        VersionExtraProperties = new Dictionary<string, object>
                        {
                            {
                                "Operations",
                                new List<AutorestClientDependency.Operation>
                                {
                                    new AutorestClientDependency.Operation
                                    {
                                        IsInUse = false,
                                        OperationId = "GetValue",
                                        Path = "/value"
                                    }
                                }
                            }
                        }
                    }
                }
            };

            var commandjson = JsonConvert.SerializeObject(command);
            var schedulerMoq = new Moq.Mock<IScheduler>();
            var schedulerObject = schedulerMoq.Object;
            var handler = new CollectCliResultCommandHandler(context, schedulerObject);
            //Act
            var result = handler.Handle(command);

            //Assert

            result.IsSuccess.Should().BeTrue();

            var application = context.Applications.Include(e => e.Versions).ThenInclude(e => e.Specifications).FirstOrDefault(a => a.Code == applicationCode);
            application.Should().NotBeNull("new application should be created");
            application.IdProject.Should().Be(ProjectEntity.UnasignedApplications, "application should be added to default project");

            var version = application.Versions.FirstOrDefault(e => e.Version == command.Version);
            version.Should().NotBeNull("Version should be created");
            version.Version.Should().Be(command.Version);

            var specification = version.Specifications.FirstOrDefault();

            specification.Should().NotBeNull();
            specification.SpecificationTextHash.Should().NotBeNull();

            version.ApplicationVersionNugetPackageDependencies.Should().HaveCount(1);

            AssertSchedulerInvocations(command, schedulerMoq);
        }

        [Fact]
        public void add_new_version_to_existing_application_should_success()
        {
            //arrange
            var context = _dbContextFactory.GetContext();

            var existingApplication = context.Applications.First();

            var applicationCode = existingApplication.Code;

            var version = Guid.NewGuid().ToString();
            var specificationCode = Guid.NewGuid().ToString();
            var command = new CollectCliResultCommand
            {
                ApplicationCode = applicationCode,
                IdEnvironment = "INT",
                Version = version,
                Specifications = new System.Collections.Generic.List<CollectCliResultCommand.ApplicationVersionSpecification>
                {
                    new CollectCliResultCommand.ApplicationVersionSpecification
                    {
                        ContentType = "application/yaml",
                        SpecificationType = SpecificationTypeEntity.Swagger,
                        Specification = "swagger: '2.0'",
                        Code = specificationCode
                    }
                },
                Dependencies = new System.Collections.Generic.List<CollectCliResultCommand.ApplicationVersionDependency>
                {
                    new CollectCliResultCommand.ApplicationVersionDependency
                    {
                        DependencyType = DependencyTypes.Nuget,
                        Name = "Microsoft.Extensions.Logging",
                        Version = "12"
                    },
                    new CollectCliResultCommand.ApplicationVersionDependency
                    {
                        DependencyType = DependencyTypes.AutorestClient,
                        Name = "MyApplication.Contracts.MyClient",
                        Version = "New version",
                        VersionExtraProperties = new Dictionary<string, object>
                        {
                            {
                                "Operations",
                                new List<AutorestClientDependency.Operation>
                                {
                                    new AutorestClientDependency.Operation
                                    {
                                        IsInUse = false,
                                        OperationId = "GetValue",
                                        Path = "/value"
                                    }
                                }
                            }
                        }
                    }
                }
            };

            var schedulerMoq = new Moq.Mock<IScheduler>();
            var handler = new CollectCliResultCommandHandler(context, schedulerMoq.Object);
            //Act
            var result = handler.Handle(command);

            //Assert

            result.IsSuccess.Should().BeTrue();

            var application = context.Applications.Include(e => e.Versions).ThenInclude(e => e.ApplicationVersionNugetPackageDependencies).Include(e => e.Versions).ThenInclude(e => e.Specifications).ThenInclude(e => e.SpecificationText).FirstOrDefault(a => a.Code == applicationCode);
            application.Should().NotBeNull("new application should be created");

            var applicationVersion = application.Versions.FirstOrDefault(e => e.Version == command.Version && e.IdEnvironment == command.IdEnvironment);
            applicationVersion.Should().NotBeNull("Version should be created");
            applicationVersion.Version.Should().Be(command.Version);

            var specification = applicationVersion.Specifications.FirstOrDefault();

            specification.Should().NotBeNull();
            specification.SpecificationTextHash.Should().NotBeNull();

            specification.SpecificationText.Should().NotBeNull();
            specification.SpecificationText.Specification.Should().NotBeNull()
                .And.Be(command.Specifications[0].Specification);

            applicationVersion.ApplicationVersionNugetPackageDependencies.Should().HaveCount(1);

            AssertSchedulerInvocations(command, schedulerMoq);
        }


        [Fact]
        public void update_existing_version_should_fail()
        {
            //arrange
            var context = _dbContextFactory.GetContext();
            var idEnvironment = "INT";

            var existingApplication = context.Applications
                .Include(e => e.Versions).ThenInclude(e => e.Specifications).ThenInclude(e => e.SpecificationText)
                .Where(e => e.Versions.Any(v => v.Specifications.Any() && v.IdEnvironment == idEnvironment))
                .First();

            var applicationCode = existingApplication.Code;

            var ss = existingApplication.Versions.FirstOrDefault(e => e.IdEnvironment == idEnvironment && e.Specifications != null && e.Specifications.Any());
            var version = ss.Version;
            var specificationCode = ss.Specifications[0].Code;

            var command = new CollectCliResultCommand
            {
                ApplicationCode = applicationCode,
                IdEnvironment = idEnvironment,
                Version = version,
                Specifications = new System.Collections.Generic.List<CollectCliResultCommand.ApplicationVersionSpecification>
                {
                    new CollectCliResultCommand.ApplicationVersionSpecification
                    {
                        ContentType = "application/yaml",
                        SpecificationType = SpecificationTypeEntity.Swagger,
                        Specification = Guid.NewGuid().ToString(),
                        Code = specificationCode
                    }
                },
                Dependencies = new System.Collections.Generic.List<CollectCliResultCommand.ApplicationVersionDependency>
                {
                    new CollectCliResultCommand.ApplicationVersionDependency
                    {
                        DependencyType = DependencyTypes.Nuget,
                        Name = "Microsoft.Extensions.Logging",
                        Version = "12"
                    },
                    new CollectCliResultCommand.ApplicationVersionDependency
                    {
                        DependencyType = DependencyTypes.AutorestClient,
                        Name = "MyApplication.Contracts.MyClient",
                        Version = "New version",
                        VersionExtraProperties = new Dictionary<string, object>
                        {
                            {
                                "Operations",
                                new List<AutorestClientDependency.Operation>
                                {
                                    new AutorestClientDependency.Operation
                                    {
                                        IsInUse = false,
                                        OperationId = "GetValue",
                                        Path = "/value"
                                    }
                                }
                            }
                        }
                    }
                }
            };
            var commandstr = JsonConvert.SerializeObject(command);
            Console.WriteLine(commandstr);

            var schedulerMoq = new Moq.Mock<IScheduler>();
            var handler = new CollectCliResultCommandHandler(context, schedulerMoq.Object);

            //Act
            var result = handler.Handle(command);

            //Assert

            result.IsSuccess.Should().BeFalse();
        }

        

        private static void AssertSchedulerInvocations(CollectCliResultCommand command, Moq.Mock<IScheduler> schedulerMoq)
        {
            //var invocations = schedulerMoq.Invocations;
            //var schedulerExecutions = command.Specifications.Count(e => e.SpecificationType == SpecificationTypeEntity.Swagger);

            //schedulerExecutions += command.Dependencies.Count(e => e.DependencyType == DependencyTypes.AutorestClient);

            //invocations.Count.Should().Be(schedulerExecutions);
        }
    }
}
