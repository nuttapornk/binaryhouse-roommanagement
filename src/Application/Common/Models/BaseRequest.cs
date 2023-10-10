using Newtonsoft.Json;

namespace Application.Common.Models;

public class BaseRequest
{
    [JsonProperty("empid")]
    public string? Username { get; set; }
}
