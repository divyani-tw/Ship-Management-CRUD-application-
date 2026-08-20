using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShipManagement.Application.Interfaces;
using ShipManagement.Domain.Entities;

namespace ShipManagement.Infrastructure.Persistence;

public sealed class ShipRepository : IShipRepository
{
    private readonly ShipManagementDbContext _dbContext;

    public ShipRepository(
        ShipManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<Ship>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await _dbContext.Ships
            .AsNoTracking()
            .OrderBy(ship => ship.ShipName)
            .ToListAsync(cancellationToken);
    }

    public async Task<Ship?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await _dbContext.Ships
            .FirstOrDefaultAsync(
                ship => ship.Id == id,
                cancellationToken);
    }

    public async Task<bool> ExistsByIMOAsync(
        string imo,
        Guid? excludingId,
        CancellationToken cancellationToken)
    {
        var query = _dbContext.Ships
            .AsNoTracking()
            .Where(ship => ship.IMO == imo);

        if (excludingId.HasValue)
        {
            query = query.Where(
                ship => ship.Id != excludingId.Value);
        }

        return await query.AnyAsync(cancellationToken);
    }

    public async Task AddAsync(
        Ship ship,
        CancellationToken cancellationToken)
    {
        await _dbContext.Ships.AddAsync(
            ship,
            cancellationToken);
    }

    public Task UpdateAsync(
        Ship ship,
        CancellationToken cancellationToken)
    {
        _dbContext.Ships.Update(ship);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(
        Ship ship,
        CancellationToken cancellationToken)
    {
        _dbContext.Ships.Remove(ship);

        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(
            cancellationToken);
    }
}