using Business.Interfaces;
using Business.Models;
namespace Data.Repositories
{
    public class ProtocoloRepository : Repository<Protocolo>, IProtocoloRepository
    {
        public ProtocoloRepository(ApplicationDbContext context) : base(context) { }
    }
}
