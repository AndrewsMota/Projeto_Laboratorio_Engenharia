using Business.Interfaces;
using Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class EspecieRepository : Repository<Especie>, IEspecieRepository
    {
        private readonly IBioterioRepository _bioterioRepository;

        public EspecieRepository(ApplicationDbContext context, IBioterioRepository bioterioRepository) : base(context)
        {
            _bioterioRepository = bioterioRepository;
        }

        public async Task<IList<Especie>> ObterTodosComBioteriosEEndereco()
        {
            var especies = await ObterTodos();
            foreach(var especie in especies)
            {
                especie.Bioterio = await _bioterioRepository.ObterPorIdComEndereco(especie.BioterioId);
            }
            return especies;
        }
    }
}
