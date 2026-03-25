using HelfenNeuGedacht.API.Application.Mapper;
using HelfenNeuGedacht.API.Application.Repositories;
using HelfenNeuGedacht.API.Application.Services.ShiftServices.Dto;
using HelfenNeuGedacht.API.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace HelfenNeuGedacht.API.Application.Services.ShiftServices
{
    public class ShiftService : IShiftService
    {
        private IShiftRepository _shiftRepositories;
        private IEventRepository _eventRepository;
        private DtoMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public ShiftService(
            IShiftRepository shiftRepository, 
            IEventRepository eventRepository,
            DtoMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager) 
        {
            _shiftRepositories = shiftRepository;
            _eventRepository = eventRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
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
                EventId = shiftRequest.EventId
            };

            var newShift = await _shiftRepositories.AddAsync(shift);

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
            await _shiftRepositories.DeleteAsync(shiftToDelete);

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
            existingShift.EventId = shift.EventId;

            await _shiftRepositories.UpdateAsync(existingShift);

            return _mapper.ToShiftResponse(existingShift);
        }
    }
}
