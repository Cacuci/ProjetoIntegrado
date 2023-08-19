using Configuration.Application.DTOs.User;
using Configuration.Domain;

namespace Configuration.Application.Queries
{
    public class UserQueries : IUserQueries
    {
        private readonly IUserRepository _userRepository;

        public UserQueries(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserResponseDTO?>> GetUserAll()
        {
            var users = Enumerable.Empty<UserResponseDTO>();

            var result = await _userRepository.GetUserAll();

            if (result.Any())
            {
                users = result.Select(user => new UserResponseDTO(user.Id, user.UserName, user.Email));
            }

            return users;
        }

        public async Task<UserResponseDTO?> GetUserByEmail(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);

            return new UserResponseDTO(user?.Id, user?.UserName, user?.Email);
        }

        public async Task<UserResponseDTO?> GetUserById(string id)
        {
            var user = await _userRepository.GetUserById(id);

            return new UserResponseDTO(user?.Id, user?.UserName, user?.Email);
        }
    }
}
