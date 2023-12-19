using System.ComponentModel;

namespace MoleQ.Core.Domain.Enums;

public enum WeaponEnum
{
    [Description("Gives all weapons to the player.")]
    GiveAllWeapons,

    [Description("Never run out of ammo.")]
    InfiniteAmmo,

    [Description("Never reload. Infinite ammo must be enabled.")]
    NoReload
}