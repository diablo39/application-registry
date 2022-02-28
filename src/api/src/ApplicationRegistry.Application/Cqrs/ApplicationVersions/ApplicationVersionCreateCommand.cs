﻿using ApplicationRegistry.Application.Services;
using ApplicationRegistry.CQRS.Abstraction;
using ApplicationRegistry.Database;
using ApplicationRegistry.Database.Entities;

namespace ApplicationRegistry.Application.Cqrs.ApplicationVersions
{
    public class ApplicationVersionCreateCommand : ICommand
    {
        public Guid ApplicationId { get; set; }

        public string EnvironmentId { get; set; }

        public string FrameworkVersion { get; set; }

        public string Version { get; set; }
    }

    public class ApplicationVersionCreateCommandValidator : AbstractValidator<ApplicationVersionCreateCommand>
    {
        public ApplicationVersionCreateCommandValidator()
        {
            RuleFor(e => e.EnvironmentId).NotEmpty().MaximumLength(25);

            RuleFor(e => e.Version).NotEmpty().MaximumLength(250);

            RuleFor(e => e.FrameworkVersion).MaximumLength(60);
        }
    }

    public class ApplicationVersionCreateCommandResult
    {
        public Guid Id { get; set; }
    }

    public class ApplicationVersionCreateCommandHandler : ICommandHandler<ApplicationVersionCreateCommand, ApplicationVersionCreateCommandResult>
    {
        private readonly IUnitOfWork _context;
        private readonly IGuidGenerator _guidGenerator;

        public ApplicationVersionCreateCommandHandler(IUnitOfWork context, IGuidGenerator guidGenerator)
        {
            _context = context;
            _guidGenerator = guidGenerator;
        }

        public async Task<OperationResult<ApplicationVersionCreateCommandResult>> ExecuteAsync(ApplicationVersionCreateCommand command)
        {
            var applicationVersion = new ApplicationVersionEntity(_guidGenerator.CreateNewSequentialGuid(), command.ApplicationId, command.EnvironmentId, command.Version)
            {
                FrameworkVersion = command.FrameworkVersion,
            };

            _context.ApplicationVersions.Add(applicationVersion);

            await _context.SaveChangesAsync();
            var result = new ApplicationVersionCreateCommandResult { Id = applicationVersion.Id };

            return OperationResult.Success(result);
        }
    }
}
