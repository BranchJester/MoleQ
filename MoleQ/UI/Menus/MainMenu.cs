using MoleQ.ServiceInjector;
using MoleQ.UI.Menus.Abstract;

namespace MoleQ.UI.Menus;

public class MainMenu : BaseMenu
{
    public MainMenu(string menuName) : base(menuName)
    {
    }

    protected override void InitializeItems()
    {
        AddSubMenu(Injector.MenuManager.PlayerMenu, "Menu");
        AddSubMenu(Injector.MenuManager.VehicleMenu, "Menu");
        AddSubMenu(Injector.MenuManager.WeaponMenu, "Menu");
    }
}