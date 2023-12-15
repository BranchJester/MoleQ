using System;
using System.Collections.Generic;
using MoleQ.Interfaces.Vehicle;

namespace MoleQ.Settings;

public class VehicleSpawnerSettings : IServiceSettings
{
    public bool WarpInSpawned { get; set; }
    public bool EnginesRunning { get; set; }

    public void ApplyToServices(IDictionary<Type, object> services)
    {
        if (services.TryGetValue(typeof(IVehicleSpawnerService), out var vehicleSpawnerService))
        {
            ((IVehicleSpawnerService)vehicleSpawnerService).WarpInSpawned = WarpInSpawned;
            ((IVehicleSpawnerService)vehicleSpawnerService).EnginesRunning = EnginesRunning;
        }
    }

    public void ExtractFromServices(IDictionary<Type, object> services)
    {
        if (services.TryGetValue(typeof(IVehicleSpawnerService), out var vehicleSpawnerService))
        {
            WarpInSpawned = ((IVehicleSpawnerService)vehicleSpawnerService).WarpInSpawned;
            EnginesRunning = ((IVehicleSpawnerService)vehicleSpawnerService).EnginesRunning;
        }
    }
}