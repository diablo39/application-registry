using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentValidation
{
    static class CommonValidators
    {
        public static IRuleBuilder<T, string> IsName<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.NotNull().NotEmpty().MaximumLength(400);
        }

        public static IRuleBuilder<T, string> IsDescription<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.MaximumLength(1200);
        }
    }
}
