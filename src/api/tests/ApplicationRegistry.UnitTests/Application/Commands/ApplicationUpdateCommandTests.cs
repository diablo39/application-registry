using ApplicationRegistry.Application.Commands;
using ApplicationRegistry.UnitTests.TestInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using ApplicationRegistry.CQRS.Abstraction;
using ApplicationRegistry.Infrastructure.ApplicationSevices;

namespace ApplicationRegistry.UnitTests.Application.Commands
{
    public class ApplicationUpdateCommandTests : IClassFixture<DbContextFactory>
    {
        private readonly DbContextFactory _dbContextFactory;

        public ApplicationUpdateCommandTests(DbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        [Fact]
        public async Task update_application_should_work_minimal_set()
        {
            // arrange
            var dbcontext = _dbContextFactory.GetContext();
            var guidGenerator = new GuidGenerator();
            var applicationCommand = new ApplicationCreateCommand
            {
                Code = Guid.NewGuid().ToString(),
                Name = Guid.NewGuid().ToString(),
            };

            applicationCommand.Endpoints.Add(new ApplicationCommandBaseEndpoint
            {
                Comment = "test",
                Path = "/tesr",
                EnvironmentId = "INT"
            });

            var createHandler = new ApplicationCreateCommandHandler(dbcontext, guidGenerator);
            var createHandlerResult = await createHandler.ExecuteAsync(applicationCommand);
            var application = dbcontext.Applications.FirstOrDefault(e => e.Code == applicationCommand.Code);

            var applicationUpdateCommand = new ApplicationUpdateCommand
            {
                Id = application.Id,
                Description = "new desc",
                Code = applicationCommand.Code,
                Name = applicationCommand.Name,
                IdProject = application.IdSystem
            };

            applicationUpdateCommand.Endpoints.Add(
                new ApplicationUpdateCommandEndpoint
                {
                    Comment = "updated",
                    Path = "/update",
                    EnvironmentId = "INT"
                });

            //act
            var handler = new ApplicationUpdateCommandHandler(dbcontext, guidGenerator);
            var handlerResult = await handler.ExecuteAsync(applicationUpdateCommand);


            //assert
            handlerResult.Should().BeAssignableTo(typeof(ISuccessResult<>));
            application.Endpoints.Should().NotBeEmpty();
            application.Endpoints[0].Path.Should().Be("/update");
        }
    }
}
