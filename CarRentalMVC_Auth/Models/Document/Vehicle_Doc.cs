namespace CarRentalMVC_Auth.Models.Document
{
    using Newtonsoft.Json;
    public class Vehicle_Doc
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

    }
}
