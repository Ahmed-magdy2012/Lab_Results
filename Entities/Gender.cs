using System.Text.Json.Serialization;

namespace Lab_Results.Entities
{

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Gender
    {
        Male,
        Female
    }
}
