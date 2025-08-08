using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMS.Models
{
    public class Units : BaseClass
    {
        [Key]
        public int Unit_Id { get; set; }
        public Project? Project { get; set; }
        [ForeignKey("Project")]
        public int? Project_Id { get; set; }
        public int Floor_Number { get; set; } // will be a FK in future from Floor Table
        public string? Unit_Number { get; set; }
        public decimal Interal_Unit_Size_Sqft {  get; set; }
        public decimal External_Unit_Size_Sqft { get; set; }
        public decimal Total_Size_Sqft { get; set; }
        [ForeignKey("UnitType")]
        public int? Unit_Type_Id { get; set; }
        public UnitType? UnitType { get; set; }
        public string? Unit_View { get; set; } // Sea View, Park View
        public string? Status { get; set; } // Available, Sold, Reserved
        public decimal Price {  get; set; }
       
    }
}
