using System;
using System.Collections.Generic;
using MoleQ.Application.Mappers;
using MoleQ.Application.ServiceInjector;
using MoleQ.Constants;
using MoleQ.Core.Application.Interfaces;
using MoleQ.Core.Domain.Interfaces;
using MoleQ.Core.Domain.Settings;
using MoleQ.Infrastructure.Repositories;

namespace MoleQ.Application.Scripts;

public class SettingsScript : BaseScript
{
    private readonly ISettingsService _settingsService = Injector.SettingsService;
    private readonly IStorageRepository _storageRepository = new StorageRepository($"{Path.Settings}/Settings.json");

    protected override void SaveSettings()
    {
        var settings = ServiceSettingsMapper.ExtractSettings<SettingsSettings>(new Dictionary<Type, object>
        {
            { typeof(ISettingsService), _settingsService }
        });
        _storageRepository.SaveSettings(settings);
    }

    protected override void LoadSettings()
    {
        var settings = _storageRepository.LoadSettings<SettingsSettings>();
        var services = new Dictionary<Type, object>
        {
            { typeof(ISettingsService), _settingsService }
        };
        ServiceSettingsMapper.ApplySettings(settings, services);
    }
}