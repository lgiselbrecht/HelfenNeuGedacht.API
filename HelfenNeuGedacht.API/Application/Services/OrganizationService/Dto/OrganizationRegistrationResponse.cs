namespace HelfenNeuGedacht.API.Application.Services.OrganizationService.Dto
{
    public class OrganizationRegistrationResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public int OrganizationId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public DateTime TokenExpiration { get; set; }
    }
}
