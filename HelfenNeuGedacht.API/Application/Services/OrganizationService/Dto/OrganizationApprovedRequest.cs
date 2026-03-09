namespace HelfenNeuGedacht.API.Application.Services.OrganizationService.Dto
{
    public class OrganizationApprovedRequest
    {
        public int OrganisationId { get; set; }

        public OrganizationApprovalStatus ApprovalStatus { get; set; } = OrganizationApprovalStatus.Pending;

        public bool IsApproved { get; set; } = false;

        public string? RejectionReason { get; set; }
    }
}
