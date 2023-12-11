using System.ComponentModel;

namespace MoleQ.Enums;

public enum VehicleEnum
{
    [Description("Repairs the vehicle")] RepairVehicle,

    [Description("Makes the vehicle indestructible")]
    Indestructible
}