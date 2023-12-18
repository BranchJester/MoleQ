using MoleQ.Constants;
using MoleQ.Repositories;
using MoleQ.Services.Settings;
using MoleQ.Settings;

namespace MoleQ.Scripts;

public class SettingsScript : BaseScript
{
    private readonly IStorageRepository _storageRepository = new StorageRepository($"{Path.Settings}/Settings.json");

    protected override void SaveSettings()
    {
        var settingsSettings = new SettingsSettings
        {
            AutoSave = SettingsService.AutoSave,
            AutoLoad = SettingsService.AutoLoad
        };
        _storageRepository.SaveSettings(settingsSettings);
    }

    protected override void LoadSettings()
    {
        var settingsSettings = _storageRepository.LoadSettings<SettingsSettings>();
        SettingsService.AutoSave = settingsSettings.AutoSave;
        SettingsService.AutoLoad = settingsSettings.AutoLoad;
    }
}