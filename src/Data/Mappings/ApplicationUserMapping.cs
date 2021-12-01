using Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
    class ApplicationUserMapping : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasKey(applicationUser => applicationUser.Id);

            builder.HasOne(applicationUser => applicationUser.Endereco)
            .WithOne(endereco => endereco.ApplicationUser)
            .HasForeignKey<Endereco>(endereco => endereco.UserId);

            builder.HasOne(applicationUser => applicationUser.UserInfo)
            .WithOne(userInfo => userInfo.ApplicationUser)
            .HasForeignKey<UserInfo>(userInfo => userInfo.UserId);

            builder.HasMany(applicationUser => applicationUser.Protocolos)
            .WithOne(protocolo => protocolo.ApplicationUser)
            .HasForeignKey(protocolo => protocolo.ApplicationUserId);

            builder.HasMany(applicationUser => applicationUser.ProtocoloParecerista)
            .WithOne(protocoloParecerista => protocoloParecerista.Parecerista)
            .HasForeignKey(protocoloParecerista => protocoloParecerista.PareceristaId);
        }
    }
}
