using System.ComponentModel.DataAnnotations;

namespace HelfenNeuGedacht.API.Application.Services.OrganizationService.Dto
{
    public class OrganizationRegistrationRequest : OrganizationRequest
    {
        [Required(ErrorMessage = "Passwort ist erforderlich")]
        [MinLength(6, ErrorMessage = "Passwort muss mindestens 6 Zeichen lang sein")]
        public string Password { get; set; } = string.Empty;
    }
}
