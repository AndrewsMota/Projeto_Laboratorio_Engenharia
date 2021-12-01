using Business.Interfaces;
using Business.Models;
namespace Data.Repositories
{
    public class ProtocolosEspeciesRepository : Repository<ProtocolosEspecies>, IProtocolosEspeciesRepository
    {
        public ProtocolosEspeciesRepository(ApplicationDbContext context) : base(context) { }
    }
}
