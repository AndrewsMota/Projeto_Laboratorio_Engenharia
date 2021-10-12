using App.ViewModels;
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.Controllers
{
    public class SecretariasController : Controller
    {
        private readonly ISecretariasService _secretariasService;

        public SecretariasController(ISecretariasService secretariasService)
        {
            _secretariasService = secretariasService;
        }

        public async Task<IActionResult> Index()
        {
            var secretarias = await _secretariasService.ListarSecretarias();
            return View(secretarias);
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

            var resultado = await _secretariasService.Adicionar(user, senha);

            if (!resultado) return View(userViewModel);

            return RedirectToAction("Index");
        }

        [Route("detalhes-secretaria/{id:guid}")]
        public async Task<IActionResult> Detalhes(string id)
        {
            var usuario = await ObterPorIdComUserInfoEEndereco(id);

            if (usuario == null) return NotFound();

            return View(usuario);
        }

        private async Task<ApplicationUser> ObterPorIdComUserInfoEEndereco(string id)
        {
            return await _secretariasService.ObterPorIdComUserInfoEEndereco(id);
        }
    }
}
