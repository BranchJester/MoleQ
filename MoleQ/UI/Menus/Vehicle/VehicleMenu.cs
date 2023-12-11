using MoleQ.Enums;
using MoleQ.Exceptions;
using MoleQ.Interfaces.Vehicle;
using MoleQ.ServiceInjector;
using MoleQ.UI.Items;
using MoleQ.UI.Menus.Abstract;
using MoleQ.UI.Notification;

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

    private void IndestructibleVehicle()
    {
        var indestructibleBVehicle =
            new CustomNativeCheckboxItem(VehicleEnum.Indestructible,
                HotkeysService.GetValueAsString(SectionEnum.Vehicle, VehicleEnum.Indestructible),
                _vehicleService.Indestructible);
        indestructibleBVehicle.CheckboxChanged += (_, _) =>
        {
            _vehicleService.Indestructible = indestructibleBVehicle.Checked;
        };
        _vehicleService.IndestructibleChanged += indestructible =>
        {
            indestructibleBVehicle.Checked = indestructible;
            Notify.CheckboxMessage("Indestructible Vehicle", indestructible);
        };
        Add(indestructibleBVehicle);
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
}