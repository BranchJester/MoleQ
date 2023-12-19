﻿using MoleQ.Application.UI.Items;
using MoleQ.Application.UI.Menus.Abstract;
using MoleQ.Core.Application.Interfaces;
using MoleQ.Core.Domain.Enums;

namespace MoleQ.Application.UI.Menus;

public class SettingsMenu : BaseMenu
{
    private readonly ISettingsService _settingsService;

    public SettingsMenu(string menuName, ISettingsService settingsService) : base(menuName)
    {
        _settingsService = settingsService;
        SaveSettings();
        LoadSettings();
        AutoSave();
        // AutoLoad();
    }

    private void AutoLoad()
    {
        var autoLoad = new CustomNativeCheckboxItem(SettingsEnum.AutoLoad,
            HotkeysService.GetValueAsString(SectionEnum.Settings, SettingsEnum.AutoLoad), _settingsService.AutoLoad);
        autoLoad.Activated += (_, _) => { _settingsService.AutoLoad = autoLoad.Checked; };
        _settingsService.AutoLoadChanged += state => { autoLoad.Checked = state; };
        Add(autoLoad);
    }

    private void AutoSave()
    {
        var autoSave = new CustomNativeCheckboxItem(SettingsEnum.AutoSave,
            HotkeysService.GetValueAsString(SectionEnum.Settings, SettingsEnum.AutoSave), _settingsService.AutoSave);
        autoSave.Activated += (_, _) => { _settingsService.AutoSave = autoSave.Checked; };
        _settingsService.AutoSaveChanged += state => { autoSave.Checked = state; };
        Add(autoSave);
    }

    private void LoadSettings()
    {
        var loadSettings = new CustomNativeItem(SettingsEnum.LoadSettings,
            HotkeysService.GetValueAsString(SectionEnum.Settings, SettingsEnum.LoadSettings));
        loadSettings.Activated += (_, _) => { _settingsService.LoadSettings(); };
        Add(loadSettings);
    }

    private void SaveSettings()
    {
        var saveSettings = new CustomNativeItem(SettingsEnum.SaveSettings,
            HotkeysService.GetValueAsString(SectionEnum.Settings, SettingsEnum.SaveSettings));
        saveSettings.Activated += (_, _) => { _settingsService.SaveSettings(); };
        Add(saveSettings);
    }
}