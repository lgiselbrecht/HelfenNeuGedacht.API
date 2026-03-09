using HelfenNeuGedacht.API.Application.Services.EventsService;
using HelfenNeuGedacht.API.Application.Services.EventsService.Dto;
using Microsoft.AspNetCore.Mvc;

namespace HelfenNeuGedacht.API.Api.Controllers
{
    [ApiController]
    [Route("api/events")]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventResponse>>> GetAllEvents()
        {
            var events = await _eventService.GetAllEventsAsync();
            return Ok(events);
        }

        // Einzelnes Event abrufen
        [HttpGet("{id}")]
        public async Task<ActionResult<EventResponse>> GetEventById(int id)
        {
            var events = await _eventService.GetEventByIdAsync(id);
            if (events == null)
                return NotFound($"Event mit der ID {id} wurde nicht gefunden.");

            return Ok(events);
        }
    }
}
