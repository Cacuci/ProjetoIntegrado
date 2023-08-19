using Configuration.Domain;
using Core.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Configuration.Repository.Context
{
    public class UserDataContext : IdentityDbContext<User>, IUnityOfWork
    {
        public UserDataContext(DbContextOptions<UserDataContext> options) : base(options) { }

        public Task<bool> Commit()
        {
            throw new NotImplementedException();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
