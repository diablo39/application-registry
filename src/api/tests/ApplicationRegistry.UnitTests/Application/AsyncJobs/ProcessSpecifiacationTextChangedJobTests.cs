using ApplicationRegistry.Application.AsyncJobs;
using ApplicationRegistry.Database.Entities;
using ApplicationRegistry.Infrastructure.Abstractions.Scheduler;
using ApplicationRegistry.UnitTests.Properties;
using ApplicationRegistry.UnitTests.TestInfrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ApplicationRegistry.UnitTests.Application.EventHandlers
{
    public class ProcessSpecifiacationTextChangedJobTests: IClassFixture<DbContextFactory>
    {
        private readonly DbContextFactory _factory;

        public ProcessSpecifiacationTextChangedJobTests(DbContextFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public void Proccess_Valid_Yaml()
        {
            // arrange
            var id = Guid.NewGuid();
            
            var dbContext = _factory.GetContext();
            var version = dbContext.ApplicationVersions.First();

            var specification = new SwaggerApplicationVersionSpecificationEntity
            {
                Id = id,
                IdApplicationVersion = version.Id,
                ContentType = "application/yaml",
                SpecificationText = new ApplicationVersionSpecificationTextEntity {Id = id, Specification = Resources.Swagger_Yaml_Correct },
                SpecificationType = SpecificationTypeEntity.Swagger,
                Code = "SWAGGER",
                SpecificationTextHash = "HASH"
            };

            dbContext.Add(specification);
            dbContext.SaveChanges();
            var loggerMoq = new Moq.Mock<ILogger<ProcessSpecifiacationTextChangedJob>>();
            var schedulerMoq = new Moq.Mock<IScheduler>();
            var eventHandler = new ProcessSpecifiacationTextChangedJob(dbContext, loggerMoq.Object, schedulerMoq.Object);

            // act
            eventHandler.Handle(id);
            eventHandler.Handle(id);

            // assert
            Console.WriteLine();
        }

        [Fact]
        public void Proccess_Invalid_Yaml()
        {
            // arrange
            var id = Guid.NewGuid();

            var dbContext = _factory.GetContext();
            var version = dbContext.ApplicationVersions.First();

            var specification = new SwaggerApplicationVersionSpecificationEntity
            {
                Id = id,
                IdApplicationVersion = version.Id,
                ContentType = "application/yaml",
                SpecificationText = new ApplicationVersionSpecificationTextEntity { Id = id, Specification = Resources.Swagger_Yaml_Invalid },
                SpecificationType = SpecificationTypeEntity.Swagger,
                SpecificationTextHash = "HASH",
                Code = "SWAGGER"
            };

            dbContext.Add(specification);
            dbContext.SaveChanges();

            var loggerMoq = new Moq.Mock<ILogger<ProcessSpecifiacationTextChangedJob>>();
            var schedulerMoq = new Moq.Mock<IScheduler>();

            var eventHandler = new ProcessSpecifiacationTextChangedJob(dbContext, loggerMoq.Object, schedulerMoq.Object);

            // act
            eventHandler.Handle(id);
            eventHandler.Handle(id);

            // assert
            Console.WriteLine();
        }
    }
}
