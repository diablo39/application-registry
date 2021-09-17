using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationRegistry.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public IEnumerable<KeyValuePair<string, string>> ValidationErrors { get; protected set; }

        protected DomainException()
            : base()
        {

        }

        public DomainException(string property, string errorMessage)
            : this()
        {
            ValidationErrors = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>(property, errorMessage) };
        }

        public DomainException(IEnumerable<KeyValuePair<string, string>> errors)
            : this()
        {
            ValidationErrors = errors;
        }

        public DomainException(params KeyValuePair<string, string>[] errors)
            : this()
        {
            ValidationErrors = errors;
        }
    }
}
