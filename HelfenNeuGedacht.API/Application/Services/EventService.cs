using HelfenNeuGedacht.API.Infrastructure.Repositories.MySqlRepository;

namespace HelfenNeuGedacht.API.Application.Services
{
    public class EventService : IEventService
    {
        private readonly ApplicationDbContext _context;
        public EventService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task<Events> CreateEventAsync(Events eventEntity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteEventAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Events>> GetAllEventsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Events> GetEventByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
