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
        private readonly IProtocolosEspeciesRepository _protocolosEspeciesRepository;
        private readonly IProtocoloPareceristaRepository _protocoloPareceristaRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IEspecieRepository _especieRepository;

        public ProtocolosService(IProtocoloRepository protocoloRepository, IUsersRepository usersRepository, IProtocolosEspeciesRepository protocolosEspeciesRepository, IEspecieRepository especieRepository, IProtocoloPareceristaRepository protocoloPareceristaRepository)
        {
            _protocoloRepository = protocoloRepository;
            _usersRepository = usersRepository;
            _protocolosEspeciesRepository = protocolosEspeciesRepository;
            _especieRepository = especieRepository;
            _protocoloPareceristaRepository = protocoloPareceristaRepository;
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
            var protocolosPareceristas = await _protocoloPareceristaRepository.ObterTodos();
            var idsProtocolosComParecerista = new List<Guid>();

            foreach(var protocoloParecerista in protocolosPareceristas)
            {
                idsProtocolosComParecerista.Add(protocoloParecerista.ProtocoloId);
            }

            var protocolos = await ListarProtocolosComPesquisador();
            var protocolosSemParecerista = new List<Protocolo>();

            foreach(var protocolo in protocolos)
            {
                if (!idsProtocolosComParecerista.Contains(protocolo.Id))
                {
                    protocolosSemParecerista.Add(protocolo);
                }
            }

            return protocolosSemParecerista;
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

        public async Task<Protocolo> ObterPorId(Guid id)
        {
            var protocolo = await _protocoloRepository.ObterPorId(id);
            protocolo = await PopularPesquisador(protocolo);

            return protocolo;
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
