using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationRegistry.Application.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class SwaggerIgnorePropertyAttribute : Attribute
    {
        public string Name { get; set; }

        public SwaggerIgnorePropertyAttribute(string name)
        {
            Name = name;
        }
    }
}
