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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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

        //[HttpGet("{code}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult<ProductResponseDTO>> GetByCodeAsync(string number, CancellationToken cancellationToken)
        //{
        //    var warehouse = await _materialQueries.GetByCodeAsync(number, cancellationToken);

        //    if (warehouse is null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(warehouse);
        //}

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<string>> Update([FromBody] IEnumerable<ProductRequestDTO> request, CancellationToken cancellationToken)
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
