using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Domain.Entity;

public enum ActivityType
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    Report,
    [JsonConverter(typeof(JsonStringEnumConverter))]
    Masterclass,
    [JsonConverter(typeof(JsonStringEnumConverter))]
    Discussion
}