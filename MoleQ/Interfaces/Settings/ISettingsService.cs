using System;

namespace MoleQ.Interfaces.Settings;

public interface ISettingsService
{
    event Action SaveSettingsActivated;
    void SaveSettings();
}