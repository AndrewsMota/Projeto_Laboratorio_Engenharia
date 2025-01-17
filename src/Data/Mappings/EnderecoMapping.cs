﻿using Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
    class EnderecoMapping : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.HasKey(endereco => endereco.Id);

            builder.Property(endereco => endereco.Logradouro)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(endereco => endereco.Numero)
                .IsRequired()
                .HasColumnType("varchar(6)");

            builder.Property(endereco => endereco.Complemento)
                .HasColumnType("varchar(300)");

            builder.Property(endereco => endereco.Cep)
                .IsRequired()
                .HasColumnType("varchar(8)");

            builder.Property(endereco => endereco.Bairro)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(endereco => endereco.Cidade)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(endereco => endereco.Estado)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.ToTable("Enderecos");
        }
    }
}
