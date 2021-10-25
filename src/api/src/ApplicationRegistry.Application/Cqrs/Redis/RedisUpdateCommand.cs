using ApplicationRegistry.CQRS.Abstraction;
using ApplicationRegistry.Database;
using ApplicationRegistry.Domain.Entities.Redis;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationRegistry.Application.Commands.Redis
{
    public class RedisUpdateCommand : RedisCommandBase, ICommand
    {

    }

    public class RedisUpdateCommandValidator : AbstractValidator<RedisUpdateCommand>
    {
        public RedisUpdateCommandValidator()
        {
            
        }
    }

    public class RedisUpdateCommandResult
    {
    }

    public class RedisUpdateCommandHandler : ICommandHandler<RedisUpdateCommand, RedisUpdateCommandResult>
    {
        private readonly IUnitOfWork _context;

        public RedisUpdateCommandHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<OperationResult<RedisUpdateCommandResult>> ExecuteAsync(RedisUpdateCommand command)
        {
            var redis = await _context.RedisRepository.GetWithEndpointsAsync(command.Id);

            if (redis == null)
            {
                return OperationResult.BusinessError<RedisUpdateCommandResult>(new KeyValuePair<string, string>(nameof(command.Id), $"Redis with id: { command.Id } not exists"));
            }

            redis.Description = command.Description;
            redis.GroupId = command.GroupId;
            redis.IdEnvironment = command.IdEnvironment;
            redis.Name = command.Name;
            redis.NetworkZone = command.NetworkZone;
            redis.Password = command.Password;
            redis.RedisDeploymentTypeId = command.RedisDeploymentTypeId;
            redis.Requester = command.Requester;
            redis.RfcId = command.RfcId;
            redis.SentinelRedisId = command.SentinelRedisId;

            foreach (var existingChild in redis.Endpoints.Where(existingChild => !command.Endpoints.Any(c => c.Id == existingChild.Id)).ToList())
            {
                redis.Endpoints.Remove(existingChild);
            }

            foreach (var childModel in command.Endpoints)
            {
                var existingChild = redis.Endpoints
                    .Where(c => c.Id == childModel.Id && c.Id != default)
                    .SingleOrDefault();

                if (existingChild != null)
                {
                    existingChild.Host = childModel.Host;
                    existingChild.Port = childModel.Port;
                }

                else
                {
                    var newChild = new RedisEndpointEntity
                    {
                        Host = childModel.Host,
                        Id = childModel.Id == default ? Guid.NewGuid() : childModel.Id,
                        Port = childModel.Port,
                        RedisId = redis.Id
                    };
                    redis.Endpoints.Add(newChild);
                }
            }

            return OperationResult.Success(new RedisUpdateCommandResult());
        }
    }
}
