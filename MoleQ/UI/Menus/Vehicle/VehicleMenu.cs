using MoleQ.Application.ServiceInjector;
using MoleQ.Application.UI.Notification;
using MoleQ.Core.Application.Interfaces.Vehicle;
using MoleQ.Core.Domain.Enums;
using MoleQ.Core.Domain.Exceptions;
using MoleQ.UI.Items;
using MoleQ.UI.Menus.Abstract;

namespace MoleQ.UI.Menus.Vehicle;

public class VehicleMenu : BaseMenu
{
    private readonly IVehicleService _vehicleService;

    public VehicleMenu(string menuName, IVehicleService vehicleService) : base(menuName)
    {
        _vehicleService = vehicleService;
    }

    protected override void InitializeItems()
    {
        AddSubMenu(Injector.MenuManager.VehicleSpawnerMenu, "Menu");
        RepairVehicle();
        IndestructibleVehicle();
    }

    private void RepairVehicle()
    {
        var repairVehicle = new CustomNativeItem(VehicleEnum.RepairVehicle,
            HotkeysService.GetValueAsString(SectionEnum.Vehicle, VehicleEnum.RepairVehicle));
        repairVehicle.Activated += (_, _) =>
        {
            try
            {
                _vehicleService.RepairVehicle();
            }
            catch (VehicleNotFoundException e)
            {
                Notify.ErrorMessage(e.Message);
            }
        };
        _vehicleService.OnRepairVehicle += () => Notify.Message("Vehicle Repaired");
        Add(repairVehicle);
    }

    private void IndestructibleVehicle()
    {
        var indestructibleBVehicle =
            new CustomNativeCheckboxItem(VehicleEnum.Indestructible,
                HotkeysService.GetValueAsString(SectionEnum.Vehicle, VehicleEnum.Indestructible),
                _vehicleService.Indestructible, state => { _vehicleService.Indestructible = state; });
        _vehicleService.IndestructibleChanged += indestructible =>
        {
            indestructibleBVehicle.Checked = indestructible;
            Notify.CheckboxMessage("Indestructible Vehicle", indestructible);
        };
        Add(indestructibleBVehicle);
    }
}