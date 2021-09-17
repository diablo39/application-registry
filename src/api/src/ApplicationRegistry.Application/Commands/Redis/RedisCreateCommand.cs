using ApplicationRegistry.Application.Commands.Redis.Base;
using ApplicationRegistry.CQRS.Abstraction;
using ApplicationRegistry.Database;
using ApplicationRegistry.Domain.Entities.Redis;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationRegistry.Application.Commands.Redis
{
    public class RedisCreateCommand : RedisCommandBase, ICommand
    {

    }

    public class RedisCreateCommandValidator : RedisCommandBaseValidator<RedisCreateCommand>
    {
        public RedisCreateCommandValidator(): base()
        {

        }
    }

    public class RedisCreateCommandResult
    {

    }

    public class RedisCreateCommandHandler : ICommandHandler<RedisCreateCommand, RedisCreateCommandResult>
    {
        private readonly IUnitOfWork _context;

        public RedisCreateCommandHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<OperationResult<RedisCreateCommandResult>> ExecuteAsync(RedisCreateCommand command)
        {
            var redis = new RedisEntity
            {
                CreateDate = DateTime.Now,
                Description = command.Description,
                GroupId = command.GroupId,
                Id = command.Id,
                IdEnvironment = command.IdEnvironment,
                Name = command.Name,
                NetworkZone = command.NetworkZone,
                Password = command.Password,
                RedisDeploymentTypeId = command.RedisDeploymentTypeId,
                Requester = command.Requester,
                RfcId = command.RfcId,
                SentinelRedisId = command.SentinelRedisId,
                Endpoints = command.Endpoints?.Select(e => new RedisEndpointEntity { Host = e.Host, Id = e.Id, Port = e.Port, RedisId = command.Id }).ToList()
            };

            _context.Redis.Add(redis);

            await _context.SaveChangesAsync();

            return OperationResult.Success(new RedisCreateCommandResult());
        }
    }
}
