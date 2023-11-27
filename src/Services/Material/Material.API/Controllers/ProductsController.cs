using Core.Communication.Mediator;
using Core.Messages.CommonMessages.Notifications;
using Material.Application.Commands;
using Material.Application.DTOs;
using Material.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Material.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IMaterialQueries _materialQueries;
        private readonly IMediatorHandler _mediatorHandler;

        public ProductsController(IMaterialQueries materialQueries, INotificationHandler<DomainNotification> notification, IMediatorHandler mediatorHandler) : base(notification, mediatorHandler)
        {
            _materialQueries = materialQueries;
            _mediatorHandler = mediatorHandler;
        }

        /// <summary>
        /// Obtém a lista de produtos
        /// </summary>        
        /// <remarks>
        /// Está API pode ser usada para retornar os produtos.
        ///
        /// Não há parâmetros obrigatórios para esta API.
        ///
        /// Somente usuários autenticados podem acessar este recurso.        
        /// </remarks>
        /// <response code="200">Se a requisição foi bem sucedida</response>        
        /// <response code="401">Se o servidor não entendeu a requisição (Usuário não identificado)</response>  
        /// <response code="403">Se o servidor não entendeu a requisição (Usuário não autorizado)</response>    
        /// <response code="404">Se o servidor não encontrar o que foi pedido</response>    
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductResponseDTO>> GetAllOrder(CancellationToken cancellationToken)
        {
            var warehouses = await _materialQueries.GetAllAsync(cancellationToken);

            if (!warehouses.Any())
            {
                return NotFound();
            }

            return Ok(warehouses);
        }

        /// <summary>
        /// Obtém um produto específico
        /// </summary>        
        /// <remarks>
        /// Está API pode ser usada para retornar um produto específico.
        ///
        /// Você precisa passar o ID de identificação do produto na URL para a chamada bem-sucedida. Nenhum outro parâmetro no corpo da requisição é necessário.
        ///
        /// Somente usuários autenticados podem acessar este recurso.        
        /// </remarks>
        /// <response code="200">Se a requisição foi bem sucedida</response>        
        /// <response code="401">Se o servidor não entendeu a requisição (Usuário não identificado)</response>  
        /// <response code="403">Se o servidor não entendeu a requisição (Usuário não autorizado)</response>    
        /// <response code="404">Se o servidor não encontrar o que foi pedido</response>    
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductResponseDTO>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var warehouse = await _materialQueries.GetByIdAsync(id, cancellationToken);

            if (warehouse is null)
            {
                return NotFound();
            }

            return Ok(warehouse);
        }

        /// <summary>
        /// Realiza a atualização cadastral de um produto específico
        /// </summary>
        /// <remarks>
        /// Use esta API para realizar a atualização cadastral de um produto específico. Todos os detalhes devem ser passados no corpo da requisição.
        ///
        /// Somente usuários autenticados podem acessar este recurso.        
        /// </remarks>        
        /// <response code="200">Se a requisição foi bem sucedida</response>        
        /// <response code="400">Se o servidor não entendeu a requisição</response>                      
        /// <response code="401">Se o servidor não entendeu a requisição (Usuário não identificado)</response>  
        /// <response code="403">Se o servidor não entendeu a requisição (Usuário não autorizado)</response>    
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<string>> Update([FromBody] IEnumerable<ProductRequestDTO> request)
        {
            if (ModelState.IsValid)
            {
                var command = new UpdateProductCommand(request);

                await _mediatorHandler.SendCommand(command);

                if (!OperationValid())
                {
                    return BadRequest(GetMessageError());
                }

                return Ok();
            }

            return BadRequest();
        }
    }
}
