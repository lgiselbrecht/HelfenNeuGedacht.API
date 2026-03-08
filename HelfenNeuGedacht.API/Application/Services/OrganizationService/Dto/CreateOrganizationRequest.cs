namespace HelfenNeuGedacht.API.Application.Services.OrganizationService.Dto
{
    public class CreateOrganizationRequest
    {
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string RegistrationNumber { get; set; } = string.Empty; //Vereinsregister, etc.
        public string? Type { get; set; } // z.B. Verein, Gemeinde, Feuerwehr

        public string? Website { get; set; }

        public string? Street { get; set; }

        public string? PostalCode { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? Country { get; set; }

        public string ContactEmail { get; set; } = string.Empty;

        public string? ContactPhone { get; set; }

        public string? ContactPersonName { get; set; }

        public string? ContactPersonRole { get; set; }

}
}
