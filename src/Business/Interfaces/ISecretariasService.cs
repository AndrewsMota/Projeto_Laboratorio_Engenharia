using Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface ISecretariasService
    {
        Task<bool> Adicionar(ApplicationUser user, string senha);
        Task<IList<ApplicationUser>> ListarSecretarias();
        Task<ApplicationUser> ObterPorId(string userId);
        Task<ApplicationUser> ObterPorIdComUserInfo(string userId);
        Task<ApplicationUser> ObterPorIdComUserInfoEEndereco(string userId);
    }
}