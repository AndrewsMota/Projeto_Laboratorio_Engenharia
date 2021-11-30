using App.ViewModels;
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.Controllers
{
    public class PesquisadoresController : Controller
    {
        private readonly IPesquisadoresService _pesquisadoresService;

        public PesquisadoresController(IPesquisadoresService pesquisadoresService)
        {
            _pesquisadoresService = pesquisadoresService;
        }

        public async Task<IActionResult> Index()
        {
            var pesquisadores = await _pesquisadoresService.ListarPesquisadores();
            return View(pesquisadores);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Adicionar()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Adicionar(ApplicationUserViewModel userViewModel)
        {
            if (!ModelState.IsValid) return View(userViewModel);

            var senha = userViewModel.Password;
            var user = new ApplicationUser
            {
                UserName = userViewModel.Email,
                Email = userViewModel.Email,
                EmailConfirmed = true,
                PhoneNumber = userViewModel.PhoneNumber,
                PhoneNumberConfirmed = true,
                Endereco = new Endereco
                {
                    Logradouro = userViewModel.Endereco.Logradouro,
                    Numero = userViewModel.Endereco.Numero,
                    Complemento = userViewModel.Endereco.Complemento,
                    Cep = userViewModel.Endereco.Cep,
                    Bairro = userViewModel.Endereco.Bairro,
                    Cidade = userViewModel.Endereco.Cidade,
                    Estado = userViewModel.Endereco.Estado
                },
                UserInfo = new UserInfo
                {
                    NomeCompleto = userViewModel.UserInfo.NomeCompleto,
                    Cpf = userViewModel.UserInfo.Cpf,
                    DataNascimento = userViewModel.UserInfo.DataNascimento,
                    Sexo = userViewModel.UserInfo.Sexo
                }
            };

            var resultado = await _pesquisadoresService.Adicionar(user, senha);

            if (!resultado) return View(userViewModel);

            return RedirectToAction("Index");
        }

        [Route("detalhes-pesquisador/{id:guid}")]
        public async Task<IActionResult> Detalhes(string id)
        {
            var usuario = await ObterPorIdComUserInfoEEndereco(id);

            if (usuario == null) return NotFound();

            return View(usuario);
        }

        [Authorize(Roles = "Admin")]
        [Route("promover/{id:guid}")]
        public async Task<IActionResult> Promover(string id)
        {
            var usuario = await ObterPorIdComUserInfo(id);
            return View(usuario);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("promover/{id:guid}")]
        public async Task<IActionResult> PromoverConfirmado(string id)
        {
            await _pesquisadoresService.PromoverPesquisador(id);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [Route("rebaixar/{id:guid}")]
        public async Task<IActionResult> Rebaixar(string id)
        {
            var usuario = await ObterPorIdComUserInfo(id);
            return View(usuario);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("rebaixar/{id:guid}")]
        public async Task<IActionResult> RebaixarConfirmado(string id)
        {
            await _pesquisadoresService.RebaixarPresidente();
            return RedirectToAction("Index");
        }

        private async Task<ApplicationUser> ObterPorIdComUserInfo(string id)
        {
            return await _pesquisadoresService.ObterPorIdComUserInfo(id);
        }

        private async Task<ApplicationUser> ObterPorIdComUserInfoEEndereco(string id)
        {
            return await _pesquisadoresService.ObterPorIdComUserInfoEEndereco(id);
        }
    }
}
