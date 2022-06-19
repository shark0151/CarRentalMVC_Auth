namespace CarRentalMVC_Auth.Models.GraphModel
{
    public class Rental_Graph
    {
        public string? id { get; set; }

        public string label = "Rental";
        public string userID { get; set; }


        public string vehicleID { get; set; }


        public string insuranceID { get; set; }


        public string pick_locationID { get; set; }


        public string drop_locationID { get; set; }

        public DateTime start_time { get; set; }

        public DateTime end_time { get; set; }

        public bool active { get; set; }
    }
}
