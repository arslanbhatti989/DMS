using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMS.Models
{
    public class City
    {
        [Key]
        public int City_Id { get; set; }
        public Country? Country { get; set; }
        [ForeignKey("Country")]
        public int? Country_Id { get; set; }
        public string? City_Name { get; set; }
        public bool Status_Active { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Update_At { get; set; }
        public string? Created_By { get; set; }
        public string? Updated_By { get; set; }
    }
}
