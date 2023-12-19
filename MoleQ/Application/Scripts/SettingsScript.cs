﻿using MoleQ.Constants;
using MoleQ.Core.Domain.Interfaces;
using MoleQ.Core.Domain.Settings;
using MoleQ.Infrastructure.Repositories;

namespace MoleQ.Application.Scripts;

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