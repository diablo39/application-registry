using ApplicationRegistry.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationRegistry.Domain.Entities
{
    public class CollectorLogEntity : IEntity<long>
    {
        public long Id { get; set; }

        public DateTime CreateDate { get; set; }

        public string Application { get; set; }

        public string Version { get; set; }

        public string Message { get; set; }

        public string Severity { get; set; }

        public string Env { get; set; }
    }
}
