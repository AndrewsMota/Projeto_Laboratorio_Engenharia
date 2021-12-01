using System;

namespace Business.Models
{
    public class ProtocoloParecerista : Entidade
    {
        public Guid ProtocoloId { get; set; }
        public string PareceristaId { get; set; }

        /* EF Relations */
        public Protocolo Protocolo { get; set; }
        public ApplicationUser Parecerista { get; set; }
    }
}
