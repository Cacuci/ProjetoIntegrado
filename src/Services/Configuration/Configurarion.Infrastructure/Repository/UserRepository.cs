using Configuration.Domain;
using Configuration.Infrastructure.Context;
using Core.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Configuration.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;

        private readonly UserDataContext _context;

        public IUnityOfWork UnityOfWork => _context;

        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task CreateUserAsync(User user, string password)
        {
            await _userManager.CreateAsync(user, password);
        }

        public async Task DeleteUserAsync(User user)
        {
            await _userManager.DeleteAsync(user);
        }

        public async Task UpdateUserAsync(User user)
        {
            await _userManager.UpdateAsync(user);
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            return user;
        }

        public async Task<User?> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            return user;
        }

        public Task<IEnumerable<User?>> GetAllUser()
        {
            var users = _userManager.Users.AsNoTracking().AsEnumerable();

            return Task.FromResult(users);
        }

        public void Dispose() => _userManager.Dispose();
    }
}
