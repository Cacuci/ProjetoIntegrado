using Core.Communication.Mediator;
using Core.Messages.CommonMessages.Notifications;
using Inbound.Application.Commands;
using Inbound.Application.DTOs;
using Inbound.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inbound.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderQueries _orderQueries;
        private readonly IMediatorHandler _mediatorHandler;

        public OrdersController(IOrderQueries orderQueries, INotificationHandler<DomainNotification> notification, IMediatorHandler mediatorHandler) : base(notification, mediatorHandler)
        {
            _orderQueries = orderQueries;
            _mediatorHandler = mediatorHandler;
        }

        /// <summary>
        /// Obtém a lista de ordens de entrada
        /// </summary>        
        /// <remarks>
        /// Está API pode ser usada para retornar as ordens de entrada.
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
        public async Task<ActionResult<OrderResponseDTO>> GetAllOrder(CancellationToken cancellationToken)
        {
            var warehouses = await _orderQueries.GetAllOrderAsync(cancellationToken);

            if (!warehouses.Any())
            {
                return NotFound();
            }

            return Ok(warehouses);
        }

        /// <summary>
        /// Obtém a ordem específica
        /// </summary>        
        /// <remarks>
        /// Está API pode ser usada para retornar a ordem específica.
        ///
        /// Você precisa passar o ID de identificação do depósito na URL para a chamada bem-sucedida. Nenhum outro parâmetro no corpo da requisição é necessário.
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
        public async Task<ActionResult<OrderResponseDTO>> GetOrderByID(Guid id, CancellationToken cancellationToken)
        {
            var warehouse = await _orderQueries.GetOrderByIdAsync(id, cancellationToken);

            if (warehouse is null)
            {
                return NotFound();
            }

            return Ok(warehouse);
        }

        /// <summary>
        /// Realiza o cadastro da ordem
        /// </summary>
        /// <remarks>
        /// Use esta API para realizar o cadastramento da ordem. Todos os detalhes devem ser passados no corpo da requisição.
        ///
        /// Somente usuários autenticados podem acessar este recurso.        
        /// </remarks>        
        /// <response code="201">Se a requisição foi bem sucedida</response>
        /// <response code="400">Se o servidor não entendeu a requisição</response>                      
        /// <response code="401">Se o servidor não entendeu a requisição (Usuário não identificado)</response>  
        /// <response code="403">Se o servidor não entendeu a requisição (Usuário não autorizado)</response>    
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<string>> CreateOrder([FromBody] OrderRequestDTO request, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var command = new CreateOrderCommand(request);

                await _mediatorHandler.SendCommand(command);

                if (!OperationValid())
                {
                    return BadRequest(GetMessageError());
                }

                var order = await _orderQueries.GetOrderByNumberAsync(request.Number, cancellationToken);

                return Created("api/orders/{id}", order?.Id);
            }

            return BadRequest();
        }

        /// <summary>
        /// Realiza a atualização de uma ordem expecífica
        /// </summary>
        /// <remarks>
        /// Use esta API para realizar a atualização de uma ordem expecífica. Todos os detalhes devem ser passados no corpo da requisição.
        ///
        /// Somente usuários autenticados podem acessar este recurso.        
        /// </remarks>        
        /// <response code="200">Se a requisição foi bem sucedida</response>        
        /// <response code="401">Se o servidor não entendeu a requisição (Usuário não identificado)</response>  
        /// <response code="403">Se o servidor não entendeu a requisição (Usuário não autorizado)</response>    
        /// <response code="404">Se o servidor não encontrar o que foi pedido</response>    
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> UpdateOrder(Guid id, [FromBody] OrderRequestDTO request, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var commad = new UpdateOrderCommand(id, request);

                await _mediatorHandler.SendCommand(commad);

                if (!OperationValid())
                {
                    return NotFound(GetMessageError());
                }

                var order = await _orderQueries.GetOrderByIdAsync(id, cancellationToken);

                return Ok(order?.Id);
            }

            return BadRequest();
        }
    }
}
