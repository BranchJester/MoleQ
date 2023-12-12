using System;
using GTA;

namespace MoleQ.Interfaces.Player;

/// <summary>
///     Used to manage the player.
/// </summary>
public interface IPlayerService
{
    /// <summary>
    ///     Makes the player invincible.
    /// </summary>
    bool Invincible { get; set; }

    /// <summary>
    ///     Allows the player to super jump.
    /// </summary>
    bool SuperJump { get; set; }

    /// <summary>
    ///     The player's wanted level.
    /// </summary>
    int WantedLevel { get; set; }

    /// <summary>
    ///     The maximum wanted level the player can have.
    /// </summary>
    int MaxWantedLevel { get; set; }

    /// <summary>
    ///     Locks the player's wanted level at the specified maximum wanted level.
    /// </summary>
    bool LockWantedLevel { get; set; }

    /// <summary>
    ///     Allows the player to have infinite stamina.
    /// </summary>
    bool InfiniteStamina { get; set; }

    /// <summary>
    ///     Represents the player's character.
    /// </summary>
    Ped Character { get; set; }

    /// <summary>
    ///     The player can no longer drown in water.
    /// </summary>
    bool InfiniteBreath { get; set; }

    bool SuperPunch { get; set; }

    event Action FixPlayerActivated;
    event Action<bool> InvincibleChanged;
    event Action<int> WantedLevelChanged;
    event Action<bool> LockWantedLevelChanged;
    event Action<bool> SuperJumpChanged;
    event Action<bool> InfiniteStaminaChanged;
    event Action<bool> InfiniteBreathChanged;
    event Action<bool> SuperPunchChanged;
    event Action<PedHash> CharacterChanged;

    /// <summary>
    ///     Fixes the player's character.
    /// </summary>
    void FixPlayer();
}