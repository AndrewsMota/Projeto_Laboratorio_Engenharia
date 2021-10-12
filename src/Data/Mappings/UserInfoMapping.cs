using Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
    class UserInfoMapping : IEntityTypeConfiguration<UserInfo>
    {
        public void Configure(EntityTypeBuilder<UserInfo> builder)
        {
            builder.HasKey(userInfo => userInfo.Id);

            builder.Property(userInfo => userInfo.NomeCompleto)
                .IsRequired()
                .HasColumnType("varchar(300)");

            builder.Property(userInfo => userInfo.Cpf)
                .IsRequired()
                .HasColumnType("varchar(11)");

            builder.Property(userInfo => userInfo.DataNascimento)
                .IsRequired()
                .HasColumnType("date");

            builder.Property(userInfo => userInfo.Sexo)
                .IsRequired()
                .HasColumnType("varchar(15)");

            builder.ToTable("UsersInfo");
        }
    }
}
