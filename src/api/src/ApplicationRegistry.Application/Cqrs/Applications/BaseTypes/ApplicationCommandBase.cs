using ApplicationRegistry.Application.Cqrs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationRegistry.Application.Commands
{
    public abstract class ApplicationCommandBase
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public string Owner { get; set; }

        public string RepositoryUrl { get; set; }

        public string BuildProcessUrls { get; set; }

        public string Framework { get; set; }

        public Guid IdProject { get; set; }

    }

}
