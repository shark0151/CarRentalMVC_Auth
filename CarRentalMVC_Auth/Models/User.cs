using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRentalMVC_Auth.Models
{
    public class User
    {
        [Column("iduser")] public int Id { get; set; }
        [Column("username")] public string username { get; set; }
        [Column("password")] public string password { get; set; }
        [Column("token")] public string? token { get; set; }
        [Column("name")] public string name { get; set; }
        [Column("email")] public string email { get; set; }
        [Column("birthdate")][DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")] public DateTime? birthdate { get; set; }
        [Column("license_id")] public string? license_id { get; set; }
        [Column("license_exp")][DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")] public DateTime? license_exp { get; set; }
        [Column("id_number")] public string? id_number { get; set; }

    }
}
