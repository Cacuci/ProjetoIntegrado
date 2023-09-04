using Core.Communication.Mediator;
using Core.Data;
using Core.Extensions;
using Core.Messages;
using Inbound.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Inbound.Infrastructure.Context
{
    public class InboundDataContext : DbContext, IUnityOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDocument> OrderDocuments { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Barcode> Barcodes { get; set; }

        public InboundDataContext(DbContextOptions<InboundDataContext> options, IMediatorHandler mediatorHandler) : base(options)
        {
            _mediatorHandler = mediatorHandler;
        }

        public async Task<bool> Commit()
        {
            foreach (var entry in ChangeTracker.Entries()
                                               .Where(entry => entry.Entity.GetType()
                                               .GetProperty("DateRegister") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DateRegister").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DateRegister").IsModified = false;
                }
            }

            bool success = await base.SaveChangesAsync() > 0;

            if (success)
            {
                await _mediatorHandler.PublishEvents(this);
            }

            return success;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entity.GetProperties().Where(p => p.IsPrimaryKey()))
                {
                    property.ValueGenerated = ValueGenerated.Never;
                }
            }

            modelBuilder.Ignore<Event>();

            modelBuilder.Entity<Order>()
                        .HasIndex(c => c.Number)
                        .IsUnique();

            modelBuilder.Entity<OrderDocument>()
                        .HasIndex(c => new
                        {
                            c.OrderId,
                            c.Number,
                        })
                        .IsUnique();

            modelBuilder.Entity<OrderItem>()
                        .HasIndex(c => new
                        {
                            c.DocumentId,
                            c.ProductId,
                            c.PackageId
                        })
                        .IsUnique();

            modelBuilder.Entity<OrderItem>()
                        .Property(c => c.Quantity).HasPrecision(15, 3);

            modelBuilder.Entity<Product>()
                        .HasIndex(c => c.Code).IsUnique();

            modelBuilder.Entity<Package>()
                        .HasIndex(c => new
                        {
                            c.Type,
                            c.Capacity
                        });

            modelBuilder.Entity<Barcode>()
                        .HasIndex(c => new
                        {
                            c.PackageId,
                            c.Code
                        });

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(InboundDataContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
