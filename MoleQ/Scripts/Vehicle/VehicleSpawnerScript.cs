using GTA;
using MoleQ.Interfaces.Vehicle;
using MoleQ.ServiceInjector;

namespace MoleQ.Scripts.Vehicle;

public class VehicleSpawnerScript : BaseScript
{
    private readonly IVehicleSpawnerService _vehicleSpawnerService;

    public VehicleSpawnerScript()
    {
        _vehicleSpawnerService = Injector.VehicleSpawnerService;
        _vehicleSpawnerService.SpawnVehicleActivated += OnVehicleSpawned;
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
}