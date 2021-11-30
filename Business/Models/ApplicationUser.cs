using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Business.Models
{
    public class ApplicationUser : IdentityUser
    {
        public Endereco Endereco { get; set; }
        public UserInfo UserInfo { get; set; }

        /* EF Relations */
        public IEnumerable<Protocolo> Protocolos { get; set; }
    }
}
