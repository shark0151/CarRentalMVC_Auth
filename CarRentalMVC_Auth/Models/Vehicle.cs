using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRentalMVC_Auth.Models
{
    public class Vehicle
    {
        [Column("idvehicle")] public int Id { get; set; }
        [Column("license_nr")] public string license_nr { get; set; }
        [Column("brand")] public string brand { get; set; }
        [Column("model")] public string model { get; set; }
        [Column("year")] public int? year { get; set; }
        [Column("seats")] public int? seats { get; set; }
        [Column("type")] public string? type { get; set; }
        [Column("transmission")] public string? transmission { get; set; }
        [Column("fuel_type")] public string fuel_type { get; set; }
        [Column("mot_exp")][DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")] public DateTime mot_exp { get; set; }
        [Column("current_loc")] public int current_loc { get; set; }
        [Column("rate")] public int rate { get; set; }
        [Column("desc")] public string? desc { get; set; }
    }
}
