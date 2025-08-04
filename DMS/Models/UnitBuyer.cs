using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMS.Models
{
    public class UnitBuyer
    {
        [Key]
        public int UnitBuyer_Id { get; set; }
        [ForeignKey("Units")]
        public int Unit_Id { get; set; }
        public Units? Units { get; set; }
        public string? BuyerType { get; set; }
        [ForeignKey("Person")]
        public int? Person_Id { get; set; }
        public Person? Person { get; set; }
        public Company? Company { get; set; }
        [ForeignKey("Company")]
        public int? Company_Id { get; set; }
        public bool IsMainBuyer {  get; set; }
    }
}
