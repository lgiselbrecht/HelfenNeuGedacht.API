using HelfenNeuGedacht.API.Application.Mapper;
using HelfenNeuGedacht.API.Application.Repositories;
using HelfenNeuGedacht.API.Application.Services.Auth.AuthService;
using HelfenNeuGedacht.API.Application.Services.OrganizationService.Dto;
using HelfenNeuGedacht.API.Domain.Constants;
using HelfenNeuGedacht.API.Domain.Entities;
using HelfenNeuGedacht.API.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using ZstdSharp.Unsafe;
using static Google.Protobuf.Compiler.CodeGeneratorResponse.Types;

namespace HelfenNeuGedacht.API.Application.Services.OrganizationService
{
    public class OrganizationService : IOrganizationService
    {
        private IOrganizationRepository _organizationRepositories;
        private DtoMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;

        public OrganizationService(
            IOrganizationRepository organizationRepositorie, 
            DtoMapper mapper,
            UserManager<ApplicationUser> userManager,
            ITokenService tokenService)
        {
            _organizationRepositories = organizationRepositorie;
            _mapper = mapper;
            _userManager = userManager;
            _tokenService = tokenService;
        }

      

        public async Task<OrganizationRegistrationResponse> RegisterOrganizationWithAdminAsync(OrganizationRequest organizationRequest, string password)
        {
            try
            {
                // 1. Check if email already exists
                var existingUser = await _userManager.FindByEmailAsync(organizationRequest.ContactEmail);
                if (existingUser != null)
                {
                    return new OrganizationRegistrationResponse
                    {
                        Success = false,
                        Message = "Ein Benutzer mit dieser E-Mail-Adresse existiert bereits."
                    };
                }

                // 2. Create Organization
                var organization = new Organization
                {
                    Name = organizationRequest.Name,
                    Description = organizationRequest.Description,
                    Type = organizationRequest.Type,
                    RegistrationNumber = organizationRequest.RegistrationNumber,
                    Website = organizationRequest.Website,
                    Street = organizationRequest.Street,
                    PostalCode = organizationRequest.PostalCode,
                    City = organizationRequest.City,
                    State = organizationRequest.State,
                    Country = organizationRequest.Country,
                    ContactEmail = organizationRequest.ContactEmail,
                    ContactPhone = organizationRequest.ContactPhone,
                    ContactPersonName = organizationRequest.ContactPersonName,
                    ContactPersonRole = organizationRequest.ContactPersonRole
                };

                var createdOrganization = await _organizationRepositories.AddAsync(organization);

                // 3. Create Admin User
                var adminUser = new ApplicationUser
                {
                    UserName = organizationRequest.ContactEmail,
                    Email = organizationRequest.ContactEmail,
                    OrganizationId = createdOrganization.Id,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                var createUserResult = await _userManager.CreateAsync(adminUser, password);

                if (!createUserResult.Succeeded)
                {
                    // Rollback: Delete organization if user creation fails
                    await _organizationRepositories.DeleteAsync(createdOrganization);
                    
                    var errors = string.Join(", ", createUserResult.Errors.Select(e => e.Description));
                    return new OrganizationRegistrationResponse
                    {
                        Success = false,
                        Message = $"Fehler beim Erstellen des Benutzers: {errors}"
                    };
                }

                // 4. Assign OrganizationAdmin role
                await _userManager.AddToRoleAsync(adminUser, Roles.OrganizationAdmin);

                // 5. Generate JWT Token
                var tokenDto = await _tokenService.CreateTokenAsync(adminUser);

                // 6. Return success response with token
                return new OrganizationRegistrationResponse
                {
                    Success = true,
                    Message = "Organisation und Administrator erfolgreich registriert!",
                    OrganizationId = createdOrganization.Id,
                    UserId = adminUser.Id,
                    Token = tokenDto.token,
                    TokenExpiration = tokenDto.expiration
                };
            }
            catch (Exception ex)
            {
                return new OrganizationRegistrationResponse
                {
                    Success = false,
                    Message = $"Ein Fehler ist aufgetreten: {ex.Message}"
                };
            }
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


       

        public async Task<OrganizationResponse> DeleteOrganizationByIdAsync(int id)
        {
            var organization = await _organizationRepositories.FindByIdAsync(id);
            await _organizationRepositories.DeleteAsync(organization);

            return _mapper.ToOrganizationResponse(organization);
        }

       
        public async Task<OrganizationResponse> GetOrganizationByIdAsync(int id)
        {
            var x = await _organizationRepositories.FindByIdAsync(id);
            if (x == null)
                return null;

            return _mapper.ToOrganizationResponse(x);
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

        public async Task<OrganizationApprovedResponse> ApproveOrganization(OrganizationApprovedRequest organizationApprovedRequest, string adminUser)
        {
            var organization = await _organizationRepositories.FindByIdAsync(organizationApprovedRequest.OrganisationId);

            if (organization == null) {
                return null;
            }

                organization.ApprovedBy = adminUser;
            organization.ApprovalStatus = organizationApprovedRequest.ApprovalStatus;
            organization.IsApproved = organizationApprovedRequest.IsApproved;
            organization.RejectionReason = organizationApprovedRequest.RejectionReason;
            organization.ApprovedAt = DateTime.UtcNow;

            var result = await _organizationRepositories.UpdateAsync(organization);
            return _mapper.ToOrganizationApprovedResponse(result);


        }


        public Task<IEnumerable<OrganizationResponse>> GetOrganizationAdminsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<OrganizationResponse> DeleteOrganizationAdminAsync(int organizationId, string adminUserId)
        {

            throw new NotImplementedException();


        }

        public Task<OrganizationResponse> UpdateOrganizationAdminAsync(Organization organization, Organization updatedOrganization)
        {
            throw new NotImplementedException();
        }

        public Task<OrganizationResponse> CreateOrganizationAdminAsync(int organizationId, string adminUserId)
        {
            throw new NotImplementedException();
        }

     
    }
}

