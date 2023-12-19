namespace MoleQ.Core.Domain.Interfaces;

public interface IStorageRepository
{
    void SaveSettings<T>(T settings);
    T LoadSettings<T>() where T : new();
}