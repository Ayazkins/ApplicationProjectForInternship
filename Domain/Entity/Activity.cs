using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Domain.Entity;

public enum Activity
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    Report,
    [JsonConverter(typeof(JsonStringEnumConverter))]
    Masterclass,
    [JsonConverter(typeof(JsonStringEnumConverter))]
    Discussion
}