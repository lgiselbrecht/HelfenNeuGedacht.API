using HelfenNeuGedacht.API.Application.Mapper;
using HelfenNeuGedacht.API.Application.Repositories;
using HelfenNeuGedacht.API.Application.Services.OrganizationService.Dto;
using HelfenNeuGedacht.API.Domain.Entities;
using HelfenNeuGedacht.API.Infrastructure.Repositories;
using static Google.Protobuf.Compiler.CodeGeneratorResponse.Types;

namespace HelfenNeuGedacht.API.Application.Services.OrganizationService
{
    public class OrganizationService : IOrganizationService
    {
        private IOrganizationRepository _organizationRepositories;
        private DtoMapper _mapper;

        public OrganizationService(IOrganizationRepository organizationRepositorie, DtoMapper mapper)
        {
            _organizationRepositories = organizationRepositorie;
            _mapper = mapper;
        }

        public Task<OrganizationResponse> CreateOrganizationAdminAsync(int organizationId, string adminUserId)
        {
            throw new NotImplementedException();
        }

        public async Task<OrganizationResponse> CreateOrganizationAsync(OrganizationRequest eventEntity)
        {
            var organisatzion = new Organization()
            {
                Name = eventEntity.Name,
                Description = eventEntity.Description,
                Type = eventEntity.Type,
                RegistrationNumber = eventEntity.RegistrationNumber,
                Website = eventEntity.Website,
                Street = eventEntity.Street,
                PostalCode = eventEntity.PostalCode,
                City = eventEntity.City,
                State = eventEntity.State,
                Country = eventEntity.Country,
                ContactEmail = eventEntity.ContactEmail,
                ContactPhone = eventEntity.ContactPhone,
                ContactPersonName = eventEntity.ContactPersonName,
                ContactPersonRole = eventEntity.ContactPersonRole


            };



            var newOrganization = await _organizationRepositories.AddAsync(organisatzion);


            return _mapper.ToOrganizationResponse(newOrganization);

        }

        public async Task<OrganizationResponse> DeleteOrganizationAdminAsync(int organizationId, string adminUserId)
        {

            throw new NotImplementedException();


        }

        public async Task<OrganizationResponse> DeleteOrganizationByIdAsync(int id)
        {
            var organization = await _organizationRepositories.FindByIdAsync(id);
            await _organizationRepositories.DeleteAsync(organization);

            return _mapper.ToOrganizationResponse(organization);
        }

        public Task<IEnumerable<OrganizationResponse>> GetOrganizationAdminsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<OrganizationResponse> GetOrganizationByIdAsync(int id)
        {
            var x = await _organizationRepositories.FindByIdAsync(id);
            if (x == null)
                return null;

            return _mapper.ToOrganizationResponse(x);
        }

        public Task<OrganizationResponse> UpdateOrganizationAdminAsync(Organization organization, Organization updatedOrganization)
        {
            throw new NotImplementedException();
        }

        public async Task<OrganizationResponse> UpdateOrganizationByIdAsync(int id, OrganizationRequest updatedOrganization)
        {
            var organization = _organizationRepositories.FindByIdAsync(id).Result;


            organization.Name = updatedOrganization.Name;
            organization.Description = updatedOrganization.Description;
            organization.Type = updatedOrganization.Type;
            organization.RegistrationNumber = updatedOrganization.RegistrationNumber;
            organization.Website = updatedOrganization.Website;
            organization.Street = updatedOrganization.Street;
            organization.PostalCode = updatedOrganization.PostalCode;
            organization.City = updatedOrganization.City;
            organization.State = updatedOrganization.State;
            organization.Country = updatedOrganization.Country;
            organization.ContactEmail = updatedOrganization.ContactEmail;
            organization.ContactPhone = updatedOrganization.ContactPhone;
            organization.ContactPersonName = updatedOrganization.ContactPersonName;
            organization.ContactPersonRole = updatedOrganization.ContactPersonRole;
            organization.UpdatedAt = DateTime.UtcNow;

            var result = await _organizationRepositories.UpdateAsync(organization);

            return _mapper.ToOrganizationResponse(result);


        }
    }
}

