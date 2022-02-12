using ApplicationRegistry.CQRS.Abstraction;
using ApplicationRegistry.Database;
using ApplicationRegistry.Database.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationRegistry.Application.Cqrs.Environments
{
    public class EnvironmentCreateCommand : ICommand
    {
        public string Id { get; set; }

        public string Description { get; set; }
    }

    public class EnvironmentCreateCommandValidator : AbstractValidator<EnvironmentCreateCommand>
    {
        public EnvironmentCreateCommandValidator()
        {
            RuleFor(e => e.Id).NotNull().NotEmpty();
        }
    }

    public class EnvironmentCreateCommandResult
    {
        public string Id { get; set; }
    }

    public class EnvironmentCreateCommandHandler : ICommandHandler<EnvironmentCreateCommand, EnvironmentCreateCommandResult>
    {
        private readonly IUnitOfWork _context;

        public EnvironmentCreateCommandHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<OperationResult<EnvironmentCreateCommandResult>> ExecuteAsync(EnvironmentCreateCommand command)
        {
            var env = new EnvironmentEntity(command.Id, command.Description);

            _context.EnvironmentsRepository.Add(env);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return OperationResult.BusinessError<EnvironmentCreateCommandResult>(new Dictionary<string, string> { { "Id", $"Environment with id: {command.Id} already exists" } });
            }
            catch (Exception)
            {
                throw;
            }

            var result = new EnvironmentCreateCommandResult { Id = env.Id };

            return OperationResult.Success(result);
        }
    }
}
