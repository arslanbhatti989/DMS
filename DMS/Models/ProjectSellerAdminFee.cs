using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMS.Models
{
    public class ProjectSellerAdminFee
    {
        [Key]
        public int Project_Seller_Admin_Fee_Id { get; set; }
        public Project? Project { get; set; }
        [ForeignKey("Project")]
        public int? Project_Id { get; set; }
        public string? OQood_Fee_Description { get; set; }
        public decimal OQoob_Fee_Value { get; set; }
        public string? Admin_Fee_Description { get; set; }
        public decimal Admin_Fee_Value { get; set; }
        public decimal Other_Charges { get; set; }
        public string? Rera_Fee { get; set; }
        public string? Rera_Fee_Description { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Update_At { get; set; }
        public string? Created_By { get; set; }
        public string? Updated_By { get; set; }
    }
}
