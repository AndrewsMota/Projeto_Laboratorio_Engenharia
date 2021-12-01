using App.ViewModels;
using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Controllers
{
    public class PesquisadoresController : Controller
    {
        private readonly IPesquisadoresService _pesquisadoresService;
        private readonly IProtocolosService _protocolosService;
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public PesquisadoresController(IPesquisadoresService pesquisadoresService,
                                       IUsersRepository usersRepository, 
                                       IProtocolosService protocolosService, 
                                       IMapper mapper)
        {
            _pesquisadoresService = pesquisadoresService;
            _usersRepository = usersRepository;
            _protocolosService = protocolosService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var pesquisadores = await _pesquisadoresService.ListarPesquisadores();
            return View(pesquisadores);
        }

        [Authorize(Roles = "Admin,Pesquisador,Presidente")]
        public async Task<IActionResult> ListarProtocolosDoParecerista()
        {
            var user = await _usersRepository.ObterUsuarioLogado();
            var protocolosPareceristas = await _protocolosService.ListarProtocolosPareceristas();
            var protocolos = new List<Protocolo>();

            foreach(var protocoloParecerista in protocolosPareceristas)
            {
                if (protocoloParecerista.PareceristaId == user.Id)
                {
                    var protocolo = await _protocolosService.ObterPorId(new Guid(protocoloParecerista.ProtocoloId));

                    if(protocolo.Status == StatusProtocolo.AguardandoParecer)
                    {
                        protocolos.Add(protocolo);
                    }
                }
            }

            return View(protocolos);
        }

        [Authorize(Roles = "Pesquisador,Presidente")]
        public IActionResult DarParecer(Guid id)
        {
            var parecerViewModel = new ParecerViewModel() { ProtocoloId = id };
            return View(parecerViewModel);
        }

        [Authorize(Roles = "Pesquisador,Presidente")]
        [HttpPost]
        public async Task<IActionResult> DarParecer(ParecerViewModel parecerViewModel)
        {
            if (!ModelState.IsValid) return View(parecerViewModel);

            var parecer = new Parecer()
            {
                ProtocoloId = parecerViewModel.ProtocoloId,
                JustificativaDoParecer = parecerViewModel.JustificativaDoParecer,
                Escolha = parecerViewModel.Escolha
            };

            await _protocolosService.SalvarParecer(parecer);
            await _protocolosService.AtualizarStatusProtocoloPorId(parecerViewModel.ProtocoloId, StatusProtocolo.AguardandoDeliberacao);

            return RedirectToAction("ListarProtocolosDoParecerista");
        }

        [Authorize(Roles = "Admin,Presidente")]
        public async Task<IActionResult> ListarProtocolosAguardandoDeliberacao()
        {
            var protocolos = await _protocolosService.ListarProtocolosAguardandoDeliberacao();
            return View(protocolos);
        }

        [Authorize(Roles = "Admin,Presidente")]
        public async Task<IActionResult> EmitirDeliberacao(Guid id)
        {
            var protocolo = await _protocolosService.ObterPorId(id);
            if (protocolo == null) return NotFound();
            protocolo = await _protocolosService.PopularEspecies(protocolo);

            var protocoloViewModel = MapearProtocoloViewModel(protocolo);

            if (protocolo == null) return NotFound();

            return View(protocoloViewModel);
        }

        [Authorize(Roles = "Admin,Presidente")]
        [HttpPost]
        public async Task<IActionResult> DeliberacaoAprovada(ProtocoloViewModel protocoloViewModel)
        {
            await _protocolosService.AtualizarStatusProtocoloPorId(protocoloViewModel.Id, StatusProtocolo.Aprovado);
            return RedirectToAction("ListarProtocolosAguardandoDeliberacao");
        }

        [Authorize(Roles = "Admin,Presidente")]
        [HttpPost]
        public async Task<IActionResult> DeliberacaoRecusada(ProtocoloViewModel protocoloViewModel)
        {
            await _protocolosService.AtualizarStatusProtocoloPorId(protocoloViewModel.Id, StatusProtocolo.Reprovado);
            return RedirectToAction("ListarProtocolosAguardandoDeliberacao");
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
                ApplicationUserViewModel = new ApplicationUserViewModel { UserInfo = new UserInfoViewModel { NomeCompleto = protocolo.ApplicationUser.UserInfo.NomeCompleto } },
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
