using Business.Interfaces;
using Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Services
{
    public class ProtocolosService : IProtocolosService
    {
        private readonly IProtocoloRepository _protocoloRepository;
        private readonly IParecerRepository _parecerRepository;
        private readonly IProtocolosEspeciesRepository _protocolosEspeciesRepository;
        private readonly IProtocoloPareceristaRepository _protocoloPareceristaRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IEspecieRepository _especieRepository;

        public ProtocolosService(IProtocoloRepository protocoloRepository,
                                 IUsersRepository usersRepository,
                                 IProtocolosEspeciesRepository protocolosEspeciesRepository,
                                 IEspecieRepository especieRepository,
                                 IProtocoloPareceristaRepository protocoloPareceristaRepository,
                                 IParecerRepository parecerRepository)
        {
            _protocoloRepository = protocoloRepository;
            _usersRepository = usersRepository;
            _protocolosEspeciesRepository = protocolosEspeciesRepository;
            _especieRepository = especieRepository;
            _protocoloPareceristaRepository = protocoloPareceristaRepository;
            _parecerRepository = parecerRepository;
        }

        public async Task<IList<Protocolo>> ListarProtocolos()
        {
            var protocolos = await _protocoloRepository.ObterTodos();
            return protocolos;
        }

        public async Task<IList<Protocolo>> ListarProtocolosComPesquisador()
        {
            var protocolos = await ListarProtocolos();
            for (int i = 0; i < protocolos.Count; i++) { protocolos[i] = await PopularPesquisador(protocolos[i]); }
            return protocolos;
        }

        public async Task<IList<Protocolo>> ListarProtocolosSemParecerista()
        {
            var protocolos = await _protocoloRepository.Buscar(protocolos => protocolos.Status == StatusProtocolo.AguardandoEnvioParaParecer);
            var protocolosComPesquisadores = new List<Protocolo>();

            foreach (var protocolo in protocolos) 
            {
                protocolosComPesquisadores.Add(await PopularPesquisador(protocolo));
            }

            return protocolosComPesquisadores;
        }

        public async Task<IList<ProtocoloParecerista>> ListarProtocolosPareceristas()
        {
            var protocolosPareceristas = await _protocoloPareceristaRepository.ObterTodos();
            return protocolosPareceristas;
        }

        public async Task<IList<Protocolo>> ListarProtocolosAguardandoDeliberacao()
        {
            var protocolos = await _protocoloRepository.Buscar(protocolos => protocolos.Status == StatusProtocolo.AguardandoDeliberacao);
            for (int i = 0; i < protocolos.Count; i++) { protocolos[i] = await PopularPesquisador(protocolos[i]); }
            return protocolos;
        }

        public async Task<Protocolo> PopularPesquisador(Protocolo protocolo)
        {
            var applicationUser = await _usersRepository.ObterPorIdComUserInfoEEndereco(protocolo.ApplicationUserId);
            if (applicationUser.UserInfo == null)
            {
                applicationUser.UserInfo = new UserInfo() { NomeCompleto = "Admin" };
            }
            protocolo.ApplicationUser = applicationUser;
            return protocolo;
        }

        public async Task SalvarProtocoloEmitido(Protocolo protocolo)
        {
            await _protocoloRepository.Adicionar(protocolo);
        }

        public async Task SalvarParecer(Parecer parecer)
        {
            await _parecerRepository.Adicionar(parecer);
        }

        public async Task<Protocolo> ObterPorId(Guid id)
        {
            var protocolo = await _protocoloRepository.ObterPorId(id);
            protocolo = await PopularPesquisador(protocolo);

            return protocolo;
        }

        public async Task AtribuirParecerista(ProtocoloParecerista protocoloParecerista)
        {
            await _protocoloPareceristaRepository.Adicionar(protocoloParecerista);
        }

        public async Task AtualizarStatusProtocoloPorId(Guid id, StatusProtocolo status)
        {
            var protocolo = await _protocoloRepository.ObterPorId(id);
            protocolo.Status = status;
            await _protocoloRepository.Atualizar(protocolo);
        }

        public async Task<Protocolo> PopularEspecies(Protocolo protocolo)
        {
            var protocoloEspecies = await _protocolosEspeciesRepository.Buscar(protocoloEspecie => protocoloEspecie.ProtocoloId == protocolo.Id);

            if(protocolo.ProtocolosEspecies == null)
            {
                protocolo.ProtocolosEspecies = new List<ProtocolosEspecies>();
            }

            foreach(var protocoloEspecie in protocoloEspecies)
            {
                protocoloEspecie.Especie = await _especieRepository.ObterPorId(protocoloEspecie.EspecieId);
                protocolo.ProtocolosEspecies.Add(protocoloEspecie);
            }

            return protocolo;
        }
    }
}
