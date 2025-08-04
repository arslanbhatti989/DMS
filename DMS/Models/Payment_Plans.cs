using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMS.Models
{
    public class Payment_Plans
    {
        [Key]
        public int Payment_Plan_Id { get; set; }
        [ForeignKey("Project")]
        public int? Project_Id { get; set; }
        public Project? Project { get; set; }
        public string? Plan_Name { get; set; }
        public bool Plan_Status { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Update_At { get; set; }
        public string? Created_By { get; set; }
        public string? Updated_By { get; set; }
    }
}
