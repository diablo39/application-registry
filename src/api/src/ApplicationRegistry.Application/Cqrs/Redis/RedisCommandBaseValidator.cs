using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationRegistry.Application.Commands.Redis.Base
{
    public abstract class RedisCommandBaseValidator<T>: AbstractValidator<T>
        where T: RedisCreateCommand
    {
        public RedisCommandBaseValidator()
        {
            RuleFor(e => e.Name).NotEmpty();
        }
    }
}
