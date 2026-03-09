namespace HelfenNeuGedacht.API.Application.Services
{
    public interface IEventService
    {
        Task<IEnumerable<Events>> GetAllEventsAsync();
        Task<Events> GetEventByIdAsync(Guid id);
        Task<Events> CreateEventAsync(Events eventEntity);
        Task DeleteEventAsync(Guid id);
    }
}
