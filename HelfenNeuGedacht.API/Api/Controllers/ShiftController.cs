using HelfenNeuGedacht.API.Application.Services.ShiftServices;
using HelfenNeuGedacht.API.Application.Services.ShiftServices.Dto;
using HelfenNeuGedacht.API.Domain.Constants;
using HelfenNeuGedacht.API.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mysqlx.Crud;

namespace HelfenNeuGedacht.API.Api.Controllers
{
    [ApiController]
    [Route("api/shifts")]
    [Authorize(Roles = $"{Roles.OrganizationAdmin},{Roles.OrganizationUser},{Roles.SuperAdmin}")]
    public class ShiftController : ControllerBase
    {
        private readonly IShiftService _shiftService;

        public ShiftController(IShiftService shiftService) {
            _shiftService = shiftService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ShiftResponse>>> GetAllShifts([FromQuery] int? eventId)
        {
            if (eventId.HasValue)
            {
                var shifts = await _shiftService.GetShiftsByEventIdAsync(eventId.Value);
                return Ok(shifts);
            }
            else
            {
                var shifts = await _shiftService.GetAllShiftsAsync();
                return Ok(shifts);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ShiftResponse>> GetShift(int Id)
        {
            var shift = await _shiftService.GetShiftByIdAsync(Id);

            if (shift == null)
            {
                return NotFound("no data recieved");
            }

            return Ok(shift);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ShiftResponse>> AddShift(CreateShiftRequest shift)
        {
            if (shift == null)
            {
                return BadRequest("no data recieved");
            }

            var createdShift = await _shiftService.AddShiftAsync(shift);
            if (createdShift == null)
            {
                return NotFound("Event not found");
                    }
            return Created();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ShiftResponse>> UpdateShift(int Id, UpdateShiftRequest shift)
        {
            if(shift == null)
            {
                return BadRequest();
            }

            var updatedShift = await _shiftService.UpdateShiftAsync(Id, shift);
            if (updatedShift == null)
            {
                return NotFound("Shift or Event not found!");
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<Shift>> DeleteShift(int Id)
        {
            var shift = await _shiftService.GetShiftByIdAsync(Id);
            if (shift == null)
                return NotFound($"Kein Dienst gefunden");

            var deletedShift = await _shiftService.DeleteShiftAsync(Id);
            return NoContent();
        }
    }
}
