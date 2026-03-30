using HelfenNeuGedacht.API.Application.Services.EventsService;
using HelfenNeuGedacht.API.Application.Services.EventsService.Dto;
using HelfenNeuGedacht.API.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelfenNeuGedacht.API.API.Controllers


{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = $"{Roles.OrganizationAdmin},{Roles.OrganizationUser}")]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<ActionResult<List<EventResponse>>> GetAllEvents([FromQuery] int? organizationId)
        {
            if (organizationId.HasValue)
            {
                var events = await _eventService.GetEventsByOrganizationIdAsync(organizationId.Value);
                return Ok(events);
            }
            else
            {
                var events = await _eventService.GetAllEventsAsync();
                return Ok(events);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<EventResponse>> GetEventById(int id, [FromQuery] bool includeShifts = false)
        {
            var events = await _eventService.GetEventByIdAsync(id, includeShifts);
            if (events == null)
                return NotFound($"Event mit der ID {id} wurde nicht gefunden.");
            return Ok(events);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<EventResponse>> CreateEvent(EventRequest eventRequest)
        {
            if (eventRequest == null)
                return BadRequest("no data recieved");
            var createdEvent = await _eventService.CreateEventAsync(eventRequest);
            return CreatedAtAction(nameof(GetEventById), new { id = createdEvent.Id }, createdEvent);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<EventResponse>> UpdateEvent(int id, EventRequest eventRequest)
        {
            if (eventRequest == null)
                return BadRequest("no data recieved");
            var updatedEvent = await _eventService.UpdateEventAsync(id, eventRequest);
            if (updatedEvent == null)
                return NotFound("Event not found");
            return Ok(updatedEvent);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteEvent(int id)
        {
            var events = await _eventService.GetEventByIdAsync(id);
            if (events == null)
                return NotFound($"Event mit der ID {id} wurde nicht gefunden.");
            await _eventService.DeleteEventAsync(id);
            return NoContent();
        }
    }
}
