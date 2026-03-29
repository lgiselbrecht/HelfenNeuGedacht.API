using HelfenNeuGedacht.API.Application.Services.OrganizationService;
using HelfenNeuGedacht.API.Application.Services.OrganizationService.Dto;
using HelfenNeuGedacht.API.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
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

        
        [HttpGet("{id}")]
        [Authorize(Roles = $"{Roles.OrganizationAdmin},{Roles.OrganizationUser}")]
        public async Task<ActionResult<Organization>> GetOrganizationById(int id)
        {
            var organization = await _organizationService.GetOrganizationByIdAsync(id);

            if (organization == null)
                return NotFound($"Keine Organization mit der ID {id} gefunden.");

            return Ok(organization);
        }
        [Authorize(Roles = $"{Roles.OrganizationAdmin}")]
        [HttpPut("{id}")]
        public async Task<ActionResult<OrganizationResponse>> UpdateOrganizationById(int id, OrganizationRequest updatedOrganization)
        {
            if (updatedOrganization == null)
                return BadRequest("UpdatedOrganization darf nicht null sein.");


            var result = await _organizationService.UpdateOrganizationByIdAsync(id, updatedOrganization);

            return Ok(result);
        }
        //für später: Innovationsprojekt
        [Authorize(Roles = $"{Roles.SuperAdmin}")]
        [HttpPut("{id}/approve")]
        public async Task<ActionResult<OrganizationResponse>> ApproveOrganizationById(OrganizationApprovedRequest approveRequest, string adminuser)
        {
            var result = await _organizationService.ApproveOrganization(approveRequest, adminuser);
            if (result == null)
                return NotFound($"Keine Organization mit der ID {approveRequest.OrganisationId} gefunden.");
            return Ok(result);
        }

        [Authorize(Roles = $"{Roles.OrganizationAdmin}")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Organization>> DeleteOrganizationById(int id)
        {
            var existingOrganization = await _organizationService.GetOrganizationByIdAsync(id);

            if (existingOrganization == null)
                return NotFound($"Keine Organization mit der ID {id} gefunden.");

            var deletedOrganization = await _organizationService.DeleteOrganizationByIdAsync(id);

            return Ok(deletedOrganization);
        }
        
    }
}