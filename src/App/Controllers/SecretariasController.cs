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
    public class SecretariasController : Controller
    {
        private readonly ISecretariasService _secretariasService;
        private readonly IPesquisadoresService _pesquisadoresService;
        private readonly IProtocolosService _protocolosService;
        private readonly IMapper _mapper;

        public SecretariasController(ISecretariasService secretariasService,
                                     IProtocolosService protocolosService,
                                     IPesquisadoresService pesquisadoresService,
                                     IMapper mapper)
        {
            _secretariasService = secretariasService;
            _protocolosService = protocolosService;
            _pesquisadoresService = pesquisadoresService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var secretarias = await _secretariasService.ListarSecretarias();
            return View(secretarias);
        }

        [Authorize(Roles = "Admin,Secretária")]
        public async Task<IActionResult> ListarProtocolosSemParecerista()
        {
            var protocolos = await _protocolosService.ListarProtocolosSemParecerista();
            return View(protocolos);
        }

        [Authorize(Roles = "Admin,Secretária")]
        public async Task<IActionResult> AtribuirParecerista(Guid id)
        {
            var atribuirPareceristaViewModel = new AtribuirPareceristaViewModel() { ProtocoloId = id };
            atribuirPareceristaViewModel = await PopularPareceristas(atribuirPareceristaViewModel);

            if(atribuirPareceristaViewModel.Pareceristas.Count == 0)
            {
                return NotFound();
            }
            return View(atribuirPareceristaViewModel);
        }

        [Authorize(Roles = "Admin,Secretária")]
        [HttpPost]
        public async Task<IActionResult> AtribuirParecerista(AtribuirPareceristaViewModel atribuirPareceristaViewModel)
        {
            var protocoloParecerista = new ProtocoloParecerista()
            {
                PareceristaId = atribuirPareceristaViewModel.PareceristaId,
                ProtocoloId = atribuirPareceristaViewModel.ProtocoloId.ToString()
            };

            await _protocolosService.AtribuirParecerista(protocoloParecerista);
            await _protocolosService.AtualizarStatusProtocoloPorId(atribuirPareceristaViewModel.ProtocoloId, StatusProtocolo.AguardandoParecer);

            return RedirectToAction("ListarProtocolosSemParecerista");
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

        private async Task<AtribuirPareceristaViewModel> PopularPareceristas(AtribuirPareceristaViewModel atribuirPareceristaViewModel)
        {
            atribuirPareceristaViewModel.Pareceristas = await _pesquisadoresService.ListarPesquisadores();
            if(atribuirPareceristaViewModel.Pareceristas[0].UserInfo.NomeCompleto == "Nenhum presidente selecionado")
            {
                atribuirPareceristaViewModel.Pareceristas.RemoveAt(0);
            }
            return atribuirPareceristaViewModel;
        }
    }
}
