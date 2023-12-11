using System;
using MoleQ.Interfaces.Weapon;
using Newtonsoft.Json;

namespace MoleQ.Services.Weapon;

public class WeaponService : IWeaponService
{
    private GTA.Weapon _currentWeapon;

    [JsonIgnore]
    public GTA.Weapon CurrentWeapon
    {
        get => _currentWeapon;
        set
        {
            _currentWeapon = value;
            CurrentWeaponChanged?.Invoke(value);
        }
    }

    public event Action<GTA.Weapon> CurrentWeaponChanged;

    public event Action GiveAllWeaponsActivated;

    public void GiveAllWeapons()
    {
        GiveAllWeaponsActivated?.Invoke();
    }
}