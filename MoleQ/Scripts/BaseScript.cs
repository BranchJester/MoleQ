﻿using System.Linq;
using System.Windows.Forms;
using GTA;
using MoleQ.Interfaces.Settings;
using MoleQ.ServiceInjector;
using MoleQ.UI.Notification;
using Control = System.Windows.Forms.Control;

namespace MoleQ.Scripts;

/// <summary>
///     Used as a base for all scripts.
/// </summary>
public abstract class BaseScript : Script
{
    protected readonly IHotkeyService HotkeysService = Injector.HotkeysService;

    public BaseScript()
    {
        KeyDown += OnKeyDown;
    }

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.S && e.Control && e.Shift) SaveSettings();

        if (e.KeyCode == Keys.L && e.Control && e.Shift) LoadSettings();
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