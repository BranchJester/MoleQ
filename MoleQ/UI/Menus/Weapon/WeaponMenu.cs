using MoleQ.Enums;
using MoleQ.Interfaces.Weapon;
using MoleQ.UI.Items;
using MoleQ.UI.Menus.Abstract;
using MoleQ.UI.Notification;

namespace MoleQ.UI.Menus.Weapon;

public class WeaponMenu : BaseMenu
{
    private readonly IWeaponService _weaponService;

    public WeaponMenu(string menuName, IWeaponService weaponService) : base(menuName)
    {
        _weaponService = weaponService;
    }

    protected override void InitializeItems()
    {
        GiveAllWeapons();
    }

    private void GiveAllWeapons()
    {
        var giveAllWeapons = new CustomNativeItem(WeaponEnum.GiveAllWeapons,
            HotkeysService.GetValueAsString(SectionEnum.Weapon, WeaponEnum.GiveAllWeapons));
        giveAllWeapons.Activated += (_, _) => { _weaponService.GiveAllWeapons(); };
        _weaponService.GiveAllWeaponsActivated += () => { Notify.Message("You have received all weapons."); };
        Add(giveAllWeapons);
    }
}