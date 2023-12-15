using System;
using System.Collections.Generic;
using MoleQ.Interfaces.Vehicle;

namespace MoleQ.Settings;

public class VehicleSettings : IServiceSettings
{
    public bool Indestructible { get; set; }

    public void ApplyToServices(IDictionary<Type, object> services)
    {
        if (services.TryGetValue(typeof(IVehicleService), out var vehicleService))
        {
            ((IVehicleService)vehicleService).Indestructible = Indestructible;
        }
    }

    public void ExtractFromServices(IDictionary<Type, object> services)
    {
        if (services.TryGetValue(typeof(IVehicleService), out var vehicleService))
        {
            Indestructible = ((IVehicleService)vehicleService).Indestructible;
        }
    }
}