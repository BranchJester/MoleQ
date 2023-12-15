using System;
using System.Collections.Generic;
using MoleQ.Interfaces.Weapon;

namespace MoleQ.Settings;

public class WeaponSettings : IServiceSettings
{
    public bool InfiniteAmmo { get; set; }
    public bool NoReload { get; set; }

    public void ApplyToServices(IDictionary<Type, object> services)
    {
        if (services.TryGetValue(typeof(IWeaponService), out var weaponService))
        {
            ((IWeaponService)weaponService).InfiniteAmmo = InfiniteAmmo;
            ((IWeaponService)weaponService).NoReload = NoReload;
        }
    }

    public void ExtractFromServices(IDictionary<Type, object> services)
    {
        if (services.TryGetValue(typeof(IWeaponService), out var weaponService))
        {
            InfiniteAmmo = ((IWeaponService)weaponService).InfiniteAmmo;
            NoReload = ((IWeaponService)weaponService).NoReload;
        }
    }
}