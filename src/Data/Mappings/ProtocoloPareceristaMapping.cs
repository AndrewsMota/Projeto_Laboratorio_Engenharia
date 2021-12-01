using Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
    class ProtocoloPareceristaMapping : IEntityTypeConfiguration<ProtocoloParecerista>
    {
        public void Configure(EntityTypeBuilder<ProtocoloParecerista> builder)
        {
            builder.HasKey(protocoloParecerista => protocoloParecerista.Id);

            builder.ToTable("ProtocoloPareceristas");
        }
    }
}
