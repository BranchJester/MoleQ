using System;
using MoleQ.Interfaces;

namespace MoleQ.Services.Settings;

public class SettingsService : ISettingsService
{
    private bool _autoLoad;
    private bool _autoSave;

    public bool AutoSave
    {
        get => _autoSave;
        set
        {
            if (_autoSave == value) return;
            _autoSave = value;
            AutoSaveChanged?.Invoke(value);
        }
    }

    public bool AutoLoad
    {
        get => _autoLoad;
        set
        {
            if (_autoLoad == value) return;
            _autoLoad = value;
            AutoLoadChanged?.Invoke(value);
        }
    }

    public event Action<bool> AutoSaveChanged;
    public event Action<bool> AutoLoadChanged;

    public event Action SaveSettingsActivated;
    public event Action LoadSettingsActivated;

    public void SaveSettings()
    {
        SaveSettingsActivated?.Invoke();
    }

    public void LoadSettings()
    {
        LoadSettingsActivated?.Invoke();
    }
}