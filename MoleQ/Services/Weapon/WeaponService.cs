using System;
using MoleQ.Interfaces.Weapon;

namespace MoleQ.Services.Weapon;

public class WeaponService : IWeaponService
{
    private GTA.Weapon _currentWeapon;
    private bool _infiniteAmmo;
    private bool _noReload;

    public GTA.Weapon CurrentWeapon
    {
        get => _currentWeapon;
        set
        {
            _currentWeapon = value;
            CurrentWeaponChanged?.Invoke(value);
        }
    }

    public bool InfiniteAmmo
    {
        get => _infiniteAmmo;
        set
        {
            if (_infiniteAmmo == value) return;
            _infiniteAmmo = value;
            InfiniteAmmoChanged?.Invoke(value);
        }
    }

    public bool NoReload
    {
        get => _noReload;
        set
        {
            if (_noReload == value) return;
            _noReload = value;
            InfiniteAmmo = value;
            NoReloadChanged?.Invoke(value);
        }
    }

    public event Action<GTA.Weapon> CurrentWeaponChanged;

    public event Action GiveAllWeaponsActivated;
    public event Action<bool> InfiniteAmmoChanged;
    public event Action<bool> NoReloadChanged;

    public void GiveAllWeapons()
    {
        GiveAllWeaponsActivated?.Invoke();
    }
}