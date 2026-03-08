using HelfenNeuGedacht.API.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Mysqlx.Crud;

namespace HelfenNeuGedacht.API.Api.Controllers
{
    [ApiController]
    [Route("api/shifts")]
    public class ShiftController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Shift>>> GetAllShifts()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Shift>> GetShift(int Id)
        {
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> AddShift()
        {
            return Created();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> UpdateShift(int Id)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> DeleteShift(int Id)
        {
            return NoContent();
        }
    }
}
