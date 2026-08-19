using System;

namespace ShipManagement.Application.DTOs;

public sealed class ShipResponse
{
    public Guid Id { get; init; }

    public string ShipName { get; init; } = string.Empty;

    public string IMO { get; init; } = string.Empty;

    public int GrossTonnage { get; init; }
}