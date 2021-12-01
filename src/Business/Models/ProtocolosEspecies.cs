using System;

namespace Business.Models
{
    public class ProtocolosEspecies : Entidade
    {
        public Guid ProtocoloId { get; set; }
        public Guid EspecieId { get; set; }
        public int Quantidade { get; set; }

        /* EF Relations */
        public Protocolo Protocolo { get; set; }
        public Especie Especie { get; set; }
    }
}
