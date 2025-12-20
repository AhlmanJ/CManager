using System.Text.Json;

namespace CManager.Infrastructure.serialization;

public class JsonDataFormatter
{
    public static readonly JsonSerializerOptions options = new JsonSerializerOptions()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true,
    };

    public static string serialize<T>(T content) => JsonSerializer.Serialize(content, options);

    public static T? Deserialize<T>(string json) => JsonSerializer.Deserialize<T>(json, options) ?? default;
}
