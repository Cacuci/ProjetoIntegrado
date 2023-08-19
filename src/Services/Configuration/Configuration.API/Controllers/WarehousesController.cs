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
    [Route("api/[controller]")]
    public class WarehousesController : ControllerBase
    {
        private readonly IWarehouseQueries _warehouseQueries;
        private readonly IMediatorHandler _mediator;

        public WarehousesController(IWarehouseQueries warehouseQueries, INotificationHandler<DomainNotification> notification, IMediatorHandler mediator) : base(notification, mediator)
        {
            _warehouseQueries = warehouseQueries;
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WarehouseResponseDTO>> GetWarehouseAll(CancellationToken cancellationToken)
        {
            var warehouses = await _warehouseQueries.GetWarehouseAllAsync(cancellationToken);

            if (!warehouses.Any())
            {
                return NotFound();
            }

            return Ok(warehouses);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WarehouseResponseDTO>> GetWarehouseByID(Guid id, CancellationToken cancellationToken)
        {
            var warehouse = await _warehouseQueries.GetWarehouseByIDAsync(id, cancellationToken);

            if (warehouse is null)
            {
                return NotFound();
            }

            return Ok(warehouse);
        }

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

                return Created("api/warehouses/{id}", warehouse.ID);
            }

            return BadRequest();
        }

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

                var warehouse = await _warehouseQueries.GetWarehouseByIDAsync(id, cancellationToken);

                return Ok(warehouse?.ID);
            }

            return BadRequest();
        }
    }
}
