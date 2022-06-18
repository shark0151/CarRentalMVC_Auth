using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace CarRentalMVC_Auth.Models.Document
{
    public class Rental_Doc
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "user")]
        public User_Doc user { get; set; }

        
        public Vehicle_Doc vehicle { get; set; }

        
        public Insurance_Doc insurance { get; set; }

        
        public Location_Doc pick_location { get; set; }

        
        public Location_Doc drop_location { get; set; }

        [JsonProperty(PropertyName = "start_time")][DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime start_time { get; set; }

        [JsonProperty(PropertyName = "end_time")][DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime end_time { get; set; }

        [JsonProperty(PropertyName = "active")]
        public bool active { get; set; }

    }
}
