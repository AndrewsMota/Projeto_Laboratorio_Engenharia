using Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
    class EspecieMapping : IEntityTypeConfiguration<Especie>
    {
        public void Configure(EntityTypeBuilder<Especie> builder)
        {
            builder.Property(especie => especie.Nome)
                .IsRequired()
                .HasColumnType("varchar(300)");

            builder.HasMany(especie => especie.ProtocolosEspecies)
            .WithOne(protocolosEspecies => protocolosEspecies.Especie)
            .HasForeignKey(protocolosEspecies => protocolosEspecies.EspecieId);

            builder.ToTable("Especies");
        }
    }
}
