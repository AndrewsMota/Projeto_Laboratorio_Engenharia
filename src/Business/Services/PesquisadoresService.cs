using Business.Interfaces;
using Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Services
{
    public class PesquisadoresService : IPesquisadoresService
    {
        private readonly IUsersRepository _usersRepository;

        public PesquisadoresService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<bool> Adicionar(ApplicationUser user, string senha)
        {
            var resultado = await _usersRepository.Adicionar(user, senha, "Pesquisador");
            return resultado;
        }

        public async Task<IList<ApplicationUser>> ListarPesquisadores()
        {
            var pesquisadores = await _usersRepository.ObterUsuariosDaRoleComUserInfo("Pesquisador");
            var presidente = await _usersRepository.ObterPresidenteComUserInfo();

            if (presidente == null)
            {
                presidente = new ApplicationUser
                {
                    UserInfo = new UserInfo
                    {
                        NomeCompleto = "Nenhum presidente selecionado"
                    }
                };
            }

            pesquisadores.Remove(presidente);
            pesquisadores.Insert(0, presidente);

            return pesquisadores;
        }

        public async Task<bool> PromoverPesquisador(string userId)
        {
            var user = await _usersRepository.ObterPorId(userId);

            await RebaixarPresidente();

            var result = await _usersRepository.AlterarRole(user, "Pesquisador", "Presidente");

            return result;
        }

        public async Task<bool> RebaixarPresidente()
        {
            var presidenteAtual = await _usersRepository.ObterPresidenteComUserInfo();
            var resultado = true;

            if (presidenteAtual != null)
            {
                resultado = await _usersRepository.AlterarRole(presidenteAtual, "Presidente", "Pesquisador");
            }

            return resultado;
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
