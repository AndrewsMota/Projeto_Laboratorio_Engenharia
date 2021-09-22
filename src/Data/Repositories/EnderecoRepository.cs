using Business.Interfaces;
using Business.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {

        public EnderecoRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Endereco> ObterEnderecoPorUserId(string userId)
        {
            return await Database.Enderecos.AsNoTracking()
                .FirstOrDefaultAsync(Endereco => Endereco.UserId == userId);
        }
    }
}
