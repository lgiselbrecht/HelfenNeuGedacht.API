using HelfenNeuGedacht.API.Application.Services.ShiftServices.Dto;

namespace HelfenNeuGedacht.API.Application.Services.EventsService.Dto
{
    public class EventResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int RequiredHelpers { get; set; }
        public int OrganizationId { get; set; }
        public List<ShiftResponse>? Shifts { get; set; }
    }
}
