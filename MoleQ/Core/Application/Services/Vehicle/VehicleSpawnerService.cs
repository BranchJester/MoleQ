using System;
using GTA;
using MoleQ.Core.Application.Interfaces.Vehicle;

namespace MoleQ.Core.Application.Services.Vehicle;

public class VehicleSpawnerService : IVehicleSpawnerService
{
    private bool _enginesRunning;
    private bool _warpInSpawned;

    public bool EnginesRunning
    {
        get => _enginesRunning;
        set
        {
            if (_enginesRunning == value) return;
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
            if (_warpInSpawned == value) return;
            _warpInSpawned = value;
            WarpInSpawnedChanged?.Invoke(value);
        }
    }

    public void SpawnVehicle(VehicleHash vehicleHash)
    {
        SpawnVehicleActivated?.Invoke(vehicleHash);
    }
}