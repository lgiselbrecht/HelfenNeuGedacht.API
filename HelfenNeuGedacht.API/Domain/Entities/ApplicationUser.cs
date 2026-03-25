using Microsoft.AspNetCore.Identity;

namespace HelfenNeuGedacht.API.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public int? OrganizationId { get; set; }
        
        public Organization? Organization { get; set; }
    }

}
