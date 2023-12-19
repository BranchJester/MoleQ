using System;
using GTA;
using MoleQ.Application.Extensions;
using MoleQ.Application.ServiceInjector;
using MoleQ.Application.UI.Items;
using MoleQ.Application.UI.Menus.Abstract;
using MoleQ.Application.UI.Notification;
using MoleQ.Core.Application.Interfaces.Vehicle;
using MoleQ.Core.Domain.Enums;

namespace MoleQ.Application.UI.Menus.Vehicle;

public class VehicleSpawnerMenu : BaseMenu
{
    private readonly IVehicleSpawnerService _vehicleSpawnerService;

    public VehicleSpawnerMenu(string menuName, IVehicleSpawnerService vehicleSpawnerService) : base(menuName)
    {
        _vehicleSpawnerService = vehicleSpawnerService;
    }

    protected override void InitializeItems()
    {
        WarpInSpawned();
        EnginesRunning();
        CreateVehicleClassMenus();
    }

    private void WarpInSpawned()
    {
        var warpInSpawned = new CustomNativeCheckboxItem(VehicleSpawnerEnum.WarpInSpawned,
            HotkeysService.GetValueAsString(SectionEnum.VehicleSpawner, VehicleSpawnerEnum.WarpInSpawned),
            _vehicleSpawnerService.WarpInSpawned);
        warpInSpawned.CheckboxChanged += (_, _) => { _vehicleSpawnerService.WarpInSpawned = warpInSpawned.Checked; };
        _vehicleSpawnerService.WarpInSpawnedChanged += warpInSpawnedChanged =>
        {
            warpInSpawned.Checked = warpInSpawnedChanged;
            Notify.CheckboxMessage("Warp In Spawned", warpInSpawnedChanged);
        };
        Add(warpInSpawned);
    }

    private void EnginesRunning()
    {
        var enginesRunning = new CustomNativeCheckboxItem(VehicleSpawnerEnum.EnginesRunning,
            HotkeysService.GetValueAsString(SectionEnum.VehicleSpawner, VehicleSpawnerEnum.EnginesRunning),
            _vehicleSpawnerService.EnginesRunning);
        enginesRunning.CheckboxChanged += (_, _) => { _vehicleSpawnerService.EnginesRunning = enginesRunning.Checked; };
        _vehicleSpawnerService.EnginesRunningChanged += enginesRunningChanged =>
        {
            enginesRunning.Checked = enginesRunningChanged;
            Notify.CheckboxMessage("Engines Running", enginesRunningChanged);
        };
        Add(enginesRunning);
    }

    private void CreateVehicleClassMenus()
    {
        foreach (VehicleClass vehicleClass in Enum.GetValues(typeof(VehicleClass)))
        {
            var vehicleClassMenu =
                new VehicleClassMenu(vehicleClass.ToPrettyString(), _vehicleSpawnerService, vehicleClass);
            Injector.MenuManager.MenuPool.Add(vehicleClassMenu);
            AddSubMenu(vehicleClassMenu, "Menu");
        }
    }
}