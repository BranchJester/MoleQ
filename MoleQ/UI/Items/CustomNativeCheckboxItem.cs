using System;
using LemonUI.Menus;
using MoleQ.Extensions;

namespace MoleQ.UI.Items;

public class CustomNativeCheckboxItem : NativeCheckboxItem
{
    public CustomNativeCheckboxItem(Enum @enum, string hotKey, bool check) : base(@enum.ToPrettyString(),
        @enum.GetDescription(), check)
    {
        if (string.IsNullOrEmpty(hotKey))
            Description += "\n\n~g~Hotkey: ~r~None";
        else
            Description += $"\n\n~g~Hotkey: {hotKey}";
    }
}