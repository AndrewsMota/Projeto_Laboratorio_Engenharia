using Business.Interfaces;
using Business.Models;
namespace Data.Repositories
{
    public class ParecerRepository : Repository<Parecer>, IParecerRepository
    {
        public ParecerRepository(ApplicationDbContext context) : base(context) { }
    }
}
