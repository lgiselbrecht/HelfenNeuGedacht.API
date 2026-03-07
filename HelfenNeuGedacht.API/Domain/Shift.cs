using HelfenNeuGedacht.API.Domain.Entities;

namespace HelfenNeuGedacht.API.Domain
{
    public class Shift : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int AgeRestriction { get; set; } = 0;

        public int Points { get; set; }

    }
}
