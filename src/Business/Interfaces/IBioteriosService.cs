using Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IBioteriosService
    {
        public Task<IList<Bioterio>> ListarBioterios();
        public Task Adicionar(Bioterio bioterio);
        public Task<Bioterio> ObterPorIdComEndereco(Guid id);
    }
}
