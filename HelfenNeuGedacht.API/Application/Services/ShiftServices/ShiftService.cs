using System.Security.Claims;
using HelfenNeuGedacht.API.Application.Mapper;
using HelfenNeuGedacht.API.Application.Repositories;
using HelfenNeuGedacht.API.Application.Services.EventsService;
using HelfenNeuGedacht.API.Application.Services.ShiftServices.Dto;
using HelfenNeuGedacht.API.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace HelfenNeuGedacht.API.Application.Services.ShiftServices
{
    public class ShiftService : IShiftService
    {
        private IShiftRepository _shiftRepositories;
        private IEventRepository _eventRepository;
        private DtoMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHubContext<DashboardHub> _dashboardHub;
        private readonly IEventService _eventService;

        public ShiftService(
            IShiftRepository shiftRepository, 
            IEventRepository eventRepository,
            DtoMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager,
            IHubContext<DashboardHub> dashboardHub,
            IEventService eventService


            ) 

        {
            _shiftRepositories = shiftRepository;
            _eventRepository = eventRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _dashboardHub = dashboardHub;
            _eventService = eventService;

        }

        private async Task<int?> GetUserOrganizationIdAsync()
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return null;

            var user = await _userManager.FindByIdAsync(userId);
            return user?.OrganizationId;
        }

        public async Task<ShiftResponse> AddShiftAsync(CreateShiftRequest shiftRequest)
        {
            var shift = new Shift()
            {
                Name = shiftRequest.Name,
                Description = shiftRequest.Description,
                Requirements = shiftRequest.Requirements,
                AgeRestriction = shiftRequest.AgeRestriction,
                Points = shiftRequest.Points,
                RequiredHelpers = shiftRequest.RequiredHelpers,
                EventId = shiftRequest.EventId
            };

            //für SignalR Update Dashboard bitte lassen //TODO: Komentar entfernen
            var newShift = await _shiftRepositories.AddAsync(shift);
            var shiftEvent = await _eventService.GetEventByIdAsync(newShift.EventId);
            var groupName = $"organization-{shiftEvent.OrganizationId}";
            await _dashboardHub.Clients.Group(groupName).SendAsync("dashboardUpdated");

            return _mapper.ToShiftResponse(newShift);
        }

        public async Task<List<ShiftResponse>> GetShiftsByEventIdAsync(int eventId)
        {
            var shifts = await _shiftRepositories.FindByEventIdAsync(eventId);
            return shifts.Select(s => _mapper.ToShiftResponse(s)).ToList();
        }


        public async Task<ShiftResponse> DeleteShiftAsync(int id)
        {
            var shiftToDelete = await _shiftRepositories.FindByIdAsync(id);
            if (shiftToDelete == null) {
                return null;
            }

            await _shiftRepositories.DeleteAsync(shiftToDelete);
              

            //für SignalR Update Dashboard bitte lassen //TODO: Komentar entfernen
            var shiftEvent = await _eventService.GetEventByIdAsync(shiftToDelete.EventId);
            var groupName = $"organization-{shiftEvent.OrganizationId}";
            await _dashboardHub.Clients.Group(groupName).SendAsync("dashboardUpdated");


            return _mapper.ToShiftResponse(shiftToDelete);
        }

        public async Task<List<ShiftResponse?>> GetAllShiftsAsync()
        {
            var organizationId = await GetUserOrganizationIdAsync();
            
            if (!organizationId.HasValue)
            {
                return new List<ShiftResponse>();
            }

    
            var organizationEvents = await _eventRepository.FindByOrganizationIdAsync(organizationId.Value);
            var eventIds = organizationEvents.Select(e => e.Id).ToList();

    
            var allShifts = await _shiftRepositories.FindAllAsync();
            var organizationShifts = allShifts.Where(s => eventIds.Contains(s.EventId));

            List<ShiftResponse> allShiftsResponse = new List<ShiftResponse>();

            foreach (var shift in organizationShifts) {
                allShiftsResponse.Add(_mapper.ToShiftResponse(shift));
            }

            return allShiftsResponse;
        }

        public async Task<ShiftResponse?> GetShiftByIdAsync(int id)
        {
            var shift = await _shiftRepositories.FindByIdAsync(id);

            if(shift == null)
            {
                return null;
            }

            return _mapper.ToShiftResponse(shift);
        }

        public async Task<ShiftResponse?> UpdateShiftAsync(int id, UpdateShiftRequest shift)
        {
            var existingShift = await _shiftRepositories.FindByIdAsync(id);

            if (existingShift == null) {
                return null;
            }

            existingShift.Name = shift.Name;
            existingShift.Description = shift.Description;
            existingShift.Requirements = shift.Requirements;
            existingShift.AgeRestriction = shift.AgeRestriction;
            existingShift.Points = shift.Points;
            //existingShift.RequiredHelpers = shift.RequiredHelpers;
            existingShift.EventId = shift.EventId;

            await _shiftRepositories.UpdateAsync(existingShift);

            return _mapper.ToShiftResponse(existingShift);
        }
    }
}
