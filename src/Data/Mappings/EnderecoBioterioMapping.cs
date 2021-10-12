using Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
    class EnderecoBioterioMapping : IEntityTypeConfiguration<EnderecoBioterio>
    {
        public void Configure(EntityTypeBuilder<EnderecoBioterio> builder)
        {
            builder.HasKey(enderecoBioterio => enderecoBioterio.Id);

            builder.Property(enderecoBioterio => enderecoBioterio.Logradouro)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(enderecoBioterio => enderecoBioterio.Numero)
                .IsRequired()
                .HasColumnType("varchar(6)");

            builder.Property(enderecoBioterio => enderecoBioterio.Complemento)
                .HasColumnType("varchar(300)");

            builder.Property(enderecoBioterio => enderecoBioterio.Cep)
                .IsRequired()
                .HasColumnType("varchar(8)");

            builder.Property(enderecoBioterio => enderecoBioterio.Bairro)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(enderecoBioterio => enderecoBioterio.Cidade)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(enderecoBioterio => enderecoBioterio.Estado)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.ToTable("EnderecosBioterios");
        }
    }
}
