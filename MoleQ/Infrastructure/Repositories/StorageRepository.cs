using System.IO;
using MoleQ.Core.Domain.Interfaces;
using Newtonsoft.Json;

namespace MoleQ.Infrastructure.Repositories;

public class StorageRepository : IStorageRepository
{
    private readonly string _filePath;
    private readonly JsonSerializerSettings _settings;

    public StorageRepository(string filePath)
    {
        _filePath = filePath;
        _settings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        // Ensure directory exists
        var directory = Path.GetDirectoryName(_filePath);
        if (!Directory.Exists(directory))
            if (directory != null)
                Directory.CreateDirectory(directory);
    }

    public void SaveSettings<T>(T settings)
    {
        var json = JsonConvert.SerializeObject(settings, _settings);
        File.WriteAllText(_filePath, json);
    }

    public T LoadSettings<T>() where T : new()
    {
        if (!File.Exists(_filePath)) return new T();

        var json = File.ReadAllText(_filePath);
        return JsonConvert.DeserializeObject<T>(json);
    }
}