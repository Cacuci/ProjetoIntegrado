using Configuration.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Configuration.Infrastructure.Mappings
{
    public class WarehouseMapping : IEntityTypeConfiguration<Warehouse>
    {
        public void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code)
                   .IsRequired()
                   .HasColumnType("varchar(20)");

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasColumnType("varchar(60)");

            builder.ToTable("Warehouses");
        }
    }
}
