using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using ApplicationRegistry.UnitTests.TestInfrastructure;
using ApplicationRegistry.Application.Commands.Redis;
using ApplicationRegistry.CQRS.Abstraction;

namespace ApplicationRegistry.UnitTests.Application.Commands
{
    public class RedisCreateCommandTests: IClassFixture<DbContextFactory>
    {
        private readonly DbContextFactory _dbContextFactory;

        public RedisCreateCommandTests(DbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        [Fact]
        public async Task add_new_redis_should_success()
        {
            // arrange
            var dbContext = _dbContextFactory.GetContext();

            var command = new RedisCreateCommand();
            var handler = new RedisCreateCommandHandler(dbContext);

            // act

            var handlerResult = await handler.ExecuteAsync(command);

            // assert
            handlerResult.Should().BeAssignableTo(typeof(ISuccessResult<>));
        }
    }
}
