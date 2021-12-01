using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class Protocolo : Entidade
    {
        public string ApplicationUserId { get; set; }
        public string Justificativa { get; set; }
        public string ResumoPt { get; set; }
        public string ResumoEn { get; set; }
        [DataType(DataType.Date)]
        public DateTime DataInicio { get; set; }
        [DataType(DataType.Date)]
        public DateTime DataTermino { get; set; }
        public StatusProtocolo Status { get; set; }

        /* EF Relations */
        public ApplicationUser ApplicationUser { get; set; }
        public IList<ProtocolosEspecies> ProtocolosEspecies { get; set; }

        public Protocolo()
        {

        }

        public Protocolo(string justificativa, string resumoPt, string resumoEn, DateTime dataInicio, DateTime dataTermino, IList<Especie> especies, IList<int> quantidades)
        {
            Justificativa = justificativa;
            ResumoPt = resumoPt;
            ResumoEn = resumoEn;
            DataInicio = dataInicio;
            DataTermino = dataTermino;
            Status = StatusProtocolo.AguardandoEnvioParaParecer;

        }
    }
}
