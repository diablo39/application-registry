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
    public class ApplicationCreateCommandTests : IClassFixture<DbContextFactory>
    {
        private readonly DbContextFactory _dbContextFactory;

        public ApplicationCreateCommandTests(DbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        [Fact]
        public async Task add_new_application_should_work_minimal_set()
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

            var handler = new ApplicationCreateCommandHandler(dbcontext, guidGenerator);

            //act

            var handlerResult = await handler.ExecuteAsync(applicationCommand);
            var application = dbcontext.Applications.FirstOrDefault(e=> e.Code == applicationCommand.Code);

            //assert
            handlerResult.Should().BeAssignableTo(typeof(ISuccessResult<>));
            application.Endpoints.Should().NotBeEmpty();
        }
    }
}
