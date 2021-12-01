using Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
    class ProtocolosEspeciesMapping : IEntityTypeConfiguration<ProtocolosEspecies>
    {
        public void Configure(EntityTypeBuilder<ProtocolosEspecies> builder)
        {
            builder.HasKey(protocoloEspecie => protocoloEspecie.Id);

            builder.Property(protocoloEspecie => protocoloEspecie.Quantidade)
                .IsRequired()
                .HasColumnType("varchar(5)");

            builder.ToTable("ProtocolosEspecies");
        }
    }
}
