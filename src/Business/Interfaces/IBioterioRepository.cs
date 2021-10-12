using Business.Models;
using System;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IBioterioRepository : IRepository<Bioterio>
    {
        public Task<Bioterio> ObterPorIdComEndereco(Guid id);
    }
}
