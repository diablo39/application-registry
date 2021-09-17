using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationRegistry.Database.Entities
{
    public class CollectorKnowledgeBaseEntity : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public DateTime CreateDate { get; set; }

        public string ClientType { get; set; }

        public Guid IdApplicationServer { get; set; }

        public Guid IdApplicationClient { get; set; }

        /// <summary>
        /// Source of knowledge. Application version dependency with autorest client
        /// </summary>
        public Guid IdApplicationVersionDependencyParent { get; set; }

        /// <summary>
        /// Source of the knoledge. Appliction version dependency with application relation
        /// </summary>
        public Guid IdApplicationVersionDependencyChild { get; set; }

        internal CollectorKnowledgeBaseEntity() { }

        public CollectorKnowledgeBaseEntity(
            string autorestClientType,
            Guid idApplicationClient,
            Guid idApplicationServer,
            Guid idApplicationVersionDependencyChild,
            Guid idApplicationVersionDependencyParent)
        {
            ClientType = autorestClientType ?? throw new ArgumentNullException(nameof(autorestClientType));
            IdApplicationClient = idApplicationClient;
            IdApplicationServer = idApplicationServer;
            IdApplicationVersionDependencyChild = idApplicationVersionDependencyChild;
            IdApplicationVersionDependencyParent = idApplicationVersionDependencyParent;
        }
    }

}
