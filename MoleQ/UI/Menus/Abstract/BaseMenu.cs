﻿using System;
using System.Drawing;
using GTA;
using LemonUI.Menus;
using LemonUI.Scaleform;
using MoleQ.Extensions;
using MoleQ.Interfaces.Settings;
using MoleQ.ServiceInjector;

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

        Injector.ServicesInitialized +=
            InitializeItems; // Initializes all menu items after the services have been initialized.
        Shown += OnShown;
        Closed += OnClosed;
        HotkeysService.HotkeyChanged += OnHotkeyChanged;
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

    protected abstract void InitializeItems();

    private void OnShown(object sender, EventArgs e)
    {
        Injector.MenuManager.LatestMenu = this;
        Injector.MenuManager.CurrentMenu = this;
    }
}