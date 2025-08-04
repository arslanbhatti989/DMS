using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMS.Models
{
    public class UnitType
    {
        [Key]
        public int Unity_Type_Id { get; set; }
        public string? Unity_Type_Name { get; set; }
        public string? Unit_Type_Status {  get; set; }

        public Project? Project { get; set; }
        [ForeignKey("Project")]
        public int Project_Id { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Update_At { get; set; }
        public string? Created_By { get; set; }
        public string? Updated_By { get; set; }
    }
}
