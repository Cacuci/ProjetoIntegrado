using Core.Data;

namespace Configuration.Domain
{
    public interface IUserRepository : IRepository<User>
    {
        void CreateAsync(User user);
        void UpdateAsync(User user);
        void DeleteAsync(User user);
        Task<User> GetByIdAsync(int id);
        Task<User> GetByNameAsync(string name);
        Task<User> GetByEmailAsync(string email);
    }
}
