using System;
using System.Collections.Generic;

namespace Business.Models
{
    public class Especie : Entidade
    {
        public Guid BioterioId { get; set; }
        public string Nome { get; set; }

        /* EF Relations */
        public Bioterio Bioterio { get; set; }
        public IEnumerable<ProtocolosEspecies> ProtocolosEspecies { get; set; }
    }
}
