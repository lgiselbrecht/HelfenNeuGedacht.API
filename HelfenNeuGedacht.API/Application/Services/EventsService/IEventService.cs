using HelfenNeuGedacht.API.Application.Services.EventsService.Dto;

namespace HelfenNeuGedacht.API.Application.Services.EventsService
{
    public interface IEventService
    {
        Task<List<EventResponse>> GetAllEventsAsync();
        Task<List<EventResponse>> GetEventsByOrganizationIdAsync(int organizationId);
        Task<EventResponse> GetEventByIdAsync(int id, bool includeShifts = false);
        Task<EventResponse> UpdateEventAsync(int id, EventRequest eventEntity);
        Task<EventResponse> CreateEventAsync(EventRequest eventEntity);
        Task<EventResponse> DeleteEventAsync(int id);
    }
}
