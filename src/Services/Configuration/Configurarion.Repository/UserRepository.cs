using Configurarion.Repository;
using Configuration.Domain;
using Core.Data;

namespace Configuration.Repository
{
    internal class UserRepository : IUserRepository
    {
        private readonly ConfigurationContext _context;

        public UserRepository(ConfigurationContext context)
        {
            _context = context;
        }

        public IUnityOfWork UnityOfWork => _context;

        public void CreateAsync(User user)
        {
            throw new NotImplementedException();
        }

        public void DeleteAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public void UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
