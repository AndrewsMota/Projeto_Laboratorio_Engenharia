using App.ViewModels;
using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace App.Controllers
{
    public class BioteriosController : Controller
    {
        private readonly IBioteriosService _bioteriosService;

        public BioteriosController(IBioteriosService bioteriosService)
        {
            _bioteriosService = bioteriosService;
        }

        public async Task<IActionResult> Index()
        {
            var bioterios = await _bioteriosService.ListarBioterios();
            return View(bioterios);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Adicionar()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Adicionar(BioterioViewModel bioterioViewModel)
        {
            if (!ModelState.IsValid) return View(bioterioViewModel);

            var bioterio = new Bioterio
            {
                Nome = bioterioViewModel.Nome,
                Email = bioterioViewModel.Email,
                Telefone = bioterioViewModel.Telefone,
                Cnpj = bioterioViewModel.Cnpj,

                EnderecoBioterio = new EnderecoBioterio
                {
                    Logradouro = bioterioViewModel.EnderecoBioterio.Logradouro,
                    Numero = bioterioViewModel.EnderecoBioterio.Numero,
                    Complemento = bioterioViewModel.EnderecoBioterio.Complemento,
                    Cep = bioterioViewModel.EnderecoBioterio.Cep,
                    Bairro = bioterioViewModel.EnderecoBioterio.Bairro,
                    Cidade = bioterioViewModel.EnderecoBioterio.Cidade,
                    Estado = bioterioViewModel.EnderecoBioterio.Estado
                }
            };

            await _bioteriosService.Adicionar(bioterio);

            return RedirectToAction("Index");
        }

        [Route("detalhes-bioterio/{id:guid}")]
        public async Task<IActionResult> Detalhes(Guid id)
        {
            var bioterio = await _bioteriosService.ObterPorIdComEndereco(id);

            if (bioterio == null) return NotFound();

            return View(bioterio);
        }
    }
}
