using Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IUsersRepository
    {
        public Task<bool> Adicionar(ApplicationUser user, string senha, string role = null);
        public Task<IList<ApplicationUser>> ObterUsuariosDaRoleComUserInfo(string role);
        public Task<ApplicationUser> ObterPresidenteComUserInfo();
        public bool ValidarAcesso(string roleName);
        public Task<ApplicationUser> ObterPorId(string id);
        public Task<bool> AlterarRole(ApplicationUser user, string roleAntiga, string roleNova);
        public Task<ApplicationUser> ObterPorIdComUserInfo(string id);
        public Task<ApplicationUser> ObterPorIdComUserInfoEEndereco(string id);
        public Task<ApplicationUser> ObterUsuarioLogado();
    }
}