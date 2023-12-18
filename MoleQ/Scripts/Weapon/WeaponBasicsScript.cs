using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GTA;
using GTA.Native;
using MoleQ.Constants;
using MoleQ.Enums;
using MoleQ.Interfaces.Weapon;
using MoleQ.Mappers;
using MoleQ.Repositories;
using MoleQ.ServiceInjector;
using MoleQ.Settings;

namespace MoleQ.Scripts.Weapon;

public class WeaponBasicsScript : BaseScript
{
    private readonly IStorageRepository _storageRepository;
    private readonly IWeaponService _weaponService;

    public WeaponBasicsScript()
    {
        _storageRepository = new StorageRepository($"{Path.Settings}/Weapon.json");
        _weaponService = Injector.WeaponService;
        _weaponService.GiveAllWeaponsActivated += GiveAllWeapons;
        _weaponService.CurrentWeaponChanged += OnWeaponChange;
        Tick += OnTick;
        KeyDown += OnKeyDown;
    }

    protected override void SaveSettings()
    {
        var settings = ServiceSettingsMapper.ExtractSettings<WeaponSettings>(new Dictionary<Type, object>
        {
            { typeof(IWeaponService), _weaponService }
        });
        _storageRepository.SaveSettings(settings);
    }

    protected override void LoadSettings()
    {
        var settings = _storageRepository.LoadSettings<WeaponSettings>();
        var services = new Dictionary<Type, object>
        {
            { typeof(IWeaponService), _weaponService }
        };
        ServiceSettingsMapper.ApplySettings(settings, services);
    }

    private void OnWeaponChange(GTA.Weapon currentWeapon)
    {
        NoReload(currentWeapon);
    }

    private void NoReload(GTA.Weapon currentWeapon)
    {
        currentWeapon.InfiniteAmmo = _weaponService.NoReload;
        currentWeapon.InfiniteAmmoClip = _weaponService.NoReload;
    }

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        var giveAllWeaponsKey = HotkeysService.GetValue(SectionEnum.Weapon, WeaponEnum.GiveAllWeapons);
        if (IsKeyPressed(giveAllWeaponsKey))
            _weaponService.GiveAllWeapons();

        var infiniteAmmoKey = HotkeysService.GetValue(SectionEnum.Weapon, WeaponEnum.InfiniteAmmo);
        if (IsKeyPressed(infiniteAmmoKey))
            _weaponService.InfiniteAmmo = !_weaponService.InfiniteAmmo;

        var noReloadKey = HotkeysService.GetValue(SectionEnum.Weapon, WeaponEnum.NoReload);
        if (IsKeyPressed(noReloadKey))
            _weaponService.NoReload = !_weaponService.NoReload;
    }

    private void GiveAllWeapons()
    {
        foreach (WeaponHash weaponHash in Enum.GetValues(typeof(WeaponHash)))
        {
            Game.Player.Character.Weapons.Give(weaponHash, 0, true, true);
            Game.Player.Character.Weapons.Current.Ammo = Game.Player.Character.Weapons.Current.MaxAmmo;
        }
    }

    private void OnTick(object sender, EventArgs e)
    {
        InfiniteAmmo();
        RefillAmmoInstantly();
    }

    private void RefillAmmoInstantly()
    {
        if (!_weaponService.NoReload) return;
        if (!Game.Player.Character.IsShooting) return;
        Function.Call(Hash.REFILL_AMMO_INSTANTLY, Game.Player.Character);
    }

    private void InfiniteAmmo()
    {
        if (!_weaponService.InfiniteAmmo) return;
        if (_weaponService.NoReload) return;
        if (Game.Player.Character.Weapons.Current.AmmoInClip > 1) return;

        Game.Player.Character.Weapons.Current.AmmoInClip = Game.Player.Character.Weapons.Current.MaxAmmoInClip;
    }
}