using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ShipManagement.Domain.Entities;

namespace ShipManagement.Application.Interfaces;

public interface IShipRepository
{
    Task<IReadOnlyList<Ship>> GetAllAsync(
        CancellationToken cancellationToken);

    Task<Ship?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<bool> ExistsByIMOAsync(
        string imo,
        Guid? excludingId,
        CancellationToken cancellationToken);

    Task AddAsync(
        Ship ship,
        CancellationToken cancellationToken);

    Task UpdateAsync(
        Ship ship,
        CancellationToken cancellationToken);

    Task DeleteAsync(
        Ship ship,
        CancellationToken cancellationToken);

    Task SaveChangesAsync(
        CancellationToken cancellationToken);
}