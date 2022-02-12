namespace ApplicationRegistry.Database.Entities
{
    public class EnvironmentEntity : IEntity<string>
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public DateTimeOffset CreateDate { get; }

        public EnvironmentEntity(string id, string description = default, DateTimeOffset createDate = default)
        {
            Id = id;
            Description = description;
            CreateDate = createDate == default ? DateTime.UtcNow : createDate;
        }
    }
}
