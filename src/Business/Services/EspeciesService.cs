﻿using Business.Interfaces;
using Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Services
{
    public class EspeciesService : IEspeciesService
    {
        private readonly IEspecieRepository _especieRepository;
        private readonly IBioteriosService _bioteriosService;

        public EspeciesService(IEspecieRepository especieRepository, IBioteriosService bioteriosService)
        {
            _especieRepository = especieRepository;
            _bioteriosService = bioteriosService;
        }

        public async Task<IList<Especie>> ListarEspecies()
        {
            var especies = await _especieRepository.ObterTodos();
            return especies;
        }

        public async Task Adicionar(Especie especie)
        {
            await _especieRepository.Adicionar(especie);
        }

        public async Task<IEnumerable<Bioterio>> ObterBioterios()
        {
            return await _bioteriosService.ListarBioterios();
        }
    }    
} 
