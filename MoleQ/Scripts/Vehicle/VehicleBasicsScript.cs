using System;
using System.Windows.Forms;
using GTA;
using MoleQ.Constants;
using MoleQ.Enums;
using MoleQ.Exceptions;
using MoleQ.Interfaces.Settings;
using MoleQ.Interfaces.Vehicle;
using MoleQ.ServiceInjector;
using MoleQ.Services.Settings;
using MoleQ.Settings;
using MoleQ.UI.Notification;

namespace MoleQ.Scripts.Vehicle;

public class VehicleBasicsScript : BaseScript
{
    private readonly IStorageService _storageService;
    private readonly IVehicleService _vehicleService;

    public VehicleBasicsScript()
    {
        _vehicleService = Injector.VehicleService;
        _storageService = new StorageService($"{Path.Settings}/Vehicle.json");
        _vehicleService.OnRepairVehicle += RepairVehicle;
        Tick += OnTick;
        KeyDown += OnKeyDown;
    }

    protected override void SaveSettings()
    {
        var vehicleSettings = new VehicleSettings
        {
            Indestructible = _vehicleService.Indestructible
        };
        _storageService.SaveSettings(vehicleSettings);
    }

    protected override void LoadSettings()
    {
        var settings = _storageService.LoadSettings<VehicleSettings>();
        _vehicleService.Indestructible = settings.Indestructible;
    }

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        var repairVehicleKey = HotkeysService.GetValue(SectionEnum.Vehicle, VehicleEnum.RepairVehicle);
        if (IsKeyPressed(repairVehicleKey))
            try
            {
                _vehicleService.RepairVehicle();
            }
            catch (VehicleNotFoundException ex)
            {
                Notify.ErrorMessage(ex.Message);
            }

        var indestructibleVehicleKey = HotkeysService.GetValue(SectionEnum.Vehicle, VehicleEnum.Indestructible);
        if (IsKeyPressed(indestructibleVehicleKey))
            _vehicleService.Indestructible = !_vehicleService.Indestructible;
    }

    private void RepairVehicle()
    {
        var ped = Game.Player.Character;

        if (!ped.IsInVehicle()) throw new VehicleNotFoundException();

        var vehicle = ped.CurrentVehicle;
        vehicle.Repair();
    }

    private void OnTick(object sender, EventArgs e)
    {
        Indestructible();
    }

    private void Indestructible()
    {
        if (!Game.Player.Character.IsInVehicle()) return;

        var vehicle = Game.Player.Character.CurrentVehicle;
        vehicle.IsInvincible = _vehicleService.Indestructible;
        vehicle.CanBeVisiblyDamaged = false;

        if (vehicle.Health != vehicle.MaxHealth) vehicle.Repair();
    }
}