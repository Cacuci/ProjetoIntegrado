using Core.DomainObjects;
using Microsoft.AspNetCore.Identity;

namespace Configuration.Domain
{
    public class User : IdentityUser, IAggregateRoot
    {
        public User() { }

        public User(string userName, string email)
        {
            UserName = userName;
            Email = email;
        }
    }
}
