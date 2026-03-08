using HelfenNeuGedacht.API.Application.Services.OrganizationService;
using HelfenNeuGedacht.API.Application.Services.OrganizationService.Dto;
using Microsoft.AspNetCore.Mvc;

namespace HelfenNeuGedacht.API.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;

        public OrganizationController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        [HttpPost]
        public async Task<ActionResult<OrganizationResponse>> CreateOrganization(CreateOrganizationRequest organization)
        {
            if (organization == null)
                return BadRequest("no data recieved");

            var createdOrganization = await _organizationService.CreateOrganizationAsync(organization);

           return Ok(createdOrganization);
           //return CreatedAtAction(nameof(GetById), new { id = newOrganization.Id }, newLecture);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Organization>> GetOrganizationById(int id)
        {
            var organization = await _organizationService.GetOrganizationByIdAsync(id);

            if (organization == null)
                return NotFound($"Keine Organization mit der ID {id} gefunden.");

            return Ok(organization);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OrganizationResponse>> UpdateOrganizationById(int id, CreateOrganizationRequest updatedOrganization)
        {
            if (updatedOrganization == null)
                return BadRequest("UpdatedOrganization darf nicht null sein.");


            var result = await _organizationService.UpdateOrganizationByIdAsync(id, updatedOrganization);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Organization>> DeleteOrganizationById(int id)
        {
            var existingOrganization = await _organizationService.GetOrganizationByIdAsync(id);

            if (existingOrganization == null)
                return NotFound($"Keine Organization mit der ID {id} gefunden.");

            var deletedOrganization = await _organizationService.DeleteOrganizationByIdAsync(id);

            return Ok(deletedOrganization);
        }

        [HttpPost("{organizationId}/admins/{adminUserId}")]
        public async Task<ActionResult<Organization>> CreateOrganizationAdmin(int organizationId, string adminUserId)
        {
            var organization = await _organizationService.GetOrganizationByIdAsync(organizationId);

            if (organization == null)
                return NotFound($"Keine Organization mit der ID {organizationId} gefunden.");

            var result = await _organizationService.CreateOrganizationAdminAsync(organizationId, adminUserId);

            return Ok(result);
        }

        [HttpGet("{id}/admins")]
        public async Task<ActionResult<IEnumerable<OrganizationResponse>>> GetOrganizationAdmins(int id)
        {
            var organization = await _organizationService.GetOrganizationByIdAsync(id);

            if (organization == null)
                return NotFound($"Keine Organization mit der ID {id} gefunden.");

            var admins = await _organizationService.GetOrganizationAdminsAsync(id);

            return Ok(admins);
        }

        [HttpPut("{organizationId}/admins")]
        public async Task<ActionResult<OrganizationResponse>> UpdateOrganizationAdmin(int organizationId, Organization updatedOrganization)
        {
          throw new NotImplementedException();
        }

        [HttpDelete("{organizationId}/admins/{adminUserId}")]
        public async Task<ActionResult<OrganizationResponse>> DeleteOrganizationAdmin(int organizationId, string adminUserId)
        {
            var organization = await _organizationService.GetOrganizationByIdAsync(organizationId);

            if (organization == null)
                return NotFound($"Keine Organization mit der ID {organizationId} gefunden.");

            var result = await _organizationService.DeleteOrganizationAdminAsync(organizationId, adminUserId);

            return Ok(result);
        }
    }
}