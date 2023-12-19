using System;
using MoleQ.Application.Managers;
using MoleQ.Constants;
using MoleQ.Core.Application.Interfaces;
using MoleQ.Core.Application.Interfaces.Player;
using MoleQ.Core.Application.Interfaces.Settings;
using MoleQ.Core.Application.Interfaces.Vehicle;
using MoleQ.Core.Application.Interfaces.Weapon;
using MoleQ.Core.Application.Services.Player;
using MoleQ.Core.Application.Services.Settings;
using MoleQ.Core.Application.Services.Vehicle;
using MoleQ.Core.Application.Services.Weapon;

namespace MoleQ.Application.ServiceInjector;

/// <summary>
///     Used to inject services and repositories into other classes.
/// </summary>
public static class Injector
{
    // Services
    public static IHotkeyService HotkeysService { get; private set; } = new HotkeysService(Path.Hotkeys);
    public static ISettingsService SettingsService { get; private set; } = new SettingsService();

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

    public static void InitializeMenuManager()
    {
        MenuManager = new MenuManager();
    }

    public static void InvokeServices()
    {
        ServicesInitialized?.Invoke();
    }
}