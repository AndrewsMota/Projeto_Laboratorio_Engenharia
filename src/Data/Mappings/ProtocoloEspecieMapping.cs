using Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
    class ProtocoloEspecieMapping : IEntityTypeConfiguration<ProtocoloEspecie>
    {
        public void Configure(EntityTypeBuilder<ProtocoloEspecie> builder)
        {
            builder.Property(protocoloEspecie => protocoloEspecie.Quantidade)
                .IsRequired()
                .HasColumnType("varchar(5)");

            builder.ToTable("ProtocolosEspecies");
        }
    }
}
