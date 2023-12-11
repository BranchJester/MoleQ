using System;

namespace MoleQ.Interfaces.Weapon;

/// <summary>
///     Service used to handle the player's weapons.
/// </summary>
public interface IWeaponService
{
    public GTA.Weapon CurrentWeapon { get; set; }
    event Action<GTA.Weapon> CurrentWeaponChanged;
    event Action GiveAllWeaponsActivated;

    /// <summary>
    ///     Gives the player all weapons.
    /// </summary>
    void GiveAllWeapons();
}