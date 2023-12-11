using System;
using GTA;
using MoleQ.Interfaces.Vehicle;

namespace MoleQ.Services.Vehicle;

public class VehicleSpawnerService : IVehicleSpawnerService
{
    private bool _enginesRunning;
    private bool _warpInSpawned;

    public bool EnginesRunning
    {
        get => _enginesRunning;
        set
        {
            _enginesRunning = value;
            EnginesRunningChanged?.Invoke(value);
        }
    }

    public event Action<VehicleHash> SpawnVehicleActivated;
    public event Action<bool> WarpInSpawnedChanged;
    public event Action<bool> EnginesRunningChanged;

    public bool WarpInSpawned
    {
        get => _warpInSpawned;
        set
        {
            _warpInSpawned = value;
            WarpInSpawnedChanged?.Invoke(value);
        }
    }

    public void SpawnVehicle(VehicleHash vehicleHash)
    {
        SpawnVehicleActivated?.Invoke(vehicleHash);
    }
}