using Configuration.Application.Commands;
using Configuration.Application.DTOs.User;
using Configuration.Application.Queries;
using Core.Communication.Mediator;
using Core.Messages.CommonMessages.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Configuration.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserQueries _userQueries;
        private readonly IMediatorHandler _mediatorHandler;

        public UsersController(IUserQueries userQueries, INotificationHandler<DomainNotification> notification, IMediatorHandler mediatorHandler) : base(notification, mediatorHandler)
        {
            _userQueries = userQueries;
            _mediatorHandler = mediatorHandler;
        }

        /// <summary>
        /// Obtém a lista de usuários
        /// </summary>        
        /// <remarks>
        /// Está API pode ser usada para retornar os usuários.
        ///
        /// Não há parâmetros obrigatórios para esta API.
        ///
        /// Somente usuários autenticados podem acessar este recurso.        
        /// </remarks>
        /// <response code="200">Se a requisição foi bem sucedida</response>        
        /// <response code="401">Se o servidor não entendeu a requisição (Usuário não identificado)</response>  
        /// <response code="403">Se o servidor não entendeu a requisição (Usuário não autorizado)</response>    
        /// <response code="404">Se o servidor não encontrar o que foi pedido</response>    
        // GET: api/<UserController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserResponseDTO>> GetAllUser()
        {
            var users = await _userQueries.GetAllUser();

            if (!users.Any())
            {
                return NotFound();
            }

            return Ok(users);
        }

        /// <summary>
        /// Obtém o usuário específico
        /// </summary>        
        /// <remarks>
        /// Está API pode ser usada para retornar o usuário específico.
        ///
        /// Você precisa passar o ID de identificação do usuário na URL para a chamada bem-sucedida. Nenhum outro parâmetro no corpo da requisição é necessário.
        ///
        /// Somente usuários autenticados podem acessar este recurso.        
        /// </remarks>
        /// <response code="200">Se a requisição foi bem sucedida</response>        
        /// <response code="401">Se o servidor não entendeu a requisição (Usuário não identificado)</response>  
        /// <response code="403">Se o servidor não entendeu a requisição (Usuário não autorizado)</response>    
        /// <response code="404">Se o servidor não encontrar o que foi pedido</response>    
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

        /// <summary>
        /// Realiza o cadastro do usuário
        /// </summary>
        /// <remarks>
        /// Use esta API para realizar o cadastramento do usuário. Todos os detalhes devem ser passados no corpo da requisição.
        ///
        /// Somente usuários autenticados podem acessar este recurso.        
        /// </remarks>        
        /// <response code="201">Se a requisição foi bem sucedida</response>
        /// <response code="400">Se o servidor não entendeu a requisição</response>                      
        /// <response code="401">Se o servidor não entendeu a requisição (Usuário não identificado)</response>  
        /// <response code="403">Se o servidor não entendeu a requisição (Usuário não autorizado)</response>    
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

        /// <summary>
        /// Realiza a atualização cadastral de um usuário específico
        /// </summary>
        /// <remarks>
        /// Use esta API para realizar a atualização cadastral de um usuário específico. Todos os detalhes devem ser passados no corpo da requisição.
        ///
        /// Somente usuários autenticados podem acessar este recurso.        
        /// </remarks>        
        /// <response code="200">Se a requisição foi bem sucedida</response>        
        /// <response code="401">Se o servidor não entendeu a requisição (Usuário não identificado)</response>  
        /// <response code="403">Se o servidor não entendeu a requisição (Usuário não autorizado)</response>    
        /// <response code="404">Se o servidor não encontrar o que foi pedido</response>    
        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Realiza a deleção de um usuário específico
        /// </summary>
        /// <remarks>
        /// Use esta API para realizar a deleção de um usuário específico. Todos os detalhes devem ser passados no corpo da requisição.
        ///
        /// Somente usuários autenticados podem acessar este recurso.        
        /// </remarks>        
        /// <response code="200">Se a requisição foi bem sucedida</response>        
        /// <response code="401">Se o servidor não entendeu a requisição (Usuário não identificado)</response>  
        /// <response code="403">Se o servidor não entendeu a requisição (Usuário não autorizado)</response>    
        /// <response code="404">Se o servidor não encontrar o que foi pedido</response>    
        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
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
