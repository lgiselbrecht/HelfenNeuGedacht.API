
using HelfenNeuGedacht.API.Domain.Entities;
namespace HelfenNeuGedacht.API.Application.Services.OrganizationService.Dto
{
    public class OrganizationApprovedResponse
    {
        public int Id { get; set; }
            
        public string Name { get; set; } = string.Empty;

        public string RegistrationNumber { get; set; } = string.Empty; //Vereinsregister
        public string? Type { get; set; } 

        public OrganizationApprovalStatus ApprovalStatus { get; set; } = OrganizationApprovalStatus.Pending;

        public bool IsApproved { get; set; } = false;

        public bool IsActive { get; set; } = true;

        public DateTime? ApprovedAt { get; set; }

        public string? ApprovedBy { get; set; }

        public string? RejectionReason { get; set; }


    }





}
