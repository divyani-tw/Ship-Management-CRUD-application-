namespace ShipManagement.Application.DTOs;

public sealed class CreateShipRequest
{
    public string ShipName { get; init; } = string.Empty;

    public string IMO { get; init; } = string.Empty;

    public int GrossTonnage { get; init; }
}