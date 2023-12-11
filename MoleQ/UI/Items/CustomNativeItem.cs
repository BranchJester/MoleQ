using System;
using LemonUI.Menus;
using MoleQ.Extensions;

namespace MoleQ.UI.Items;

public class CustomNativeItem : NativeItem
{
    public CustomNativeItem(string text, string description) : base(text, description)
    {
    }

    public CustomNativeItem(Enum @enum, string hotKey) : base(@enum.ToPrettyString(),
        @enum.GetDescription())
    {
        if (string.IsNullOrEmpty(hotKey))
            Description += "\n\n~g~Hotkey: ~r~None";
        else
            Description += $"\n\n~g~Hotkey: {hotKey}";
    }
}