using System;
using GTA;
using MoleQ.Application.Extensions;
using MoleQ.Application.UI.Notification;
using MoleQ.Core.Application.Interfaces.Vehicle;
using MoleQ.UI.Items;
using MoleQ.UI.Menus.Abstract;

namespace MoleQ.UI.Menus.Vehicle;

public class VehicleClassMenu : BaseMenu
{
    private readonly VehicleClass _vehicleClass;
    private readonly IVehicleSpawnerService _vehicleSpawnerService;

    public VehicleClassMenu(string menuName, IVehicleSpawnerService vehicleSpawnerService, VehicleClass vehicleClass) :
        base(menuName)
    {
        _vehicleSpawnerService = vehicleSpawnerService;
        _vehicleClass = vehicleClass;
        Shown += OnShown;
    }

    private void OnShown(object sender, EventArgs e)
    {
        CreateItems();
    }

    private void CreateItems()
    {
        Clear();
        foreach (var vehicleHash in GTA.Vehicle.GetAllModelsOfClass(_vehicleClass))
        {
            var vehicleDisplayName = Game.GetLocalizedString((int)vehicleHash);

            if (string.IsNullOrEmpty(vehicleDisplayName)) vehicleDisplayName = vehicleHash.ToPrettyString();

            var vehicleMenuItem = new CustomNativeItem(vehicleDisplayName, $"Spawn {vehicleDisplayName}");
            vehicleMenuItem.Activated += (_, _) => { _vehicleSpawnerService.SpawnVehicle(vehicleHash); };
            Add(vehicleMenuItem);
        }

        _vehicleSpawnerService.SpawnVehicleActivated += vHash =>
        {
            var vehicleDisplayName = Game.GetLocalizedString((int)vHash);

            if (string.IsNullOrEmpty(vehicleDisplayName)) vehicleDisplayName = vHash.ToPrettyString();

            Notify.Message($"Spawned {vehicleDisplayName}");
        };
    }
}