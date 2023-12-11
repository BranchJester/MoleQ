using System;
using MoleQ.Constants;
using MoleQ.Interfaces.Player;
using MoleQ.Interfaces.Settings;
using MoleQ.Interfaces.Vehicle;
using MoleQ.Interfaces.Weapon;
using MoleQ.Services.Player;
using MoleQ.Services.Settings;
using MoleQ.Services.Vehicle;
using MoleQ.Services.Weapon;
using MoleQ.UI.Manager;

namespace MoleQ.ServiceInjector;

/// <summary>
///     Used to inject services and repositories into other classes.
/// </summary>
public static class Injector
{
    // Services
    public static IHotkeyService HotkeysService { get; private set; } = new HotkeysService(Path.Hotkeys);

    public static IPlayerService PlayerService { get; private set; }

    public static ITeleportService TeleportService { get; private set; }

    public static ISuperRunService SuperRunService { get; private set; }

    public static IVehicleService VehicleService { get; private set; }
    public static IVehicleSpawnerService VehicleSpawnerService { get; private set; }
    public static IWeaponService WeaponService { get; private set; }

    // Repositories

    // Menu manager
    public static MenuManager MenuManager { get; private set; }

    // Events
    public static event Action ServicesInitialized;

    public static void Initialize()
    {
        /*
         * Services
         */

        // Player
        PlayerService = new PlayerService();
        TeleportService = new TeleportService();
        SuperRunService = new SuperRunService();

        // Vehicle
        VehicleService = new VehicleService();
        VehicleSpawnerService = new VehicleSpawnerService();

        // Weapon
        WeaponService = new WeaponService();

        // Repositories

        // Menu Manager
        MenuManager = new MenuManager();
    }

    public static void InvokeInitialize()
    {
        ServicesInitialized?.Invoke();
    }
}