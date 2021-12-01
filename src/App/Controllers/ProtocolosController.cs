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
    [Authorize(Roles = "Pesquisador,Admin,Presidente")]
    public class ProtocolosController : Controller
    {
        private readonly IProtocolosService _protocolosService;
        private readonly IEspeciesService _especiesService;
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public ProtocolosController(IProtocolosService protocolosService, IMapper mapper, IEspeciesService especiesService, IUsersRepository usersRepository)
        {
            _protocolosService = protocolosService;
            _mapper = mapper;
            _especiesService = especiesService;
            _usersRepository = usersRepository;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var protocolos = await _protocolosService.ListarProtocolosComPesquisador();
            return View(protocolos);
        }

        public async Task<IActionResult> Emitir()
        {
            var protocoloViewModel = await PopularEspeciesComBioterio(new ProtocoloViewModel());
            protocoloViewModel.Quantidades = new List<int>();

            for (int i = 0; i < protocoloViewModel.Especies.Count; i++)
            {
                protocoloViewModel.Quantidades.Add(0);
            }
            return View(protocoloViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Emitir(ProtocoloViewModel protocoloViewModel)
        {
            if (!ModelState.IsValid) return View(protocoloViewModel);

            await PopularEspeciesDesejadas(protocoloViewModel);
            var protocolo = MapearProtocolo(protocoloViewModel);
            protocolo.ApplicationUser = await _usersRepository.ObterUsuarioLogado();
            await _protocolosService.SalvarProtocoloEmitido(protocolo);

            return RedirectToAction("Index");
        }

        [Route("detalhes-protocolo/{id:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> Detalhes(Guid id)
        {
            var protocolo = await _protocolosService.ObterPorId(id);
            if (protocolo == null) return NotFound();
            protocolo = await _protocolosService.PopularEspecies(protocolo);

            var protocoloViewModel = MapearProtocoloViewModel(protocolo);

            if (protocolo == null) return NotFound();

            return View(protocoloViewModel);
        }

        private async Task<ProtocoloViewModel> PopularEspeciesComBioterio(ProtocoloViewModel protocoloViewModel)
        {
            protocoloViewModel.Especies = _mapper.Map<IList<EspecieViewModel>>(await _especiesService.ListarEspeciesComBioterio());
            return protocoloViewModel;
        }

        private async Task<ProtocoloViewModel> PopularEspeciesDesejadas(ProtocoloViewModel protocoloViewModel)
        {
            protocoloViewModel = await PopularEspeciesComBioterio(protocoloViewModel);

            var especies = new List<EspecieViewModel>();
            var quantidades = new List<int>();

            if(protocoloViewModel.Quantidades != null)
            {
                for (int i = 0; i < protocoloViewModel.Quantidades.Count; i++)
                {
                    if (protocoloViewModel.Quantidades[i] > 0)
                    {
                        especies.Add(protocoloViewModel.Especies[i]);
                        quantidades.Add(protocoloViewModel.Quantidades[i]);
                    }
                }
            }

            protocoloViewModel.Especies = especies;
            protocoloViewModel.Quantidades = quantidades;

            return protocoloViewModel;
        }

        private Protocolo MapearProtocolo(ProtocoloViewModel protocoloViewModel)
        {
            var protocolo = new Protocolo()
            {
                Justificativa = protocoloViewModel.Justificativa,
                ResumoPt = protocoloViewModel.ResumoPt,
                ResumoEn = protocoloViewModel.ResumoEn,
                DataInicio = protocoloViewModel.DataInicio,
                DataTermino = protocoloViewModel.DataTermino,
                Status = StatusProtocolo.AguardandoEnvioParaParecer,
                ProtocolosEspecies = new List<ProtocolosEspecies>()
            };

            for (int i = 0; i < protocoloViewModel.Quantidades.Count; i++)
            {
                protocolo.ProtocolosEspecies.Add(
                    new ProtocolosEspecies()
                    {
                        ProtocoloId = protocolo.Id,
                        EspecieId = protocoloViewModel.Especies[i].Id,
                        Quantidade = protocoloViewModel.Quantidades[i]
                    });
            }

            return protocolo;
        }

        private ProtocoloViewModel MapearProtocoloViewModel(Protocolo protocolo)
        {
            var protocoloViewModel = new ProtocoloViewModel()
            {
                Id = protocolo.Id,
                Justificativa = protocolo.Justificativa,
                ResumoPt = protocolo.ResumoPt,
                ResumoEn = protocolo.ResumoEn,
                DataInicio = protocolo.DataInicio,
                DataTermino = protocolo.DataTermino,
                Status = protocolo.Status,
                ApplicationUserViewModel = new ApplicationUserViewModel { UserInfo = new UserInfoViewModel { NomeCompleto = protocolo.ApplicationUser.UserInfo.NomeCompleto}},
                Especies = new List<EspecieViewModel>(),
                Quantidades = new List<int>(),
            };

            for (int i = 0; i < protocolo.ProtocolosEspecies.Count; i++)
            {
                protocoloViewModel.Especies.Add(_mapper.Map<EspecieViewModel>(protocolo.ProtocolosEspecies[i].Especie));
                protocoloViewModel.Quantidades.Add(protocolo.ProtocolosEspecies[i].Quantidade);
            }

            return protocoloViewModel;
        }
    }
}
