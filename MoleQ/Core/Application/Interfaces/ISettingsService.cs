using System;

namespace MoleQ.Core.Application.Interfaces;

public interface ISettingsService
{
    public bool AutoLoad { get; set; }
    public bool AutoSave { get; set; }
    event Action<bool> AutoLoadChanged;
    event Action<bool> AutoSaveChanged;
    event Action SaveSettingsActivated;
    event Action LoadSettingsActivated;
    void SaveSettings();
    void LoadSettings();
}