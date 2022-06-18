using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace CarRentalMVC_Auth.Models.Document
{
    public class User_Doc
    {
        [JsonProperty(PropertyName = "id")] public string Id { get; set; }
        [JsonProperty(PropertyName = "id_number")] public string? id_number { get; set; }
        [JsonProperty(PropertyName = "license_id")] public string? license_id { get; set; }
        [JsonProperty(PropertyName = "license_exp")][DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")] public DateTime? license_exp { get; set; }
        
    }
}
