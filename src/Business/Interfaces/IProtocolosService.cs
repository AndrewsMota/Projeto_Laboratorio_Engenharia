using Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IProtocolosService
    {
        public Task<IList<Protocolo>> ListarProtocolos();
        public Task SalvarProtocoloEmitido(Protocolo protocolo);
        public Task<Protocolo> PopularPesquisador(Protocolo protocolo);
        public Task<IList<Protocolo>> ListarProtocolosComPesquisador();
        public Task<IList<Protocolo>> ListarProtocolosSemParecerista();
        public Task<Protocolo> ObterPorId(Guid id);
        public Task<Protocolo> PopularEspecies(Protocolo protocolo);
    }
}
