using Google.Protobuf.WellKnownTypes;
using HelfenNeuGedacht.API.Application.Services.OrganizationService.Dto;

namespace HelfenNeuGedacht.API.Application.Services.OrganizationService
{
    public interface IOrganizationService
    {
        public Task<OrganizationRegistrationResponse> RegisterOrganizationWithAdminAsync(OrganizationRequest organizationRequest, string password);
        public Task<OrganizationResponse> GetOrganizationByIdAsync(int id);
        public Task<OrganizationResponse> UpdateOrganizationByIdAsync(int id, OrganizationRequest updatedOrganization);
        public Task<OrganizationResponse> DeleteOrganizationByIdAsync(int id);

        //TODO: Frontend anbindung erst im Innovationsprojekt 
        public Task<OrganizationApprovedResponse> ApproveOrganization(OrganizationApprovedRequest organizationApprovedRequest, string adminUser);



    }
}
