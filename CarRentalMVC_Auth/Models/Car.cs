namespace CarRentalMVC_Auth.Models
{
    using Newtonsoft.Json;
    public class Car
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

    }
}
