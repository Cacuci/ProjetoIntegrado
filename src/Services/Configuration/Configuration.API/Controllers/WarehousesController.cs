using Configuration.API.DTOs.Warehouse;
using Configuration.Domain;
using Core.Communication.Mediator;
using Core.Messages.CommonMessages.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Configuration.API.Controllers
{
    public class WarehousesController : ControllerBase
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IMediatorHandler _mediator;
        private readonly INotificationHandler<DomainNotification> _notification;

        public WarehousesController(IWarehouseRepository warehouseRepository, INotificationHandler<DomainNotification> notification, IMediatorHandler mediator) : base(notification, mediator)
        {
            _warehouseRepository = warehouseRepository;
            _notification = (DomainNotificationHandler)notification;
            _mediator = mediator;
        }

        // GET: api/<WarehousesController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WarehouseResponseDTO>> GetWarehouseAll(CancellationToken cancellationToken)
        {
            var warehouses = await _warehouseRepository.GetWarehouseAllAsync(cancellationToken);

            if (warehouses is not null)
            {
                var response = warehouses.Select(warehouse => new WarehouseResponseDTO(warehouse.Id, warehouse.Code, warehouse.Name));

                return Ok(response);
            }

            return NotFound();
        }

        // GET: api/<UserController>5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WarehouseResponseDTO>> GetWarehouseByID(Guid id, CancellationToken cancellationToken)
        {
            var warehouse = await _warehouseRepository.GetWarehouseByIdAsync(id, cancellationToken);

            if (warehouse is not null)
            {
                var response = new WarehouseResponseDTO(warehouse.Id, warehouse.Code, warehouse.Name);

                return Ok(response);
            }

            return NotFound();
        }

        // POST api/<WarehouseController>5
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<Guid>> CreateWarehouse([FromBody] WarehouseRequestDTO request)
        {
            if (ModelState.IsValid)
            {
                var warehouse = await _warehouseRepository.GetWarehouseByCodeAsync(request.Code);

                if (warehouse is not null)
                {
                    var warehousemodel = new Warehouse(request.Code, request.Name);

                    await _warehouseRepository.CreateWarehouseAsync(warehousemodel);

                    warehouse = await _warehouseRepository.GetWarehouseByCodeAsync(warehouse.Code);

                    return Created("api/warehouses/{id}", warehouse.Id);
                }

                return BadRequest("Deposito já cadastrada");
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> UpdateWarehouse(Guid id, [FromBody] WarehouseRequestDTO request)
        {
            if (ModelState.IsValid)
            {
                var warehouse = await _warehouseRepository.GetWarehouseByIdAsync(id);

                if (warehouse is not null)
                {
                    warehouse.UpdateName(request.Name);

                    warehouse.UpdateCode(request.Code);

                    await _warehouseRepository.UpdateAsync(warehouse);

                    return Ok(warehouse.Id);
                }

                return BadRequest("Deposito não encontrado");
            }

            return BadRequest();
        }
    }
}
