using MoleQ.Core.Application.Interfaces;
using MoleQ.Core.Domain.Enums;
using MoleQ.UI.Items;
using MoleQ.UI.Menus.Abstract;

namespace MoleQ.UI.Menus;

public class SettingsMenu : BaseMenu
{
    private readonly ISettingsService _settingsService;

    public SettingsMenu(string menuName, ISettingsService settingsService) : base(menuName)
    {
        _settingsService = settingsService;
        SaveSettings();
        LoadSettings();
        AutoSave();
        // AutoLoad(); -- TODO: Fix AutoLoad
    }

    private void AutoLoad()
    {
        var autoLoad = new CustomNativeCheckboxItem(SettingsEnum.AutoLoad,
            HotkeysService.GetValueAsString(SectionEnum.Settings, SettingsEnum.AutoLoad), _settingsService.AutoLoad,
            state => { _settingsService.AutoLoad = state; });
        _settingsService.AutoLoadChanged += state => { autoLoad.Checked = state; };
        Add(autoLoad);
    }

    private void AutoSave()
    {
        var autoSave = new CustomNativeCheckboxItem(SettingsEnum.AutoSave,
            HotkeysService.GetValueAsString(SectionEnum.Settings, SettingsEnum.AutoSave), _settingsService.AutoSave,
            state => { _settingsService.AutoSave = state; });
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