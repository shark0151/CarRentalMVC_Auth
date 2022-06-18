using Newtonsoft.Json;

namespace CarRentalMVC_Auth.Models.Document
{
    public class Location_Doc
    {
        [JsonProperty(PropertyName = "id")] public string Id { get; set; }
        [JsonProperty(PropertyName = "name")] public string name { get; set; }
        [JsonProperty(PropertyName = "address")] public string address { get; set; }
        [JsonProperty(PropertyName = "zipcode")] public string zipcode { get; set; }
    }
}
