using System;
using MoleQ.Enums;

namespace MoleQ.Interfaces.Player;

/// <summary>
///     Service used to handle the player's speed settings.
/// </summary>
public interface ISuperRunService
{
    /// <summary>
    ///     A super speed value for the player.
    /// </summary>
    PlayerSuperSpeedEnum PlayerSuperSpeed { get; set; }

    event Action<PlayerSuperSpeedEnum> PlayerSuperSpeedChanged;
}