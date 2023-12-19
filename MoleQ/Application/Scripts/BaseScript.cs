using System;
using System.Linq;
using System.Windows.Forms;
using GTA;
using MoleQ.Application.ServiceInjector;
using MoleQ.Application.UI.Notification;
using MoleQ.Core.Application.Interfaces;
using MoleQ.Core.Application.Interfaces.Settings;
using MoleQ.Core.Domain.Enums;
using Control = System.Windows.Forms.Control;

namespace MoleQ.Application.Scripts;

/// <summary>
///     Used as a base for all scripts.
/// </summary>
public class BaseScript : Script
{
    protected readonly IHotkeyService HotkeysService = Injector.HotkeysService;

    protected readonly ISettingsService SettingsService = Injector.SettingsService;


    public BaseScript()
    {
        KeyDown += OnKeyDown;
        SettingsService.SaveSettingsActivated += SaveSettings;
        SettingsService.LoadSettingsActivated += LoadSettings;
        Tick += OnTick;
        Aborted += OnAbort;
    }

    private void OnTick(object sender, EventArgs e)
    {
        if (SettingsService.AutoSave && Game.IsPaused)
            SaveSettings();
    }

    private void OnAbort(object sender, EventArgs e)
    {
        if (SettingsService.AutoSave)
            SaveSettings();
    }

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        var saveSettingsKey = HotkeysService.GetValue(SectionEnum.Settings, SettingsEnum.SaveSettings);
        if (IsKeyPressed(saveSettingsKey)) SaveSettings();

        var loadSettingsKey = HotkeysService.GetValue(SectionEnum.Settings, SettingsEnum.LoadSettings);
        if (IsKeyPressed(loadSettingsKey)) LoadSettings();
    }

    protected virtual void SaveSettings()
    {
        Notify.Message("Settings Saved");
    }

    protected virtual void LoadSettings()
    {
        Notify.Message("Settings Loaded");
    }

    protected bool IsKeyPressed((Keys mainKey, Keys[] modifierKeys) keys)
    {
        // Check if the main key is currently pressed.
        var isMainKeyPressed = Game.IsKeyPressed(keys.mainKey);
        if (!isMainKeyPressed) return false; // If the main key is not pressed, return false.

        // Get the current state of the modifier keys (like Ctrl, Shift, Alt).
        var currentModifiers = Control.ModifierKeys;

        // Check if any required modifier key is not currently pressed.
        if (keys.modifierKeys.Any(key =>
                !currentModifiers.HasFlag(key)))
            return false; // If any required modifier key is not pressed, return false.

        // Define a list of valid modifier keys.
        var validModifiers = new[] { Keys.Control, Keys.Shift, Keys.Alt };

        // Check if all valid modifiers are either not required or currently pressed.
        return validModifiers.All(mod => !currentModifiers.HasFlag(mod) || keys.modifierKeys.Contains(mod));
    }
}