using System.ComponentModel.DataAnnotations;

namespace CarRentalMVC_Auth.Models.GraphModel
{
    public class User_Graph
    {
        public string? id { get; set; }

        public string label = "User";
        public string name { get; set; }
        public string? birthdate { get; set; }
        public string? license_id { get; set; }
        public string? license_exp { get; set; }
        public string? id_number { get; set; }
    }
}
