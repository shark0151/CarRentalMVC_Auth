using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRentalMVC_Auth.Models
{
    public class Rental
    {
        [Column("idrental")] public int Id { get; set; }
        [Column("iduser")] public int iduser { get; set; }
        [Column("idvehicle")] public int idvehicle { get; set; }
        [Column("start_time")][DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")] public DateTime? start_time { get; set; }
        [Column("end_time")][DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")] public DateTime? end_time { get; set; }
        [Column("pickup_loc")] public int pickup_loc { get; set; }
        [Column("dropoff_loc")] public int dropoff_loc { get; set; }
        [Column("active")] public bool active { get; set; }
        [Column("idinsurance")] public int idinsurance { get; set; }
    }
}
