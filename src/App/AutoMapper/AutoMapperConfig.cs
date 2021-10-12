using AutoMapper;
using App.ViewModels;
using Business.Models;

namespace App.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Bioterio, BioterioViewModel>().ReverseMap();
            CreateMap<Especie, EspecieViewModel>().ReverseMap();
        }
    }
}
