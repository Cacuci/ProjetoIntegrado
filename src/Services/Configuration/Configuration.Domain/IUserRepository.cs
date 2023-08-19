using Core.Data;

namespace Configuration.Domain
{
    public interface IUserRepository : IRepository<User>
    {
        Task CreateUserAsync(User user, string password);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(User user);
        Task<User?> GetUserById(string id);
        Task<User?> GetUserByEmail(string email);
        Task<IEnumerable<User?>> GetUserAll();
    }
}
