using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ShipManagement.Application.DTOs;

namespace ShipManagement.Application.Interfaces;

public interface IShipService
{
    Task<IReadOnlyList<ShipResponse>> GetAllAsync(
        CancellationToken cancellationToken);

    Task<ShipResponse?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<ShipResponse> CreateAsync(
        CreateShipRequest request,
        CancellationToken cancellationToken);

    Task<bool> UpdateAsync(
        Guid id,
        UpdateShipRequest request,
        CancellationToken cancellationToken);

    Task<bool> DeleteAsync(
        Guid id,
        CancellationToken cancellationToken);
}