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

            var applicationCommand = new ApplicationCreateCommand
            {
                Code = Guid.NewGuid().ToString(),
                Id = Guid.NewGuid(),
                Name = Guid.NewGuid().ToString()
            };

            var handler = new ApplicationCreateCommandHandler(dbcontext);

            //act

            var handlerResult = await handler.ExecuteAsync(applicationCommand);

            //assert
            handlerResult.Should().BeAssignableTo(typeof(ISuccessResult<>));
        }


    }
}
