using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShipManagement.Application.DTOs;
using ShipManagement.Application.Interfaces;

namespace ShipManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ShipsController : ControllerBase
{
    private readonly IShipService _shipService;

    public ShipsController(IShipService shipService)
    {
        _shipService = shipService;
    }

    [HttpGet]
    [ProducesResponseType(
        typeof(IReadOnlyList<ShipResponse>),
        StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ShipResponse>>> GetAll(
        CancellationToken cancellationToken)
    {
        var ships = await _shipService.GetAllAsync(
            cancellationToken);

        return Ok(ships);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(
        typeof(ShipResponse),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ShipResponse>> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var ship = await _shipService.GetByIdAsync(
            id,
            cancellationToken);

        if (ship is null)
        {
            return NotFound();
        }

        return Ok(ship);
    }

    [HttpPost]
    [ProducesResponseType(
        typeof(ShipResponse),
        StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ShipResponse>> Create(
        [FromBody] CreateShipRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var ship = await _shipService.CreateAsync(
                request,
                cancellationToken);

            return CreatedAtAction(
                nameof(GetById),
                new { id = ship.Id },
                ship);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(
                new
                {
                    message = exception.Message
                });
        }
        catch (InvalidOperationException exception)
        {
            return Conflict(
                new
                {
                    message = exception.Message
                });
        }
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateShipRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var updated = await _shipService.UpdateAsync(
                id,
                request,
                cancellationToken);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }
        catch (ArgumentException exception)
        {
            return BadRequest(
                new
                {
                    message = exception.Message
                });
        }
        catch (InvalidOperationException exception)
        {
            return Conflict(
                new
                {
                    message = exception.Message
                });
        }
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        Guid id,
        CancellationToken cancellationToken)
    {
        var deleted = await _shipService.DeleteAsync(
            id,
            cancellationToken);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}