using System;
using System.Drawing;
using GTA;
using LemonUI.Menus;
using LemonUI.Scaleform;
using MoleQ.Application.Extensions;
using MoleQ.Application.ServiceInjector;
using MoleQ.Core.Application.Interfaces.Settings;

namespace MoleQ.UI.Menus.Abstract;

public abstract class BaseMenu : NativeMenu
{
    protected readonly IHotkeyService HotkeysService = Injector.HotkeysService;

    protected BaseMenu(string menuName) : base("MoleQ", menuName)
    {
        MenuName = menuName;
        Banner.Color = Color.FromArgb(255, 0, 0, 0);
        var instructionalButton = new InstructionalButton("Change Hotkey", Control.SelectWeapon);
        Buttons.Add(instructionalButton);

        Shown += OnShown;
        Closed += OnClosed;
        HotkeysService.HotkeyChanged += OnHotkeyChanged;

        Injector.ServicesInitialized += InitializeItems; // When all services are initialized, initialize the items.
    }

    public string MenuName { get; set; }

    private void OnHotkeyChanged(string section, string keyName, string newKey)
    {
        foreach (var item in Items)
        {
            if (item.Title.ToEnumStyle() == keyName)
            {
                // Split the description at the hotkey-line and replace the hotkey
                var descriptionParts = item.Description.Split(new[] { "\n\n~g~Hotkey:" }, StringSplitOptions.None);
                if (descriptionParts.Length >= 2)
                    item.Description = $"{descriptionParts[0]}\n\n~g~Hotkey: {newKey}";
            }
        }
    }

    private void OnClosed(object sender, EventArgs e)
    {
        Injector.MenuManager.CurrentMenu = null;
    }

    protected virtual void InitializeItems()
    {
    }

    private void OnShown(object sender, EventArgs e)
    {
        Injector.MenuManager.LatestMenu = this;
        Injector.MenuManager.CurrentMenu = this;
    }
}