using System.ComponentModel.DataAnnotations.Schema;

namespace CarRentalMVC_Auth.Models
{
    public class Insurance
    {
        [Column("idinsurance")] public int Id { get; set; }
        [Column("type")] public string type { get; set; }
        [Column("rate")] public int rate { get; set; }
        [Column("desc")] public string? description { get; set; }
    }
}
