using Google.Protobuf.WellKnownTypes;
using HelfenNeuGedacht.API.Application.Services.OrganizationService.Dto;

namespace HelfenNeuGedacht.API.Application.Services.OrganizationService
{
    public interface IOrganizationService
    {
        //Organizations
        public Task<OrganizationResponse> CreateOrganizationAsync(CreateOrganizationRequest eventEntity);
        public Task<Organization> GetOrganizationByIdAsync(int id);
        public Task<Organization> UpdateOrganizationByIdAsync(int id, Organization updatedOrganization);
        public Task<Organization> DeleteOrganizationByIdAsync(int id);



        //Admins, denke das muss dann hier raus und in User rein buw user endität und dann hier nur die relation zwischen user und organization, frog mi it
        public Task<IEnumerable<Organization>> GetOrganizationAdminsAsync(int id);
        public Task<Organization> CreateOrganizationAdminAsync(int organizationId, string adminUserId);
        public Task<Organization> UpdateOrganizationAdminAsync(Organization organization, Organization updatedOrganization);
        public Task<Organization> DeleteOrganizationAdminAsync(int organizationId, string adminUserId);




    }
}
