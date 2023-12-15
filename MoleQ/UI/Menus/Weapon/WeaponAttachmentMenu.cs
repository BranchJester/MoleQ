using System;
using MoleQ.Extensions;
using MoleQ.Interfaces.Weapon;
using MoleQ.UI.Menus.Abstract;

namespace MoleQ.UI.Menus.Weapon;

public class WeaponAttachmentMenu : BaseMenu
{
    private readonly IWeaponService _weaponService;

    public WeaponAttachmentMenu(string menuName, IWeaponService weaponService) : base(menuName)
    {
        _weaponService = weaponService;
        Shown += OnShown;
    }

    protected override void InitializeItems()
    {
    }

    private void OnShown(object sender, EventArgs e)
    {
        Clear();
        foreach (var weaponComponent in _weaponService.CurrentWeapon.Components)
        {
            var localizedName = weaponComponent.LocalizedName;

            if (string.IsNullOrEmpty(localizedName)) localizedName = weaponComponent.ComponentHash.ToPrettyString();
        }
    }
}