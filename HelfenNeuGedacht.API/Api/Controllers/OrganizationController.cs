using HelfenNeuGedacht.API.Application.Services.OrganizationService;
using HelfenNeuGedacht.API.Application.Services.OrganizationService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelfenNeuGedacht.API.API.Controllers
//TODO: Add/check Authorization
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

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<OrganizationRegistrationResponse>> RegisterOrganization([FromBody] OrganizationRegistrationRequest request)
        {
            if (request == null)
                return BadRequest("Keine Daten erhalten");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _organizationService.RegisterOrganizationWithAdminAsync(request, request.Password);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "OrganizationAdmin,SuperAdmin")]
        public async Task<ActionResult<OrganizationResponse>> CreateOrganization(OrganizationRequest organization)
        {
            if (organization == null)
                return BadRequest("no data recieved");

            var createdOrganization = await _organizationService.CreateOrganizationAsync(organization);

           return Ok(createdOrganization);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "OrganizationAdmin,SuperAdmin")]
        public async Task<ActionResult<Organization>> GetOrganizationById(int id)
        {
            var organization = await _organizationService.GetOrganizationByIdAsync(id);

            if (organization == null)
                return NotFound($"Keine Organization mit der ID {id} gefunden.");

            return Ok(organization);
        }
         [Authorize(Roles = "OrganizationAdmin,SuperAdmin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<OrganizationResponse>> UpdateOrganizationById(int id, OrganizationRequest updatedOrganization)
        {
            if (updatedOrganization == null)
                return BadRequest("UpdatedOrganization darf nicht null sein.");


            var result = await _organizationService.UpdateOrganizationByIdAsync(id, updatedOrganization);

            return Ok(result);
        }
        [Authorize(Roles = "SuperAdmin")]
        [HttpPut("{id}/approve")]
        public async Task<ActionResult<OrganizationResponse>> ApproveOrganizationById(OrganizationApprovedRequest approveRequest, string adminuser)
        {
            var result = await _organizationService.ApproveOrganization(approveRequest, adminuser);
            if (result == null)
                return NotFound($"Keine Organization mit der ID {approveRequest.OrganisationId} gefunden.");
            return Ok(result);
        }

         [Authorize(Roles = "SuperAdmin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Organization>> DeleteOrganizationById(int id)
        {
            var existingOrganization = await _organizationService.GetOrganizationByIdAsync(id);

            if (existingOrganization == null)
                return NotFound($"Keine Organization mit der ID {id} gefunden.");

            var deletedOrganization = await _organizationService.DeleteOrganizationByIdAsync(id);

            return Ok(deletedOrganization);
        }
         [Authorize(Roles = "OrganizationAdmin,SuperAdmin")]
        [HttpPost("{organizationId}/admins/{adminUserId}")]
        public async Task<ActionResult<Organization>> CreateOrganizationAdmin(int organizationId, string adminUserId)
        {
            var organization = await _organizationService.GetOrganizationByIdAsync(organizationId);

            if (organization == null)
                return NotFound($"Keine Organization mit der ID {organizationId} gefunden.");

            var result = await _organizationService.CreateOrganizationAdminAsync(organizationId, adminUserId);

            return Ok(result);
        }
         [Authorize(Roles = "OrganizationAdmin,SuperAdmin")]
        [HttpGet("{id}/admins")]
        public async Task<ActionResult<IEnumerable<OrganizationResponse>>> GetOrganizationAdmins(int id)
        {
            var organization = await _organizationService.GetOrganizationByIdAsync(id);

            if (organization == null)
                return NotFound($"Keine Organization mit der ID {id} gefunden.");

            var admins = await _organizationService.GetOrganizationAdminsAsync(id);

            return Ok(admins);
        }
         [Authorize(Roles = "OrganizationAdmin,SuperAdmin")]
        [HttpPut("{organizationId}/admins")]
        public async Task<ActionResult<OrganizationResponse>> UpdateOrganizationAdmin(int organizationId, Organization updatedOrganization)
        {
          throw new NotImplementedException();
        }
        [Authorize(Roles = "OrganizationAdmin,SuperAdmin")]
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