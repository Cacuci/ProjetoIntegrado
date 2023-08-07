using Configuration.API.DTOs.User;
using Configuration.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Configuration.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: api/<UserController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserResponseDTO>> GetUserAll()
        {
            var users = await _userRepository.GetUserAllAsync();

            if (users is not null)
            {
                var response = users.Select(user => new UserResponseDTO(user.Id, user.UserName, user.Email));

                return Ok(response);
            }

            return NotFound();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserResponseDTO>> GetUserByID(string id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);

            if (user is not null)
            {
                var response = new UserResponseDTO(user.Id, user.UserName, user.Email);

                return Ok(response);
            }

            return NotFound();
        }

        // POST api/<UserController>5
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<string>> CreateUser([FromBody] UserRequestDTO request)
        {
            if (ModelState.IsValid)
            {
                var user = await _userRepository.GetUserByEmailAsync(request.Email);

                if (user is null)
                {
                    var userModel = new User(request.Name, request.Password, request.Email);

                    await _userRepository.CreateUserAsync(userModel);

                    user = await _userRepository.GetUserByEmailAsync(userModel.Email);

                    return Created("api/users/{id}", user.Id);
                }

                return BadRequest("Email já cadastrado");
            }

            return BadRequest(ModelState);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<string>> UpdateUser(string id, [FromBody] UserUpdateRequestDTO request)
        {
            if (ModelState.IsValid)
            {
                var user = await _userRepository.GetUserByIdAsync(id);

                if (user is not null)
                {
                    user.UserName = request.Name;

                    await _userRepository.UpdateUserAsync(user);

                    return Ok(user.Id);
                }

                return BadRequest("Usuário não encontrado");
            }

            return BadRequest(ModelState);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> DeleteUser(string id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);

            if (user is not null)
            {
                await _userRepository.DeleteUserAsync(user);

                return Ok(user?.Id);
            }

            return BadRequest("Usuário não encontrado");
        }
    }
}
