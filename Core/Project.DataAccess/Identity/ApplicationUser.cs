using Microsoft.AspNetCore.Identity;

namespace Domain.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string Name { get; set; }
    }
}