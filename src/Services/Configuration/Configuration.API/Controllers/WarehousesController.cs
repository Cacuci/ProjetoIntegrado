using Configuration.Application.Commands;
using Configuration.Application.DTOs.Warehouse;
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
    [Route("api/warehouses")]
    public class WarehousesController : ControllerBase
    {
        private readonly IWarehouseQueries _warehouseQueries;
        private readonly IMediatorHandler _mediator;

        public WarehousesController(IWarehouseQueries warehouseQueries, INotificationHandler<DomainNotification> notification, IMediatorHandler mediator) : base(notification, mediator)
        {
            _warehouseQueries = warehouseQueries;
            _mediator = mediator;
        }

        /// <summary>
        /// Obtém a lista de depósitos
        /// </summary>        
        /// <remarks>
        /// Está API pode ser usada para retornar os depósitos.
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
        public async Task<ActionResult<WarehouseResponseDTO>> GetAllWarehouse(CancellationToken cancellationToken)
        {
            var warehouses = await _warehouseQueries.GetAllWarehouseAsync(cancellationToken);

            if (!warehouses.Any())
            {
                return NotFound();
            }

            return Ok(warehouses);
        }

        /// <summary>
        /// Obtém o depósito específico
        /// </summary>        
        /// <remarks>
        /// Está API pode ser usada para retornar o depósito específico.
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
        public async Task<ActionResult<WarehouseResponseDTO>> GetWarehouseByID(Guid id, CancellationToken cancellationToken)
        {
            var warehouse = await _warehouseQueries.GetWarehouseByIdAsync(id, cancellationToken);

            if (warehouse is null)
            {
                return NotFound();
            }

            return Ok(warehouse);
        }

        /// <summary>
        /// Realiza o cadastro do depósito
        /// </summary>
        /// <remarks>
        /// Use esta API para realizar o cadastramento do depósito. Todos os detalhes devem ser passados no corpo da requisição.
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
        public async Task<ActionResult<string>> CreateWarehouse([FromBody] WarehouseRequestDTO request, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var command = new CreateWarehouseCommand(request.Code, request.Name);

                await _mediator.SendCommand(command);

                if (!OperationValid())
                {
                    return BadRequest(GetMessageError());
                }

                var warehouse = await _warehouseQueries.GetWarehouseByCodeAsync(request.Code, cancellationToken);

                return Created("api/warehouses/{id}", warehouse.Id);
            }

            return BadRequest();
        }

        /// <summary>
        /// Realiza a atualização cadastral de um depósito específico
        /// </summary>
        /// <remarks>
        /// Use esta API para realizar a atualização cadastral de um depósito específico. Todos os detalhes devem ser passados no corpo da requisição.
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
        public async Task<ActionResult> UpdateWarehouse(Guid id, [FromBody] WarehouseRequestDTO request, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var commad = new UpdateWarehouseCommand(id, request.Code, request.Name);

                await _mediator.SendCommand(commad);

                if (!OperationValid())
                {
                    return NotFound(GetMessageError());
                }

                var warehouse = await _warehouseQueries.GetWarehouseByIdAsync(id, cancellationToken);

                return Ok(warehouse?.Id);
            }

            return BadRequest();
        }
    }
}
