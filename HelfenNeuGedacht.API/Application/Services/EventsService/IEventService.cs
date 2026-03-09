using HelfenNeuGedacht.API.Application.Services.EventsService.Dto;

namespace HelfenNeuGedacht.API.Application.Services.EventsService
{
    public interface IEventService
    {
        Task<IEnumerable<EventResponse>> GetAllEventsAsync();
        Task<EventResponse> GetEventByIdAsync(int id);
        Task<EventResponse> CreateEventAsync(EventRequest eventEntity);
        Task DeleteEventAsync(int id);
    }
}
