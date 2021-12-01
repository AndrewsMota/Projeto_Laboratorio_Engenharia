using System;

namespace Business.Models
{
    public class Parecer : Entidade
    {
        public Guid ProtocoloId { get; set; }
        public string JustificativaDoParecer { get; set; }
        public Escolha Escolha { get; set; }

        public Parecer()
        {

        }

        public Parecer(Guid protocoloId, string justificativaDoParecer, Escolha escolha)
        {
            ProtocoloId = protocoloId;
            JustificativaDoParecer = justificativaDoParecer;
            Escolha = escolha;
        }
    }
}
