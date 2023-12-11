using System;
using System.Drawing;
using System.Windows.Forms;
using GTA;
using GTA.UI;
using MoleQ.Extensions;
using MoleQ.ServiceInjector;

namespace MoleQ.Scripts;

public class KeybindingChangeScript : BaseScript
{
    private const double DisplayDuration = 2.0; // Duration for displaying the text on the screen (in seconds)

    // Text element for displaying key press information, positioned at (500, 500) on the screen, in green color
    private readonly TextElement _displayKeyInfo = new("", new PointF(500, 500), 1.0f, Color.Green);

    // Flags and variables to manage key press state and timing
    private bool _isWaitingForKey; // Indicates whether the script is waiting for a key press
    private DateTime _lastKeyPressTime; // Stores the time of the last key press
    private bool _shouldDrawMessage; // Indicates whether the message should be drawn on the screen

    // Constructor for the script
    public KeybindingChangeScript()
    {
        KeyDown += OnKeyDown;
        Tick += OnTick;
        _shouldDrawMessage = false;
    }

    private void OnTick(object sender, EventArgs e)
    {
        // Return immediately if HotkeysService or current menu is not available
        if (HotkeysService is null) return;
        var currentMenu = Injector.MenuManager.CurrentMenu;
        if (currentMenu is null) return;

        if (_isWaitingForKey)
        {
            _shouldDrawMessage = true;
            var modifierKeys = GetModifierKeys();

            // Iterate through all possible keys
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
            {
                try
                {
                    if (Game.IsKeyPressed(key) && key != Keys.Tab && !IsModifierKey(key))
                    {
                        // Update display text and set the keybinding in the hotkeys config file through the service
                        _displayKeyInfo.Caption = $"{modifierKeys}{key}";
                        HotkeysService.SetValue(currentMenu.Name, currentMenu.SelectedItem.Title.ToEnumStyle(),
                            _displayKeyInfo.Caption);
                        _lastKeyPressTime = DateTime.Now; // Update the last key press time
                        _isWaitingForKey = false; // No longer waiting for a key
                        break;
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    // Ignoring exceptions due to potential mismatches in key enumeration and game engine keys
                }
            }
        }

        // Draw the message if required and handle display duration
        if (_shouldDrawMessage)
        {
            _displayKeyInfo.Draw(); // Draw the message on the screen
            // If the message has been displayed for the set duration, stop drawing it
            if ((DateTime.Now - _lastKeyPressTime).TotalSeconds >= DisplayDuration) _shouldDrawMessage = false;
        }
    }

    private string GetModifierKeys()
    {
        var modifiers = "";
        if (Game.IsKeyPressed(Keys.ControlKey)) modifiers += "Ctrl + ";
        if (Game.IsKeyPressed(Keys.ShiftKey)) modifiers += "Shift + ";
        if (Game.IsKeyPressed(Keys.Menu)) modifiers += "Alt + ";

        return modifiers;
    }

    private bool IsModifierKey(Keys key)
    {
        return key == Keys.ControlKey || key == Keys.ShiftKey || key == Keys.Menu;
    }

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        // Toggle key waiting state and update message when Space is pressed and a menu is open
        if (e.KeyCode == Keys.Tab && Injector.MenuManager.CurrentMenu is not null)
        {
            _isWaitingForKey = !_isWaitingForKey;
            _displayKeyInfo.Caption = "Press a key...";
            _lastKeyPressTime = DateTime.Now;
            _shouldDrawMessage = true;

            if (!_isWaitingForKey)
            {
                // Add additional logic here if needed when stopping to wait for a key
            }
        }
    }
}