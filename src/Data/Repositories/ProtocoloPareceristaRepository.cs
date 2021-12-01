using Business.Interfaces;
using Business.Models;
namespace Data.Repositories
{
    public class ProtocoloPareceristaRepository : Repository<ProtocoloParecerista>, IProtocoloPareceristaRepository
    {
        public ProtocoloPareceristaRepository(ApplicationDbContext context) : base(context) { }
    }
}
