namespace MoleQ.Interfaces.Settings;

public interface IStorageService
{
    void SaveSettings<T>(T settings);
    T LoadSettings<T>() where T : new();
}