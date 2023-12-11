using System.Windows.Forms;
using GTA;
using MoleQ.Constants;
using MoleQ.Enums;
using MoleQ.Interfaces.Settings;
using MoleQ.Interfaces.Vehicle;
using MoleQ.ServiceInjector;
using MoleQ.Services.Settings;

namespace MoleQ.Scripts.Vehicle;

public class VehicleSpawnerScript : BaseScript
{
    private readonly IStorageService _storageService;
    private readonly IVehicleSpawnerService _vehicleSpawnerService;

    public VehicleSpawnerScript()
    {
        _vehicleSpawnerService = Injector.VehicleSpawnerService;
        _storageService = new StorageService($"{Path.Settings}/VehicleSpawner.json");
        _vehicleSpawnerService.SpawnVehicleActivated += OnVehicleSpawned;
        KeyDown += OnKeyDown;
    }

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        var warpInSpawnedKey = HotkeysService.GetValue(SectionEnum.VehicleSpawner, VehicleSpawnerEnum.WarpInSpawned);
        if (IsKeyPressed(warpInSpawnedKey))
            _vehicleSpawnerService.WarpInSpawned = !_vehicleSpawnerService.WarpInSpawned;

        var enginesRunningKey = HotkeysService.GetValue(SectionEnum.VehicleSpawner, VehicleSpawnerEnum.EnginesRunning);
        if (IsKeyPressed(enginesRunningKey))
            _vehicleSpawnerService.EnginesRunning = !_vehicleSpawnerService.EnginesRunning;
    }

    private void OnVehicleSpawned(VehicleHash vehicleHash)
    {
        SpawnVehicle(vehicleHash);
    }

    private void SpawnVehicle(VehicleHash vehicleHash)
    {
        var vehicle = World.CreateVehicle(vehicleHash,
            Game.Player.Character.Position + Game.Player.Character.ForwardVector * 5.0f,
            Game.Player.Character.Heading * -45.0f);
        vehicle.PlaceOnGround();
        vehicle.IsPersistent = true;

        if (_vehicleSpawnerService.WarpInSpawned)
        {
            vehicle.Heading = Game.Player.Character.Heading;
            Game.Player.Character.SetIntoVehicle(vehicle, VehicleSeat.Driver);
        }

        if (_vehicleSpawnerService.EnginesRunning) vehicle.IsEngineRunning = true;
    }

    protected override void SaveSettings()
    {
        _storageService.SaveSettings(_vehicleSpawnerService);
    }
}