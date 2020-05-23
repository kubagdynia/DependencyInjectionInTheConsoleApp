using System;
using System.Text.Json.Serialization;

namespace LoadingMultipleConfig.Configuration.Models
{
    [Serializable]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ImportType
    {
        SaveInDb,
        SendToBackOfficeSystem,
        InformBookstore
    }
}