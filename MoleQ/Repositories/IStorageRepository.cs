namespace MoleQ.Repositories;

public interface IStorageRepository
{
    void SaveSettings<T>(T settings);
    T LoadSettings<T>() where T : new();
}