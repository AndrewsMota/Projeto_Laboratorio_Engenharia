using Business.Interfaces;
using Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Services
{
    public class BioteriosService : IBioteriosService
    {
        private readonly IBioterioRepository _bioterioRepository;

        public BioteriosService(IBioterioRepository bioterioRepository)
        {
            _bioterioRepository = bioterioRepository;
        }

        public async Task<IList<Bioterio>> ListarBioterios()
        {
            var bioterios = await _bioterioRepository.ObterTodos();
            return bioterios;
        }

        public async Task Adicionar(Bioterio bioterio)
        {
            await _bioterioRepository.Adicionar(bioterio);
        }

        public async Task<Bioterio> ObterPorIdComEndereco(Guid id)
        {
            var bioterio = await _bioterioRepository.ObterPorIdComEndereco(id);

            return bioterio;
        }
    }
}
