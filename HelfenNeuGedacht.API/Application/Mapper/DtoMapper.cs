
﻿using HelfenNeuGedacht.API.Application.Services.ShiftServices.Dto;
using HelfenNeuGedacht.API.Domain.Entities;

﻿using System.Security.Cryptography.X509Certificates;
using HelfenNeuGedacht.API.Application.Services.OrganizationService.Dto;
using static Google.Protobuf.Compiler.CodeGeneratorResponse.Types;

namespace HelfenNeuGedacht.API.Application.Mapper
{
    public class DtoMapper
    {

        public ShiftResponse ToShiftResponse(Shift shift)
        {
            return new ShiftResponse()
            {
                Id = shift.Id,
                Name = shift.Name,
                Description = shift.Description,
                Requirements = shift.Requirements,
                AgeRestriction = shift.AgeRestriction,
                Points = shift.Points,
            };
        }

        public OrganizationResponse ToOrganizationResponse(Organization organization)
        {
            return new OrganizationResponse()
            {
                Id = organization.Id,
                Name = organization.Name,
                Description = organization.Description,
                RegistrationNumber = organization.RegistrationNumber,
                Type = organization.Type,
                Website = organization.Website,
                Street = organization.Street,
                PostalCode = organization.PostalCode,
                City = organization.City,
                State = organization.State,
                Country = organization.Country,
                ContactEmail = organization.ContactEmail,
                ContactPersonName  = organization.ContactPersonName,
                ContactPhone = organization.ContactPhone,
                ContactPersonRole = organization.ContactPersonRole
                                            


    };



        }

        public OrganizationApprovedResponse ToOrganizationApprovedResponse(Organization organization)
        {
            return new OrganizationApprovedResponse()
            {
                Id = organization.Id,
                Name = organization.Name,
                RegistrationNumber = organization.RegistrationNumber,
                Type = organization.Type,
                ApprovalStatus = organization.ApprovalStatus,
                IsApproved = organization.IsApproved,
                IsActive = organization.IsActive,
                ApprovedAt = organization.ApprovedAt,
                ApprovedBy = organization.ApprovedBy,
                RejectionReason = organization.RejectionReason
            };
        }


    }
}
