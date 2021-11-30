using Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IPesquisadoresService
    {
        Task<bool> Adicionar(ApplicationUser user, string senha);
        public Task<IList<ApplicationUser>> ListarPesquisadores();
        public Task<bool> PromoverPesquisador(string userId);
        public Task<ApplicationUser> ObterPorId(string userId);
        public Task<ApplicationUser> ObterPorIdComUserInfo(string userId);
        public Task<bool> RebaixarPresidente();
        public Task<ApplicationUser> ObterPorIdComUserInfoEEndereco(string userId);
    }
}