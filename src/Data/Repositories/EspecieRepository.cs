using Business.Interfaces;
using Business.Models;

namespace Data.Repositories
{
    public class EspecieRepository : Repository<Especie>, IEspecieRepository
    {
        public EspecieRepository(ApplicationDbContext context) : base(context) { }
    }
}
