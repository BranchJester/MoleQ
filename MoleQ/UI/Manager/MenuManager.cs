﻿using LemonUI;
using LemonUI.Menus;
using MoleQ.ServiceInjector;
using MoleQ.UI.Menus;
using MoleQ.UI.Menus.Player;
using MoleQ.UI.Menus.Vehicle;
using MoleQ.UI.Menus.Weapon;

namespace MoleQ.UI.Manager;

public class MenuManager
{
    public MenuManager()
    {
        MenuPool = new ObjectPool();
        MainMenu = new MainMenu("Main");
        PlayerMenu = new PlayerMenu("Player", Injector.PlayerService, Injector.SuperRunService);
        VehicleMenu = new VehicleMenu("Vehicle", Injector.VehicleService);
        VehicleSpawnerMenu = new VehicleSpawnerMenu("Vehicle Spawner", Injector.VehicleSpawnerService);
        TeleportMenu = new TeleportMenu("Teleport", Injector.TeleportService);
        WeaponMenu = new WeaponMenu("Weapon", Injector.WeaponService);

        MenuPool.Add(MainMenu);
        MenuPool.Add(PlayerMenu);
        MenuPool.Add(VehicleMenu);
        MenuPool.Add(VehicleSpawnerMenu);
        MenuPool.Add(TeleportMenu);
        MenuPool.Add(WeaponMenu);
    }

    public ObjectPool MenuPool { get; }
    public MainMenu MainMenu { get; }
    public PlayerMenu PlayerMenu { get; }
    public VehicleMenu VehicleMenu { get; }
    public TeleportMenu TeleportMenu { get; }
    public WeaponMenu WeaponMenu { get; }

    public NativeMenu LatestMenu { get; set; }
    public NativeMenu CurrentMenu { get; set; }

    public NativeItem SelectedItem { get; set; }
    public NativeMenu VehicleSpawnerMenu { get; set; }
}