using Newtonsoft.Json;

namespace CarRentalMVC_Auth.Models.Document
{
    public class Insurance_Doc
    {
        [JsonProperty(PropertyName = "id")] public string Id { get; set; }
        [JsonProperty(PropertyName = "type")] public string type { get; set; }
        [JsonProperty(PropertyName = "rate")] public int rate { get; set; }
        [JsonProperty(PropertyName = "description")] public string? description { get; set; }
    }
}
