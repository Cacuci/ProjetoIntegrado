using Core.DomainObjects;
using Microsoft.AspNetCore.Identity;

namespace Configuration.Domain
{
    public class User : IdentityUser, IAggregateRoot
    {
        public User() { }

        public User(string userName, string password, string email)
        {
            UserName = userName;
            PasswordHash = password;
            Email = email;
        }
    }
}
