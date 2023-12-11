using System;

namespace MoleQ.Interfaces.Vehicle;

/// <summary>
///     Service used to handle the player's vehicle.
/// </summary>
public interface IVehicleService
{
    /// <summary>
    ///     Gets or sets a value indicating whether the vehicle is indestructible.
    /// </summary>
    bool Indestructible { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether the vehicle is invincible.
    /// </summary>
    GTA.Vehicle CurrentVehicle { get; set; }

    event Action<bool> IndestructibleChanged;

    event Action OnRepairVehicle;

    /// <summary>
    ///     Repairs the player's vehicle.
    /// </summary>
    void RepairVehicle();
}