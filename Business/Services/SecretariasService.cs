using Business.Interfaces;
using Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Services
{
    public class SecretariasService : ISecretariasService
    {
        private readonly IUsersRepository _usersRepository;

        public SecretariasService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<bool> Adicionar(ApplicationUser user, string senha)
        {
            var resultado = await _usersRepository.Adicionar(user, senha, "Secretária");
            return resultado;
        }

        public async Task<IList<ApplicationUser>> ListarSecretarias()
        {
            var secretarias = await _usersRepository.ObterUsuariosDaRoleComUserInfo("Secretária");
            return secretarias;
        }

        public async Task<ApplicationUser> ObterPorId(string userId)
        {
            var user = await _usersRepository.ObterPorId(userId);

            return user;
        }

        public async Task<ApplicationUser> ObterPorIdComUserInfo(string userId)
        {
            var user = await _usersRepository.ObterPorIdComUserInfo(userId);

            return user;
        }

        public async Task<ApplicationUser> ObterPorIdComUserInfoEEndereco(string userId)
        {
            var user = await _usersRepository.ObterPorIdComUserInfoEEndereco(userId);

            return user;
        }
    }
}
