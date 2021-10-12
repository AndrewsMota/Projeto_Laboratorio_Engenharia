using App.ViewModels;
using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Controllers
{
    public class EspeciesController : Controller
    {
        private readonly IEspeciesService _especiesService;
        private readonly IMapper _mapper;

        public EspeciesController(IEspeciesService especiesService, IMapper mapper)
        {
            _especiesService = especiesService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var especies = await _especiesService.ListarEspecies();
            return View(especies);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Adicionar()
        {
            var especieViewModel = await PopularBioterios(new EspecieViewModel());
            return View(especieViewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Adicionar(EspecieViewModel especieViewModel)
        {
            if (!ModelState.IsValid) return View(especieViewModel);

            var especie = _mapper.Map<Especie>(especieViewModel);

            await _especiesService.Adicionar(especie);

            return RedirectToAction("Index");
        }

        private async Task<EspecieViewModel> PopularBioterios(EspecieViewModel especie)
        {
            especie.Bioterios = _mapper.Map<IEnumerable<BioterioViewModel>>(await _especiesService.ObterBioterios());
            return especie;
        }
    }
}
