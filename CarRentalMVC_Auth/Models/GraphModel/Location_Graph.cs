namespace CarRentalMVC_Auth.Models.GraphModel
{
    public class Location_Graph
    {
        public string? id { get; set; }

        public string label = "Location";
        public string name { get; set; }
        public string address { get; set; }
        public string zipcode { get; set; }
    }
}
