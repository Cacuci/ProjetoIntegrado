using Configuration.Application.Commands;
using Configuration.Application.Queries;
using Configuration.Application.Queries.DTOs.User;
using Core.Communication.Mediator;
using Core.Messages.CommonMessages.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Configuration.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserQueries _userQueries;
        private readonly IMediatorHandler _mediatorHandler;

        public UsersController(IUserQueries userQueries, INotificationHandler<DomainNotification> notification, IMediatorHandler mediatorHandler) : base(notification, mediatorHandler)
        {
            _userQueries = userQueries;
            _mediatorHandler = mediatorHandler;
        }

        // GET: api/<UserController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserResponseDTO>> GetUserAll()
        {
            var users = await _userQueries.GetUserAll();

            if (!users.Any())
            {
                return NotFound();
            }

            return Ok(users);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserResponseDTO>> GetUserByID(string id)
        {
            var user = await _userQueries.GetUserById(id);

            if (string.IsNullOrEmpty(user?.ID))
            {
                return NotFound();
            }

            return Ok(user);
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
                var command = new CreateUserCommand(request.Name, request.Email, request.Password);

                await _mediatorHandler.SendCommand(command);

                if (!OperationValid())
                {
                    return BadRequest(GetMessageError());
                }

                var user = await _userQueries.GetUserByEmail(request.Email);

                return Created("api/users/{id}", user?.ID);
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
                var command = new UpdateUserCommand(id, request.Name);

                await _mediatorHandler.SendCommand(command);

                if (!OperationValid())
                {
                    return NotFound(GetMessageError());
                }

                var user = await _userQueries.GetUserById(id);

                return Ok(user?.ID);
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
            var command = new DeleteUserCommand(id);

            await _mediatorHandler.SendCommand(command);

            if (!OperationValid())
            {
                return NotFound(GetMessageError());
            }

            return Ok(id);
        }
    }
}
