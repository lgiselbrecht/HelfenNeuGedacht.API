using System.Security.Claims;
using HelfenNeuGedacht.API.Application.Mapper;
using HelfenNeuGedacht.API.Application.Repositories;
using HelfenNeuGedacht.API.Application.Services.EventsService.Dto;
using HelfenNeuGedacht.API.Domain.Entities;
using HelfenNeuGedacht.API.Infrastructure.Repositories;
using HelfenNeuGedacht.API.Infrastructure.Repositories.MySqlRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace HelfenNeuGedacht.API.Application.Services.EventsService
{
    public class EventService : IEventService
    {
        private IEventRepository _eventRepository;
        private DtoMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHubContext<DashboardHub> _dashboardHub;

        public EventService(
            IEventRepository eventRepository, 
            DtoMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager,
            IHubContext<DashboardHub> dashboardHub
            )

        {
            _eventRepository = eventRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _dashboardHub = dashboardHub;
        }

        private async Task<int?> GetUserOrganizationIdAsync()
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return null;

            var user = await _userManager.FindByIdAsync(userId);
            return user?.OrganizationId;
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
            var groupName = $"organization-{newEvent.OrganizationId}";
            await _dashboardHub.Clients.Group(groupName).SendAsync("dashboardUpdated");
            return _mapper.ToEventResponse(newEvent);
        }

        public async Task<EventResponse> DeleteEventAsync(int id)
        {
            var eventToDelete = await _eventRepository.FindByIdAsync(id);
            await _eventRepository.DeleteAsync(eventToDelete);
          
            var orgId = eventToDelete.OrganizationId;
            var groupName = $"organization-{orgId}";
            await _dashboardHub.Clients.Group(groupName).SendAsync("dashboardUpdated");
            return _mapper.ToEventResponse(eventToDelete);
        }

        public async Task<List<EventResponse>> GetAllEventsAsync()
        {
            var organizationId = await GetUserOrganizationIdAsync();

            // empty list if user has no organization            
            if (!organizationId.HasValue)
            {
                return new List<EventResponse>();
            }

            var allEvents = await _eventRepository.FindByOrganizationIdAsync(organizationId.Value);
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

        // This method is currently not used, but it can be useful for future features like an Superadmin dashboard
     
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
