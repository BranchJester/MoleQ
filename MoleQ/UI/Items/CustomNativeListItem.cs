using System;
using LemonUI.Menus;
using MoleQ.Application.Extensions;

namespace MoleQ.UI.Items;

public class CustomNativeListItem<T> : NativeListItem<T>
{
    public CustomNativeListItem(Enum @enum, params T[] objects) : base(@enum.ToPrettyString(), @enum.GetDescription(),
        objects)
    {
    }
}