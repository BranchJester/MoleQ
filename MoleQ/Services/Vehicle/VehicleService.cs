using System;
using MoleQ.Interfaces.Vehicle;

namespace MoleQ.Services.Vehicle;

public class VehicleService : IVehicleService
{
    private bool _indestructible;

    public bool Indestructible
    {
        get => _indestructible;
        set
        {
            if (_indestructible == value) return;
            _indestructible = value;
            IndestructibleChanged?.Invoke(value);
        }
    }

    public GTA.Vehicle CurrentVehicle { get; set; }

    public event Action<bool> IndestructibleChanged;

    public event Action OnRepairVehicle;

    public void RepairVehicle()
    {
        OnRepairVehicle?.Invoke();
    }
}