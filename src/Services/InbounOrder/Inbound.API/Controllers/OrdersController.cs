﻿using Core.Communication.Mediator;
using Core.Messages.CommonMessages.Notifications;
using Inbound.Application.Commands;
using Inbound.Application.Queries;
using Inbound.Application.Queries.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inbound.API.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderQueries _orderQueries;
        private readonly IMediatorHandler _mediator;

        public OrdersController(IOrderQueries orderQueries, INotificationHandler<DomainNotification> notification, IMediatorHandler mediator) : base(notification, mediator)
        {
            _orderQueries = orderQueries;
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderResponseDTO>> GetOrderAll(CancellationToken cancellationToken)
        {
            var warehouses = await _orderQueries.GetorderAllAsync(cancellationToken);

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
        public async Task<ActionResult<OrderResponseDTO>> GetOrderByID(Guid id, CancellationToken cancellationToken)
        {
            var warehouse = await _orderQueries.GetOrderByIdAsync(id, cancellationToken);

            if (warehouse is null)
            {
                return NotFound();
            }

            return Ok(warehouse);
        }

        //[HttpGet("{number}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult<OrderResponseDTO>> GetOrderByNumber(string number, CancellationToken cancellationToken)
        //{
        //    var warehouse = await _orderQueries.GetOrderByNumberAsync(number, cancellationToken);

        //    if (warehouse is null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(warehouse);
        //}

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

                await _mediator.SendCommand(command);

                if (!OperationValid())
                {
                    return BadRequest(GetMessageError());
                }

                var order = await _orderQueries.GetOrderByNumberAsync(request.Number, cancellationToken);

                return Created("api/orders/{id}", order?.Id);
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> UpdateWarehouse(Guid id, [FromBody] OrderRequestDTO request, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var commad = new UpdateOrderCommand(id, request);

                await _mediator.SendCommand(commad);

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