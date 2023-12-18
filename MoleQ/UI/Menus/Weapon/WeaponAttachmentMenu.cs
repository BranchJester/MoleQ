using System;
using System.Collections.Generic;
using System.Linq;
using GTA;
using LemonUI.Menus;
using MoleQ.Extensions;
using MoleQ.Interfaces.Weapon;
using MoleQ.UI.Menus.Abstract;

namespace MoleQ.UI.Menus.Weapon;

public class WeaponAttachmentMenu : BaseMenu
{
    private readonly IWeaponService _weaponService;

    public WeaponAttachmentMenu(string menuName, IWeaponService weaponService) : base(menuName)
    {
        _weaponService = weaponService;
        Shown += OnShown;
    }

    private void OnShown(object sender, EventArgs e)
    {
        Clear();
        var currentWeapon = _weaponService.CurrentWeapon;
        if (currentWeapon == null) return;
        Dictionary<string, List<WeaponComponent>> weaponComponents = new();

        // Create a dictionary to store the default selected items for each category.
        var defaultSelectedItems = new Dictionary<string, string>();

        foreach (var weaponComponent in currentWeapon.Components)
        {
            if (weaponComponent.AttachmentPoint == WeaponAttachmentPoint.Invalid) continue;

            var categoryKey = GetCategoryKeyForAttachmentPoint(weaponComponent.AttachmentPoint);
            if (string.IsNullOrEmpty(categoryKey)) continue;

            if (!weaponComponents.ContainsKey(categoryKey))
                weaponComponents.Add(categoryKey, new List<WeaponComponent>());

            weaponComponents[categoryKey].Add(weaponComponent);

            // Check if this component is active, and if so, set it as the default selected item for the category.
            if (weaponComponent.Active)
                defaultSelectedItems[categoryKey] = !string.IsNullOrEmpty(weaponComponent.LocalizedName)
                    ? weaponComponent.LocalizedName
                    : weaponComponent.ComponentHash.ToPrettyString();
        }

        foreach (var weaponComponent in weaponComponents)
        {
            var item = new NativeListItem<string>(weaponComponent.Key, weaponComponent.Value.Select(c =>
            {
                if (!string.IsNullOrEmpty(c.LocalizedName))
                    return c.LocalizedName;
                return c.ComponentHash.ToPrettyString();
            }).ToArray());

            // Set the default selected item for this category.
            if (defaultSelectedItems.TryGetValue(weaponComponent.Key, out var defaultSelectedItem))
                item.SelectedItem = defaultSelectedItem;

            item.ItemChanged += (itemSender, i) =>
            {
                // Update the actual weapon component when an item is selected.
                var selectedComponent = weaponComponent.Value.FirstOrDefault(c =>
                    (string.IsNullOrEmpty(c.LocalizedName)
                        ? c.ComponentHash.ToPrettyString()
                        : c.LocalizedName) == item.SelectedItem);

                if (selectedComponent != null)
                {
                    // Deactivate all components in the category.
                    foreach (var component in weaponComponent.Value)
                    {
                        component.Active = false;
                    }

                    // Activate the selected component.
                    selectedComponent.Active = true;
                }
            };
            Add(item);
        }
    }

    private string GetCategoryKeyForAttachmentPoint(WeaponAttachmentPoint attachmentPoint)
    {
        // Map attachment points to category keys.
        switch (attachmentPoint)
        {
            case WeaponAttachmentPoint.Clip:
            case WeaponAttachmentPoint.Clip2:
                return "Clip";

            case WeaponAttachmentPoint.Grip:
            case WeaponAttachmentPoint.Grip2:
                return "Grip";

            case WeaponAttachmentPoint.Rail:
            case WeaponAttachmentPoint.Rail2:
                return "Rail";

            case WeaponAttachmentPoint.Scope:
            case WeaponAttachmentPoint.Scope2:
                return "Scope";

            case WeaponAttachmentPoint.Supp:
            case WeaponAttachmentPoint.Supp2:
                return "Supp";

            case WeaponAttachmentPoint.FlashLaser:
            case WeaponAttachmentPoint.FlashLaser2:
                return "FlashLaser";

            case WeaponAttachmentPoint.GunRoot:
                return "GunRoot";

            case WeaponAttachmentPoint.TorchBulb:
                return "TorchBulb";

            case WeaponAttachmentPoint.Barrel:
                return "Barrel";

            default:
                return null;
        }
    }
}