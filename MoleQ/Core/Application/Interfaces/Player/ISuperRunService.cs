using System;
using MoleQ.Core.Domain.Enums;

namespace MoleQ.Core.Application.Interfaces.Player;

/// <summary>
///     Service used to handle the player's speed settings.
/// </summary>
public interface ISuperRunService
{
    /// <summary>
    ///     A super speed value for the player.
    /// </summary>
    PlayerSuperSpeedEnum SuperRun { get; set; }

    event Action<PlayerSuperSpeedEnum> PlayerSuperSpeedChanged;
}