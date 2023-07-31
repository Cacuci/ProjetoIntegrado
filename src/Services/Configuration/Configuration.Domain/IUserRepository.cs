namespace Configuration.Domain
{
    public interface IUserRepository
    {
        Task CreateAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
        Task<User?> GetByIdAsync(string id);
        Task<User?> GetByEmailAsync(string email);
        Task<IEnumerable<User?>> GetAllAsync();
    }
}
