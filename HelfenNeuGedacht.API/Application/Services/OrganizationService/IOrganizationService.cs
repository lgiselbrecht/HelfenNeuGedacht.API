using Google.Protobuf.WellKnownTypes;
using HelfenNeuGedacht.API.Application.Services.OrganizationService.Dto;

namespace HelfenNeuGedacht.API.Application.Services.OrganizationService
{
    public interface IOrganizationService
    {
        //Organizations
        public Task<OrganizationResponse> CreateOrganizationAsync(OrganizationRequest eventEntity);
        public Task<OrganizationResponse> GetOrganizationByIdAsync(int id);
        public Task<OrganizationResponse> UpdateOrganizationByIdAsync(int id, OrganizationRequest updatedOrganization);
        public Task<OrganizationResponse> DeleteOrganizationByIdAsync(int id);



        //Admins, denke das muss dann hier raus und in User rein buw user endität und dann hier nur die relation zwischen user und organization, frog mi it
        public Task<IEnumerable<OrganizationResponse>> GetOrganizationAdminsAsync(int id);
        public Task<OrganizationResponse> CreateOrganizationAdminAsync(int organizationId, string adminUserId);
        public Task<OrganizationResponse> UpdateOrganizationAdminAsync(Organization organization, Organization updatedOrganization);
        public Task<OrganizationResponse> DeleteOrganizationAdminAsync(int organizationId, string adminUserId);




    }
}
