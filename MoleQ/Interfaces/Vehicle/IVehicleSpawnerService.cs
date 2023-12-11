using System;
using GTA;

namespace MoleQ.Interfaces.Vehicle;

public interface IVehicleSpawnerService
{
    bool WarpInSpawned { get; set; }
    bool EnginesRunning { get; set; }
    event Action<VehicleHash> SpawnVehicleActivated;
    event Action<bool> WarpInSpawnedChanged;
    event Action<bool> EnginesRunningChanged;
    void SpawnVehicle(VehicleHash vehicleHash);
}