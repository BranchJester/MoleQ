using MoleQ.Enums;
using MoleQ.Interfaces.Weapon;
using MoleQ.ServiceInjector;
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
        CreateWeaponAttachmentMenu();
        GiveAllWeapons();
        InfiniteAmmo();
        NoReload();
    }

    private void CreateWeaponAttachmentMenu()
    {
        var weaponAttachmentMenu = new WeaponAttachmentMenu("Weapon Attachment", _weaponService);
        Injector.MenuManager.MenuPool.Add(weaponAttachmentMenu);
        AddSubMenu(weaponAttachmentMenu, "Menu");
    }

    private void GiveAllWeapons()
    {
        var giveAllWeapons = new CustomNativeItem(WeaponEnum.GiveAllWeapons,
            HotkeysService.GetValueAsString(SectionEnum.Weapon, WeaponEnum.GiveAllWeapons));
        giveAllWeapons.Activated += (_, _) => { _weaponService.GiveAllWeapons(); };
        _weaponService.GiveAllWeaponsActivated += () => { Notify.Message("You have received all weapons."); };
        Add(giveAllWeapons);
    }

    private void InfiniteAmmo()
    {
        var infiniteAmmo = new CustomNativeCheckboxItem(WeaponEnum.InfiniteAmmo,
            HotkeysService.GetValueAsString(SectionEnum.Weapon, WeaponEnum.InfiniteAmmo), _weaponService.InfiniteAmmo);
        infiniteAmmo.CheckboxChanged += (_, _) => { _weaponService.InfiniteAmmo = infiniteAmmo.Checked; };
        _weaponService.InfiniteAmmoChanged += state =>
        {
            infiniteAmmo.Checked = state;
            if (_weaponService.NoReload && !state)
                _weaponService.NoReload = false;
            Notify.CheckboxMessage("Infinite Ammo", state);
        };
        Add(infiniteAmmo);
    }

    private void NoReload()
    {
        var noReload = new CustomNativeCheckboxItem(WeaponEnum.NoReload,
            HotkeysService.GetValueAsString(SectionEnum.Weapon, WeaponEnum.NoReload), _weaponService.NoReload);
        noReload.CheckboxChanged += (_, _) => { _weaponService.NoReload = noReload.Checked; };
        noReload.Enabled = _weaponService.InfiniteAmmo;
        _weaponService.NoReloadChanged += state =>
        {
            noReload.Checked = state;
            Notify.CheckboxMessage("No Reload", state);
        };
        _weaponService.InfiniteAmmoChanged += state => { noReload.Enabled = state; };
        Add(noReload);
    }
}