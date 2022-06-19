namespace CarRentalMVC_Auth.Models.GraphModel
{
    public class Insurance_Graph
    {
        public string? id { get; set; }

        public string label = "Insurance";
        public string type { get; set; }
        public int rate { get; set; }
        public string? description { get; set; }
    }
}
