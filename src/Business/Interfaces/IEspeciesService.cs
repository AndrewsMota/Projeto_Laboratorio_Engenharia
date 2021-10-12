using Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IEspeciesService
    {
        public Task<IList<Especie>> ListarEspecies();
        public Task Adicionar(Especie especie);
        public Task<IEnumerable<Bioterio>> ObterBioterios();
        public Task<IList<Especie>> ListarEspeciesComBioterio();
        public Task<Especie> ObterPorId(Guid id);
    }
}
