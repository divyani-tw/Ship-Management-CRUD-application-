using System;

namespace ShipManagement.Domain.Entities;

public sealed class Ship
{
    private Ship()
    {
        // Required by Entity Framework Core.
    }

    public Ship(
        string shipName,
        string imo,
        int grossTonnage)
    {
        Id = Guid.NewGuid();

        UpdateDetails(
            shipName,
            imo,
            grossTonnage);
    }

    public Guid Id { get; private set; }

    public string ShipName { get; private set; } = string.Empty;

    public string IMO { get; private set; } = string.Empty;

    public int GrossTonnage { get; private set; }

    public void UpdateDetails(
        string shipName,
        string imo,
        int grossTonnage)
    {
        ShipName = ValidateShipName(shipName);
        IMO = ValidateIMO(imo);
        GrossTonnage = ValidateGrossTonnage(grossTonnage);
    }

    private static string ValidateShipName(string shipName)
    {
        if (string.IsNullOrWhiteSpace(shipName))
        {
            throw new ArgumentException(
                "Ship name is required.",
                nameof(shipName));
        }

        var trimmedName = shipName.Trim();

        if (trimmedName.Length > 200)
        {
            throw new ArgumentException(
                "Ship name cannot exceed 200 characters.",
                nameof(shipName));
        }

        return trimmedName;
    }

    private static string ValidateIMO(string imo)
    {
        if (string.IsNullOrWhiteSpace(imo))
        {
            throw new ArgumentException(
                "IMO is required.",
                nameof(imo));
        }

        var trimmedIMO = imo.Trim();

        if (trimmedIMO.Length != 7 ||
            !long.TryParse(trimmedIMO, out _))
        {
            throw new ArgumentException(
                "IMO must contain exactly 7 digits.",
                nameof(imo));
        }

        return trimmedIMO;
    }

    private static int ValidateGrossTonnage(int grossTonnage)
    {
        if (grossTonnage <= 0)
        {
            throw new ArgumentException(
                "Gross tonnage must be greater than zero.",
                nameof(grossTonnage));
        }

        return grossTonnage;
    }
}