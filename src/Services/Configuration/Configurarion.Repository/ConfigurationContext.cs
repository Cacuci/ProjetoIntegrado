using Configuration.Domain;
using Core.Communication.Mediator;
using Core.Data;
using Core.Extensions;
using Core.Messages;
using Microsoft.EntityFrameworkCore;

namespace Configurarion.Repository
{
    public class ConfigurationContext : DbContext, IUnityOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;

        public DbSet<User> AspNetUsers { get; set; }

        public ConfigurationContext(DbContextOptions<ConfigurationContext> options, IMediatorHandler mediatorHandler) : base(options)
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
            modelBuilder.Ignore<Event>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ConfigurationContext).Assembly);

            foreach (var relationship in modelBuilder.Model
                                         .GetEntityTypes()
                                         .SelectMany(e => e
                                         .GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ConfigurationContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
