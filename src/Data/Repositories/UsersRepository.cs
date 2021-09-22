using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserInfoRepository _userInfoRepository;
        private readonly IEnderecoRepository _enderecoRepository ;
        private readonly IHttpContextAccessor _httpContextAccessor;

        protected readonly ApplicationDbContext Database;
        protected readonly DbSet<ApplicationUser> DbSet;

        public UsersRepository(ApplicationDbContext context,
                               UserManager<ApplicationUser> userManager,
                               SignInManager<ApplicationUser> signInManager,
                               IUserInfoRepository userInfoRepository,
                               IHttpContextAccessor httpContextAccessor,
                               IEnderecoRepository enderecoRepository)
        {
            Database = context;
            DbSet = Database.Set<ApplicationUser>();
            _userManager = userManager;
            _signInManager = signInManager;
            _userInfoRepository = userInfoRepository;
            _httpContextAccessor = httpContextAccessor;
            _enderecoRepository = enderecoRepository;
        }

        public async Task<bool> Adicionar(ApplicationUser user, string senha, string role = null)
        {
            var resultado = await _userManager.CreateAsync(user, senha);
            if (resultado.Succeeded)
            {
                if (role != null)
                {
                    await _userManager.AddToRoleAsync(user, role);
                    if(role == "pesquisador")
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                    }
                }
                return true;
            }
            return false;
        }

        public async Task<IList<ApplicationUser>> ObterUsuariosDaRoleComUserInfo(string role)
        {
            var usuarios = await _userManager.GetUsersInRoleAsync(role);

            foreach (var usuario in usuarios)
            {
                usuario.UserInfo = await _userInfoRepository.ObterUserInfoPorUserId(usuario.Id);
            }

            return usuarios;
        }

        public async Task<ApplicationUser> ObterPresidenteComUserInfo()
        {
            var presidentes = await ObterUsuariosDaRoleComUserInfo("presidente");
            if (presidentes.Count == 1)
            {
                return presidentes[0];
            }
            return null;
        }

        public bool ValidarAcesso(string roleName)
        {
            return _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated &&
                   _httpContextAccessor.HttpContext.User.IsInRole(roleName);
        }

        public async Task<ApplicationUser> ObterPorId(string id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<ApplicationUser> ObterPorIdComUserInfo(string id)
        {
            var usuario = await ObterPorId(id);
            usuario.UserInfo = await _userInfoRepository.ObterUserInfoPorUserId(usuario.Id);
            return usuario;
        }

        public async Task<ApplicationUser> ObterPorIdComUserInfoEEndereco(string id)
        {
            var usuario = await ObterPorIdComUserInfo(id);
            usuario.Endereco = await _enderecoRepository.ObterEnderecoPorUserId(usuario.Id);
            return usuario;
        }

        public async Task<bool> AlterarRole(ApplicationUser user, string roleAntiga, string roleNova)
        {
            await _userManager.AddToRoleAsync(user, roleNova);
            await _userManager.RemoveFromRoleAsync(user, roleAntiga);

            return true;
        }
    }
}
