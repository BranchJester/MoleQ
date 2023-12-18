using MoleQ.Enums;
using MoleQ.Interfaces.Settings;
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
        AutoLoad();
    }

    private void AutoLoad()
    {
    }

    private void AutoSave()
    {
    }

    private void LoadSettings()
    {
    }

    private void SaveSettings()
    {
        var saveSettings = new CustomNativeItem(SettingsEnum.SaveSettings,
            HotkeysService.GetValueAsString(SectionEnum.Settings, SettingsEnum.SaveSettings));
        saveSettings.Activated += (_, _) => { _settingsService.SaveSettings(); };
        Add(saveSettings);
    }
}