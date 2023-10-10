using System.Text.Json.Serialization;

namespace Application.Common.Models;

public class Error
{
    [JsonPropertyName("errorCode")]
    public string? Code { get; set; }

    [JsonPropertyName("errorHeader")]
    public string? Header { get; set; }

    [JsonPropertyName("errorMessage")]
    public string? Message { get; set; }
}
