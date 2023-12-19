using System;
using GTA;

namespace MoleQ.Core.Application.Interfaces.Player;

/// <summary>
///     Service used to handle the player's teleportation.
/// </summary>
public interface ITeleportService
{
    event Action<BlipSprite> OnTeleportToBlip;

    /// <summary>
    ///     Teleports the player to the specified marker.
    /// </summary>
    void TeleportToBlip(BlipSprite blipSprite = BlipSprite.Waypoint);
}