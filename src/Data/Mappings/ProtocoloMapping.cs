using Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
    class ProtocoloMapping : IEntityTypeConfiguration<Protocolo>
    {
        public void Configure(EntityTypeBuilder<Protocolo> builder)
        {
            builder.Property(protocolo => protocolo.Justificativa)
                .IsRequired()
                .HasColumnType("varchar(500)");
            
            builder.Property(protocolo => protocolo.ResumoPt)
                .IsRequired()
                .HasColumnType("varchar(1000)");
            
            builder.Property(protocolo => protocolo.ResumoEn)
                .IsRequired()
                .HasColumnType("varchar(1000)");

            builder.Property(protocolo => protocolo.DataInicio)
                .IsRequired()
                .HasColumnType("date");

            builder.Property(protocolo => protocolo.DataTermino)
                .IsRequired()
                .HasColumnType("date");

            builder.Property(userInfo => userInfo.Status)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.HasMany(protocolo => protocolo.ProtocolosEspecies)
            .WithOne(protocolosEspecies => protocolosEspecies.Protocolo)
            .HasForeignKey(protocolosEspecies => protocolosEspecies.ProtocoloId);

            builder.ToTable("Protocolos");
        }
    }
}
