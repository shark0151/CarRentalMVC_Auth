using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRentalMVC_Auth.Models
{
    public class Location
    {
        [Column("idlocation")] public int Id { get; set; }
        [Column("name")] public string name { get; set; }
        [Column("address")] public string address { get; set; }
        [Column("zip_code")] public string zipcode { get; set; }
        [Column("desc")] public string? description { get; set; }
        
    }
}
