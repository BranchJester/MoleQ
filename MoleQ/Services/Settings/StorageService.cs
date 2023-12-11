using System.IO;
using MoleQ.Interfaces.Settings;
using Newtonsoft.Json;

namespace MoleQ.Services.Settings;

public class StorageService : IStorageService
{
    private readonly string _filePath;
    private readonly JsonSerializerSettings _settings;

    public StorageService(string filePath)
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