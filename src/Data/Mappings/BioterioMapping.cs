using Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
    class BioterioMapping : IEntityTypeConfiguration<Bioterio>
    {
        public void Configure(EntityTypeBuilder<Bioterio> builder)
        {
            builder.HasKey(bioterio => bioterio.Id);

            builder.Property(bioterio => bioterio.Nome)
                .IsRequired()
                .HasColumnType("varchar(300)");

            builder.Property(bioterio => bioterio.Telefone)
                .IsRequired()
                .HasColumnType("varchar(11)");
            
            builder.Property(bioterio => bioterio.Email)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(bioterio => bioterio.Cnpj)
                .IsRequired()
                .HasColumnType("varchar(14)");

            builder.HasMany(bioterio => bioterio.Especies)
                .WithOne(especie => especie.Bioterio)
                .HasForeignKey(especie => especie.BioterioId);

            builder.HasOne(bioterio => bioterio.Endereco)
                .WithOne(enderecoBioterio => enderecoBioterio.Bioterio)
                .HasForeignKey<EnderecoBioterio>(enderecoBioterio => enderecoBioterio.BioterioId);

            builder.ToTable("Bioterios");
        }
    }
}
