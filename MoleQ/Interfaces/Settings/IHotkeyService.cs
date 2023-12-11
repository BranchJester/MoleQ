using System;
using System.Windows.Forms;
using MoleQ.Enums;

namespace MoleQ.Interfaces.Settings;

/// <summary>
///     Service used to handle the hotkeys.
/// </summary>
public interface IHotkeyService
{
    /// <summary>
    ///     Gets the value of the specified hotkey.
    /// </summary>
    /// <param name="section">The section to look up the hotkey in.</param>
    /// <param name="keyName">The key name to look up.</param>
    /// <returns>The value of the specified hotkey.</returns>
    (Keys mainKey, Keys[] modifierKeys) GetValue(SectionEnum section, Enum keyName);

    /// <summary>
    ///     Gets the value of the specified hotkey as a string representation.
    /// </summary>
    /// <param name="section">The section to look up the hotkey in.</param>
    /// <param name="keyName">The key name to look up.</param>
    /// <returns>The value of the specified hotkey as a string representation.</returns>
    string GetValueAsString(SectionEnum section, Enum keyName);

    /// <summary>
    ///     Sets the value of the specified hotkey.
    /// </summary>
    /// <param name="section">The section to look up the hotkey in.</param>
    /// <param name="keyName">The key name to look up.</param>
    /// <param name="newKey">The new key to set.</param>
    void SetValue(string section, string keyName, string newKey);

    event Action<string, string, string> HotkeyChanged;
}