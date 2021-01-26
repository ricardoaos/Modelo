using FiscalPrime.Backend.Domain.Entities.DominioExterno;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiscalPrime.Backend.Infra.Data.Mappers.Entities
{
    public class DominioExternoTypeConfiguration : IEntityTypeConfiguration<DominioExterno>
    {
        public void Configure(EntityTypeBuilder<DominioExterno> builder)
        {
            builder.ToTable("DominioExterno");

            builder.HasKey(t => new { t.IdDominio, t.Codigo });
        }
    }
}
