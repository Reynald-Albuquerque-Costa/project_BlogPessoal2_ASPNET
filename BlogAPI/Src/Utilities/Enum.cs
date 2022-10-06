using System.Text.Json.Serialization;

namespace BlogAPI.Src.Utilities
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TypeUser
    {
        NORMAL,
        ADMINISTRADOR
    }
}
