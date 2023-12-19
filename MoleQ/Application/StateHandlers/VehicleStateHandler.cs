using GTA;
using MoleQ.Application.ServiceInjector;
using MoleQ.Core.Application.Interfaces.Vehicle;

namespace MoleQ.Application.StateHandlers;

/// <summary>
///     Used to update the state of the vehicle service layer.
/// </summary>
public class VehicleStateHandler : IStateHandler
{
    private readonly IVehicleService _vehicleService = Injector.VehicleService;

    public void UpdateState()
    {
        UpdateCurrentVehicle();
    }

    private void UpdateCurrentVehicle()
    {
        if (_vehicleService.CurrentVehicle != Game.Player.Character.CurrentVehicle)
            _vehicleService.CurrentVehicle = Game.Player.Character.CurrentVehicle;
    }
}