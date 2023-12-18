using System;
using MoleQ.Constants;
using MoleQ.Interfaces.Player;
using MoleQ.Interfaces.Settings;
using MoleQ.Interfaces.Vehicle;
using MoleQ.Interfaces.Weapon;
using MoleQ.Managers;
using MoleQ.Services.Player;
using MoleQ.Services.Settings;
using MoleQ.Services.Vehicle;
using MoleQ.Services.Weapon;

namespace MoleQ.ServiceInjector;

/// <summary>
///     Used to inject services and repositories into other classes.
/// </summary>
public static class Injector
{
    // Services
    public static IHotkeyService HotkeysService { get; private set; } = new HotkeysService(Path.Hotkeys);
    public static SettingsService SettingsService { get; private set; } = new();

    public static IPlayerService PlayerService { get; private set; } = new PlayerService();

    public static ITeleportService TeleportService { get; private set; } = new TeleportService();

    public static ISuperRunService SuperRunService { get; private set; } = new SuperRunService();

    public static ISuperPunchService SuperPunchService { get; private set; } = new SuperPunchService();
    public static IVehicleService VehicleService { get; private set; } = new VehicleService();
    public static IVehicleSpawnerService VehicleSpawnerService { get; private set; } = new VehicleSpawnerService();

    public static IWeaponService WeaponService { get; private set; } = new WeaponService();
    // public static ISettingsService SettingsService { get; private set; }


    // Repositories


    // Menu manager
    public static MenuManager MenuManager { get; private set; }

    // // Events
    public static event Action ServicesInitialized;

    public static void InitializeMenus()
    {
        MenuManager = new MenuManager();
    }

    public static void InvokeInitialize()
    {
        ServicesInitialized?.Invoke();
    }
}