using System.ComponentModel;

namespace MoleQ.Core.Domain.Enums;

public enum SettingsEnum
{
    [Description("Saves all settings.")] SaveSettings,

    [Description("Loads all settings.")] LoadSettings,

    [Description("Toggles the auto save feature.")]
    AutoSave,

    [Description("Toggles the auto load feature.")]
    AutoLoad
}