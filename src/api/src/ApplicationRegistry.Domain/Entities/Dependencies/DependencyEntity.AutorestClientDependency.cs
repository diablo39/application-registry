using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationRegistry.Database.Entities
{

    public class AutorestClientDependency : DependencyEntity
    {
        public class Operation
        {
            public string OperationId { get; set; }

            public string Path { get; set; }

            public bool IsInUse { get; set; }
        }

        internal AutorestClientDependency() { }

        public AutorestClientDependency(string clientId)
            : base(GetId(clientId), clientId, DependencyTypes.AutorestClient)
        {

        }

        public static string GetId(string clientId)
        {
            return string.Concat(DependencyTypes.AutorestClient, ":", clientId);
        }
    }
}
