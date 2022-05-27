using System.ComponentModel.DataAnnotations;

namespace CarRentalMVC_Auth.Models.GraphModel
{
    public class GraphUser
    {
        public string id { get; set; }
        public string label { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string? birthdate { get; set; }
        public string? license_id { get; set; }
        public string? license_exp { get; set; }
        public string? id_number { get; set; }
    }
}
