using HelfenNeuGedacht.API.Application.Services.EventsService.Dto;

namespace HelfenNeuGedacht.API.Application.Services.EventsService
{
    public interface IEventService
    {
        Task<List<EventResponse>> GetAllEventsAsync();
        Task<EventResponse> GetEventByIdAsync(int id);
        Task<EventResponse> UpdateEventAsync(EventRequest eventEntity);
        Task<EventResponse> CreateEventAsync(EventRequest eventEntity);
        Task<EventResponse> DeleteEventAsync(int id);
    }
}
