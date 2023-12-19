using System;
using System.Collections.Generic;
using MoleQ.Core.Application.Interfaces;

namespace MoleQ.Core.Domain.Settings;

public class SettingsSettings : IServiceSettings
{
    public bool AutoSave { get; set; }
    public bool AutoLoad { get; set; }

    public void ApplyToServices(IDictionary<Type, object> services)
    {
        if (services.TryGetValue(typeof(ISettingsService), out var settingsService))
        {
            ((ISettingsService)settingsService).AutoSave = AutoSave;
            ((ISettingsService)settingsService).AutoLoad = AutoLoad;
        }
    }

    public void ExtractFromServices(IDictionary<Type, object> services)
    {
        if (services.TryGetValue(typeof(ISettingsService), out var settingsService))
        {
            AutoSave = ((ISettingsService)settingsService).AutoSave;
            AutoLoad = ((ISettingsService)settingsService).AutoLoad;
        }
    }
}