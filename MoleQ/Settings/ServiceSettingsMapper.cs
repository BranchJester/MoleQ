using System;
using System.Collections.Generic;

namespace MoleQ.Settings;

public static class ServiceSettingsMapper
{
    public static T ExtractSettings<T>(IDictionary<Type, object> services) where T : IServiceSettings, new()
    {
        var settings = new T();
        settings.ExtractFromServices(services);
        return settings;
    }

    public static void ApplySettings<T>(T settings, IDictionary<Type, object> services) where T : IServiceSettings
    {
        settings.ApplyToServices(services);
    }
}