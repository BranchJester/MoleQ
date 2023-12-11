using GTA;
using MoleQ.Interfaces.Vehicle;
using MoleQ.ServiceInjector;

namespace MoleQ.StateHandlers;

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