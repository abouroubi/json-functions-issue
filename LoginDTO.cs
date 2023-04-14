using System.Text.Json;
using System.Text.Json.Serialization;

namespace Company.Function;
public partial class LoginDTO
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }
}

public static class SerializeLoginDTO
{
    public static string ToJson(this LoginDTO self) => JsonSerializer.Serialize(self);
}

