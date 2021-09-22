using Business.Models;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IEnderecoRepository
    {
        Task<Endereco> ObterEnderecoPorUserId(string userId);
    }
}