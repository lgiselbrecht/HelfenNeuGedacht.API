namespace HelfenNeuGedacht.API.Application.Services.ShiftServices.Dto
{
    public class ShiftResponse
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Requirements { get; set; } = string.Empty;

        public int AgeRestriction { get; set; } = 0;

        public int Points { get; set; }

        public int EventId { get; set; }
    }
}
