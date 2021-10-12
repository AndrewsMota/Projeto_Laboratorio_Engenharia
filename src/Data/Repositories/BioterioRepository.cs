using Business.Interfaces;
using Business.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class BioterioRepository : Repository<Bioterio>, IBioterioRepository
    {
        public BioterioRepository(ApplicationDbContext context) : base(context) { }

        private async Task<EnderecoBioterio> ObterEnderecoPorBioterioId(Guid bioterioId)
        {
            return await Database.EnderecosBioterios.AsNoTracking()
                .FirstOrDefaultAsync(Endereco => Endereco.BioterioId == bioterioId);
        }

        public async Task<Bioterio> ObterPorIdComEndereco(Guid id)
        {
            var bioterio = await ObterPorId(id);
            bioterio.Endereco = await ObterEnderecoPorBioterioId(bioterio.Id);
            return bioterio;
        }
    }
}
