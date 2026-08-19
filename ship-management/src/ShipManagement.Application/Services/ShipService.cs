using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ShipManagement.Application.DTOs;
using ShipManagement.Application.Interfaces;
using ShipManagement.Domain.Entities;

namespace ShipManagement.Application.Services;

public sealed class ShipService : IShipService
{
    private readonly IShipRepository _shipRepository;

    public ShipService(IShipRepository shipRepository)
    {
        _shipRepository = shipRepository;
    }

    public async Task<IReadOnlyList<ShipResponse>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        var ships = await _shipRepository.GetAllAsync(
            cancellationToken);

        return ships
            .Select(MapToResponse)
            .ToList();
    }

    public async Task<ShipResponse?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var ship = await _shipRepository.GetByIdAsync(
            id,
            cancellationToken);

        return ship is null
            ? null
            : MapToResponse(ship);
    }

    public async Task<ShipResponse> CreateAsync(
        CreateShipRequest request,
        CancellationToken cancellationToken)
    {
        var imo = request.IMO.Trim();

        var exists = await _shipRepository.ExistsByIMOAsync(
            imo,
            null,
            cancellationToken);

        if (exists)
        {
            throw new InvalidOperationException(
                $"A ship with IMO '{imo}' already exists.");
        }

        var ship = new Ship(
            request.ShipName,
            imo,
            request.GrossTonnage);

        await _shipRepository.AddAsync(
            ship,
            cancellationToken);

        await _shipRepository.SaveChangesAsync(
            cancellationToken);

        return MapToResponse(ship);
    }

    public async Task<bool> UpdateAsync(
        Guid id,
        UpdateShipRequest request,
        CancellationToken cancellationToken)
    {
        var ship = await _shipRepository.GetByIdAsync(
            id,
            cancellationToken);

        if (ship is null)
        {
            return false;
        }

        var imo = request.IMO.Trim();

        var exists = await _shipRepository.ExistsByIMOAsync(
            imo,
            id,
            cancellationToken);

        if (exists)
        {
            throw new InvalidOperationException(
                $"A ship with IMO '{imo}' already exists.");
        }

        ship.UpdateDetails(
            request.ShipName,
            imo,
            request.GrossTonnage);

        await _shipRepository.UpdateAsync(
            ship,
            cancellationToken);

        await _shipRepository.SaveChangesAsync(
            cancellationToken);

        return true;
    }

    public async Task<bool> DeleteAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var ship = await _shipRepository.GetByIdAsync(
            id,
            cancellationToken);

        if (ship is null)
        {
            return false;
        }

        await _shipRepository.DeleteAsync(
            ship,
            cancellationToken);

        await _shipRepository.SaveChangesAsync(
            cancellationToken);

        return true;
    }

    private static ShipResponse MapToResponse(Ship ship)
    {
        return new ShipResponse
        {
            Id = ship.Id,
            ShipName = ship.ShipName,
            IMO = ship.IMO,
            GrossTonnage = ship.GrossTonnage
        };
    }
}