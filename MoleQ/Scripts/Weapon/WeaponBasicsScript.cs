using System;
using System.Windows.Forms;
using GTA;
using MoleQ.Enums;
using MoleQ.Interfaces.Weapon;
using MoleQ.ServiceInjector;

namespace MoleQ.Scripts.Weapon;

public class WeaponBasicsScript : BaseScript
{
    private readonly IWeaponService _weaponService;

    public WeaponBasicsScript()
    {
        _weaponService = Injector.WeaponService;
        _weaponService.GiveAllWeaponsActivated += GiveAllWeapons;
        Tick += OnTick;
        KeyDown += OnKeyDown;
    }

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        var giveAllWeaponsKey = HotkeysService.GetValue(SectionEnum.Weapon, WeaponEnum.GiveAllWeapons);
        if (IsKeyPressed(giveAllWeaponsKey))
            _weaponService.GiveAllWeapons();
    }

    private void GiveAllWeapons()
    {
        foreach (WeaponHash weaponHash in Enum.GetValues(typeof(WeaponHash)))
        {
            Game.Player.Character.Weapons.Give(weaponHash, 9999, true, true);
        }
    }

    private void OnTick(object sender, EventArgs e)
    {
    }
}