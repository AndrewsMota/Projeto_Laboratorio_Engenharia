using Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
    class ParecerMapping : IEntityTypeConfiguration<Parecer>
    {
        public void Configure(EntityTypeBuilder<Parecer> builder)
        {
            builder.HasKey(parecer => parecer.Id);

            builder.Property(parecer => parecer.ProtocoloId)
                .IsRequired()
                .HasColumnType("varchar(40)");
            
            builder.Property(parecer => parecer.JustificativaDoParecer)
                .IsRequired()
                .HasColumnType("varchar(1000)");

            builder.Property(parecer => parecer.Escolha)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.ToTable("Pareceres");
        }
    }
}
