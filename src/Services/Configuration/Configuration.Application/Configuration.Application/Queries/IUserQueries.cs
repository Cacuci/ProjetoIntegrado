using Configuration.Application.Queries.DTOs.User;

namespace Configuration.Application.Queries
{
    public interface IUserQueries
    {
        Task<UserResponseDTO?> GetUserById(string id);
        Task<UserResponseDTO?> GetUserByEmail(string email);
        Task<IEnumerable<UserResponseDTO?>> GetUserAll();
    }
}
