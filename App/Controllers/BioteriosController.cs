using App.ViewModels;
using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Controllers
{
    public class BioteriosController : Controller
    {
        private readonly IBioteriosService _bioteriosService;
        private readonly IEspeciesService _especiesService;
        private readonly IMapper _mapper;

        public BioteriosController(IBioteriosService bioteriosService, IMapper mapper, IEspeciesService especiesService)
        {
            _bioteriosService = bioteriosService;
            _mapper = mapper;
            _especiesService = especiesService;
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

                Endereco = new EnderecoBioterio
                {
                    Logradouro = bioterioViewModel.Endereco.Logradouro,
                    Numero = bioterioViewModel.Endereco.Numero,
                    Complemento = bioterioViewModel.Endereco.Complemento,
                    Cep = bioterioViewModel.Endereco.Cep,
                    Bairro = bioterioViewModel.Endereco.Bairro,
                    Cidade = bioterioViewModel.Endereco.Cidade,
                    Estado = bioterioViewModel.Endereco.Estado
                }
            };

            await _bioteriosService.Adicionar(bioterio);

            return RedirectToAction("Index");
        }

        [Route("detalhes-bioterio/{id:guid}")]
        public async Task<IActionResult> Detalhes(Guid id)
        {
            var bioterio = _mapper.Map<BioterioViewModel>(await _bioteriosService.ObterPorIdComEndereco(id));
            var especies = _mapper.Map<IList<EspecieViewModel>>(await _especiesService.ListarEspeciesComBioterio());
            var especieColocada = new List<string>();

            foreach (var especie in especies)
            {
                if (especie.Bioterio.Id == bioterio.Id && !especieColocada.Contains(especie.Nome))
                {
                    if (bioterio.Especies == null) bioterio.Especies = new List<EspecieViewModel>();
                    bioterio.Especies.Add(especie);
                    especieColocada.Add(especie.Nome);
                }
            }

            if (bioterio == null) return NotFound();

            return View(bioterio);
        }
    }
}
