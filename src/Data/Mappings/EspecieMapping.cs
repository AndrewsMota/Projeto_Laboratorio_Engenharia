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

            builder.ToTable("Especies");
        }
    }
}
