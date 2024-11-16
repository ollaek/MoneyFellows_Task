using Microsoft.AspNetCore.Identity;

namespace MF_Task.Infrastructure.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}