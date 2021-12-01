using Business.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<EnderecoBioterio> EnderecosBioterios { get; set; }
        public DbSet<Bioterio> Bioterios { get; set; }
        public DbSet<UserInfo> UsersInfo { get; set; }
        public DbSet<Especie> Especies { get; set; }
        public DbSet<Protocolo> Protocolos { get; set; }
        public DbSet<ProtocolosEspecies> ProtocolosEspecies { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);



            base.OnModelCreating(modelBuilder);
        }
    }
}
