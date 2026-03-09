using HelfenNeuGedacht.API.Application.Mapper;
using HelfenNeuGedacht.API.Application.Repositories;
using HelfenNeuGedacht.API.Application.Services.EventsService.Dto;
using HelfenNeuGedacht.API.Domain.Entities;

using HelfenNeuGedacht.API.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using HelfenNeuGedacht.API.Infrastructure.Repositories.MySqlRepository;

namespace HelfenNeuGedacht.API.Application.Services.EventsService
{
    public class EventService : IEventService
    {
        private IEventRepository _eventRepository;
        private DtoMapper _mapper;

        public EventService(IEventRepository eventRepository, DtoMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }
        public async Task<EventResponse> CreateEventAsync(EventRequest eventEntity)
        {
            var events = new HelpingEvents()
            {
                Title = eventEntity.Title,
                Description = eventEntity.Description,
                Location = eventEntity.Location,
                StartDate = eventEntity.StartDate,
                EndDate = eventEntity.EndDate,
            };
            
            var newEvent = await _eventRepository.AddAsync(events);
            return _mapper.ToEventResponse(newEvent);
            //throw new NotImplementedException();
        }

        public Task DeleteEventAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteEventAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<HelpingEvents>> GetAllEventsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<HelpingEvents> GetEventByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<HelpingEvents> GetEventByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<EventResponse>> IEventService.GetAllEventsAsync()
        {
            throw new NotImplementedException();
        }

        Task<EventResponse> IEventService.GetEventByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
