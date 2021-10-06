using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationRegistry.Application.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class SwaggerIgnorePropertyAttribute : Attribute
    {
        public string[] Names { get; set; }

        public SwaggerIgnorePropertyAttribute(params string[] names)
        {
            Names = names;
        }
    }
}
