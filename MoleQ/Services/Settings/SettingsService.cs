using System;
using MoleQ.Interfaces.Settings;

namespace MoleQ.Services.Settings;

public class SettingsService : ISettingsService
{
    public event Action SaveSettingsActivated;

    public void SaveSettings()
    {
        SaveSettingsActivated?.Invoke();
    }
}