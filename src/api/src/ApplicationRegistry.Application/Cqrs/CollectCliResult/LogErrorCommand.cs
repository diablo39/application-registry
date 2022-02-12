using ApplicationRegistry.CQRS.Abstraction;
using ApplicationRegistry.Database;
using ApplicationRegistry.Database.Entities;
using ApplicationRegistry.Domain.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationRegistry.Application.Commands
{
    public class LogErrorCommand : ICommand
    {
        public string Application { get; set; }

        public string Version { get; set; }

        public string Message { get; set; }

        public string Severity { get; set; }

        public string Env { get; set; }
    }

    public class LogErrorCommandValidator : AbstractValidator<LogErrorCommand>
    {
        public LogErrorCommandValidator()
        {

        }
    }

    public class LogErrorCommandResult
    {
    }

    public class LogErrorCommandHandler : ICommandHandler<LogErrorCommand, LogErrorCommandResult>
    {
        private readonly IUnitOfWork _context;

        public LogErrorCommandHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<OperationResult<LogErrorCommandResult>> ExecuteAsync(LogErrorCommand command)
        {
            var result = new LogErrorCommandResult();

            var logEntry = new CollectorLogEntity
            {
                CreateDate = DateTime.UtcNow,
                Application = command.Application,
                Message = command.Message,
                Version = command.Version,
                Env = command.Env,
                Severity = command.Severity
            };

            _context.CollectorLogs.Add(logEntry);
            
            await _context.SaveChangesAsync();

            return OperationResult.Success(result);
        }
    }
}
