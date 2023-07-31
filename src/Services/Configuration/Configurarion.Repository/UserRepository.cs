using Configuration.Domain;
using Microsoft.AspNetCore.Identity;

namespace Configuration.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;

        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task CreateAsync(User user)
        {
            await _userManager.CreateAsync(user);
        }

        public async Task DeleteAsync(User user)
        {
            await _userManager.DeleteAsync(user);
        }

        public async Task UpdateAsync(User user)
        {
            await _userManager.UpdateAsync(user);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            return await Task.FromResult(user);
        }

        public async Task<User?> GetByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            return await Task.FromResult(user);
        }

        public Task<IEnumerable<User?>> GetAllAsync()
        {
            IEnumerable<User?> users = _userManager.Users.AsEnumerable();

            return Task.FromResult(users);
        }
    }
}
