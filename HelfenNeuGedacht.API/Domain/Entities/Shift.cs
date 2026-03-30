namespace HelfenNeuGedacht.API.Domain.Entities
{
    public class Shift : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Requirements { get; set; } = string.Empty;
        public int AgeRestriction { get; set; } = 0;
        public int Points { get; set; }
        public int RequiredHelpers { get; set; } = 0;
        public int EventId { get; set; }
        public Event? Event { get; set; }
    }
}
