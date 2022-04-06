using Microsoft.AspNetCore.Identity;

namespace Maksgogo.Models
{
    public class User : IdentityUser
    {
        public string Email { get; set; } = null!;

    }
}
