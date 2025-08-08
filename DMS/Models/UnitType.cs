using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMS.Models
{
    public class UnitType : BaseClass
    {
        [Key]
        public int Unity_Type_Id { get; set; }
        public string? Unity_Type_Name { get; set; }
        public string? Unit_Type_Status {  get; set; }

        public Project? Project { get; set; }
        [ForeignKey("Project")]
        public int Project_Id { get; set; }
       
    }
}
