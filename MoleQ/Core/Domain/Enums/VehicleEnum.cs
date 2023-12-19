using System.ComponentModel;

namespace MoleQ.Core.Domain.Enums;

public enum VehicleEnum
{
    [Description("Repairs the vehicle")] RepairVehicle,

    [Description("Makes the vehicle indestructible")]
    Indestructible
}