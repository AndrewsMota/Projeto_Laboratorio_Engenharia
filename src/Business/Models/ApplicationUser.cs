using Microsoft.AspNetCore.Identity;

namespace Business.Models
{
    public class ApplicationUser : IdentityUser
    {
        public Endereco Endereco { get; set; }
        public UserInfo UserInfo { get; set; }
    }
}
