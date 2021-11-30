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
            var especies = await _especiesService.ListarEspeciesComBioterio();
            var especiesVM = _mapper.Map<IEnumerable<EspecieViewModel>>(especies);

            IList<EspecieViewModel> especiesAgrupadasVM = new List<EspecieViewModel>();

            foreach (var especieVM in especiesVM)
            {
                var repetido = false;
                foreach (var especieAgrupadaVM in especiesAgrupadasVM)
                {
                    if (especieVM.Nome == especieAgrupadaVM.Nome)
                    {
                        if (especieAgrupadaVM.Bioterios == null) especieAgrupadaVM.Bioterios = new List<BioterioViewModel>();
                        especieAgrupadaVM.Bioterios.Add(especieVM.Bioterio);
                        repetido = true;
                        break;
                    }
                }
                if (repetido == false)
                {
                    if (especieVM.Bioterios == null) especieVM.Bioterios = new List<BioterioViewModel>();
                    especieVM.Bioterios.Add(especieVM.Bioterio);
                    especiesAgrupadasVM.Add(especieVM);
                }
            }

            return View(especiesAgrupadasVM);
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
            especie.Nome = especie.Nome.ToUpper();

            await _especiesService.Adicionar(especie);

            return RedirectToAction("Index");
        }

        private async Task<EspecieViewModel> PopularBioterios(EspecieViewModel especie)
        {
            especie.Bioterios = _mapper.Map<IList<BioterioViewModel>>(await _especiesService.ObterBioterios());
            return especie;
        }

        [Route("detalhes-especie/{id:guid}")]
        public async Task<IActionResult> Detalhes(Guid id)
        {
            var especie = _mapper.Map<EspecieViewModel>(await _especiesService.ObterPorId(id));
            var especies = _mapper.Map<IList<EspecieViewModel>>(await _especiesService.ListarEspeciesComBioterio());
            var bioterioColocado = new List<Guid>();

            foreach (var esp in especies)
            {
                if (especie.Nome == esp.Nome && !bioterioColocado.Contains(esp.Bioterio.Id))
                {
                    if (especie.Bioterios == null) especie.Bioterios = new List<BioterioViewModel>();
                    especie.Bioterios.Add(esp.Bioterio);
                    bioterioColocado.Add(esp.Bioterio.Id);
                }
            }

            if (especie == null) return NotFound();

            return View(especie);
        }
    }
}
