using System;

namespace MoleQ.Interfaces.Weapon;

/// <summary>
///     Service used to handle the player's weapons.
/// </summary>
public interface IWeaponService
{
    public GTA.Weapon CurrentWeapon { get; set; }
    bool InfiniteAmmo { get; set; }
    bool NoReload { get; set; }
    event Action<GTA.Weapon> CurrentWeaponChanged;
    event Action GiveAllWeaponsActivated;
    event Action<bool> InfiniteAmmoChanged;

    event Action<bool> NoReloadChanged;

    /// <summary>
    ///     Gives the player all weapons.
    /// </summary>
    void GiveAllWeapons();
}