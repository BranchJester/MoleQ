using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using GTA;
using MoleQ.Core.Application.Interfaces.Settings;
using MoleQ.Core.Domain.Enums;

namespace MoleQ.Core.Application.Services.Settings;

public class HotkeysService : IHotkeyService
{
    private readonly ScriptSettings _config;

    public HotkeysService(string path)
    {
        _config = ScriptSettings.Load(path);
    }

    public (Keys mainKey, Keys[] modifierKeys) GetValue(SectionEnum section, Enum keyName)
    {
        return GetValueInternal(section.ToString(), keyName.ToString());
    }

    public string GetValueAsString(SectionEnum section, Enum keyName)
    {
        var value = _config.GetValue(section.ToString(), keyName.ToString(), "");
        value = value.Replace("+", " + ");
        return value;
    }

    public void SetValue(string section, string keyName, string newKey)
    {
        _config.SetValue(section, keyName, newKey);
        HotkeyChanged?.Invoke(section, keyName, newKey);
        _config.Save();
    }

    public event Action<string, string, string> HotkeyChanged;

    private (Keys mainKey, Keys[] modifierKeys) GetValueInternal(string section, string keyName)
    {
        var keyString = _config.GetValue(section, keyName, "");

        // Standardize CTRL variations to Control
        keyString = Regex.Replace(keyString, @"\bCTRL\b", "Control", RegexOptions.IgnoreCase);

        // Use Regex to split on '+' with optional spaces around it
        var keyParts = Regex.Split(keyString, @"\s*\+\s*");

        var mainKey = Keys.None;
        var modifierKeys = new List<Keys>();

        foreach (var part in keyParts)
        {
            if (Enum.TryParse(part, true, out Keys key)) // The 'true' parameter makes parsing case-insensitive
            {
                if (key == Keys.Control || key == Keys.Shift || key == Keys.Alt)
                {
                    modifierKeys.Add(key);
                }
                else
                {
                    if (mainKey == Keys.None) mainKey = key; // Set the first non-modifier key as the main key
                }
            }
        }

        return (mainKey, modifierKeys.ToArray());
    }
}