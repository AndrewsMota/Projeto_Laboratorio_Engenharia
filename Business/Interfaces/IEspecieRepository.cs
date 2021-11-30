using Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IEspecieRepository : IRepository<Especie>
    {
        public Task<IList<Especie>> ObterTodosComBioteriosEEndereco();
    }
}
