using System;
using System.Windows.Forms;
using MoleQ.Enums;
using MoleQ.ServiceInjector;
using MainMenu = MoleQ.UI.Menus.MainMenu;

namespace MoleQ.Scripts;

public class Main : BaseScript
{
    private readonly MainMenu _mainMenu;

    public Main()
    {
        // Initialize must be called before any other actions.
        Injector.Initialize();

        _mainMenu = Injector.MenuManager.MainMenu;

        // Invoke the initialize method to initialize all services and repositories.
        // When this is called, all services and repositories are initialized and the menu manager is ready to be used.
        Injector.InvokeInitialize();

        Tick += OnTick;
        KeyDown += OnKeyDown;
    }

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        var menuKey = HotkeysService.GetValue(SectionEnum.Menu, MenuEnum.Toggle);
        if (IsKeyPressed(menuKey))
        {
            var latestMenu = Injector.MenuManager.LatestMenu;
            if (latestMenu == null)
                _mainMenu.Visible = !_mainMenu.Visible;
            else
                latestMenu.Visible = !latestMenu.Visible;
        }
    }

    private void OnTick(object sender, EventArgs e)
    {
        Injector.MenuManager.MenuPool.Process();
    }
}