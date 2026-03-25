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
                OrganizationId = eventEntity.OrganizationId
            };

            var newEvent = await _eventRepository.AddAsync(events);
            return _mapper.ToEventResponse(newEvent);
        }

        public async Task<EventResponse> DeleteEventAsync(int id)
        {
            var eventToDelete = await _eventRepository.FindByIdAsync(id);
            await _eventRepository.DeleteAsync(eventToDelete);

            return _mapper.ToEventResponse(eventToDelete);
        }

        public async Task<List<EventResponse>> GetAllEventsAsync()
        {
            var allEvents = await _eventRepository.FindAllAsync();
            List<EventResponse> eventResponses = new List<EventResponse>();

            foreach (var events in allEvents)
            {
                eventResponses.Add(_mapper.ToEventResponse(events));
            }
            return eventResponses;
        }

        public async Task<EventResponse?> GetEventByIdAsync(int id, bool includeShifts = false)
        {
            HelpingEvents? events;
            
            if (includeShifts)
            {
                events = await _eventRepository.FindByIdWithShiftsAsync(id);
            }
            else
            {
                events = await _eventRepository.FindByIdAsync(id);
            }
            
            if (events == null)
            {
                return null;
            }

            return _mapper.ToEventResponse(events, includeShifts);
        }

        public async Task<List<EventResponse>> GetEventsByOrganizationIdAsync(int organizationId)
        {
            var events = await _eventRepository.FindByOrganizationIdAsync(organizationId);
            return events.Select(e => _mapper.ToEventResponse(e)).ToList();
        }

        public async Task<EventResponse> UpdateEventAsync(EventRequest eventEntity)
        {
            var existingEvent = await _eventRepository.FindByIdAsync(eventEntity.Id);

            if (existingEvent == null)
            {
                throw new Exception("Event not found");
            }

            existingEvent.Title = eventEntity.Title;
            existingEvent.Description = eventEntity.Description;
            existingEvent.Location = eventEntity.Location;
            existingEvent.StartDate = eventEntity.StartDate;
            existingEvent.EndDate = eventEntity.EndDate;
            existingEvent.OrganizationId = eventEntity.OrganizationId;

            await _eventRepository.UpdateAsync(existingEvent);
            return _mapper.ToEventResponse(existingEvent);
        }
    }
}
