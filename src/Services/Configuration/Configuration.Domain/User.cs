using Core.DomainObjects;

namespace Configuration.Domain
{
    public class User : Entity, IAggregateRoot
    {
        public string UserName { get; private set; }

        public string Password { get; private set; }

        public string Email { get; private set; }

        public DateTime DateRegister { get; private set; }

        public User(string userName, string password, string email)
        {
            UserName = userName;
            Password = password;
            Email = email;
        }
    }
}
