using ApplicationRegistry.CQRS.Abstraction;
using ApplicationRegistry.Database;
using ApplicationRegistry.Database.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationRegistry.Application.Cqrs.Environments
{
    public class EnvironmentUpdateCommand : ICommand
    {
        public virtual string Id { get; set; }

        public virtual string Description { get; set; }
    }

    public class EnvironmentUpdateCommandValidator : AbstractValidator<EnvironmentUpdateCommand>
    {
        public EnvironmentUpdateCommandValidator()
        {
            RuleFor(e => e.Id).NotNull().NotEmpty();
        }
    }

    public class EnvironmentUpdateCommandResult
    {
        public string Id { get; set; }
    }

    public class EnvironmentUpdateCommandHandler : ICommandHandler<EnvironmentUpdateCommand, EnvironmentUpdateCommandResult>
    {
        private readonly IUnitOfWork _context;

        public EnvironmentUpdateCommandHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<OperationResult<EnvironmentUpdateCommandResult>> ExecuteAsync(EnvironmentUpdateCommand command)
        {
            var env = _context.EnvironmentsRepository.Get(command.Id);

            if (env == null)
            {
                env = new EnvironmentEntity { Id = command.Id, CreateDate = DateTime.Now };
                _context.EnvironmentsRepository.Add(env);
            }

            env.Description = command.Description;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return OperationResult.BusinessError<EnvironmentUpdateCommandResult>(new Dictionary<string, string> { { "Id", $"Environment with id: {command.Id} already exists" } });
            }
            catch (Exception)
            {
                throw;
            }

            var result = new EnvironmentUpdateCommandResult { Id = env.Id };

            return OperationResult.Success(result);
        }
    }
}
