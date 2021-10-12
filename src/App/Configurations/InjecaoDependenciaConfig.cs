using Business.Interfaces;
using Business.Services;
using Data;
using Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace App.Configurations
{
    public static class InjecaoDependenciaConfig
    {
        public static IServiceCollection AddDependencias(this IServiceCollection services)
        {
            services.AddScoped<ApplicationDbContext>();
            services.AddScoped<IUserInfoRepository, UserInfoRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IBioterioRepository, BioterioRepository>();
            services.AddScoped<IEspecieRepository, EspecieRepository>();
            services.AddScoped<IPesquisadoresService, PesquisadoresService>();
            services.AddScoped<ISecretariasService, SecretariasService>();
            services.AddScoped<IBioteriosService, BioteriosService>();
            services.AddScoped<IEspeciesService, EspeciesService>();

            return services;
        }
    }
}
