using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DMS.Models.ViewModels
{
    
    public class ProjectSellerAdminFeeViewModel : BaseClass
    {
        public int Project_Seller_Admin_Fee_Id { get; set; }
        [Required]
        public int Project_Id { get; set; }

        public string? OQood_Fee_Description { get; set; } = string.Empty;
        [Required]

        public decimal OQoob_Fee_Value { get; set; }

        public string? Admin_Fee_Description { get; set; }
        [Required]

        public decimal Admin_Fee_Value { get; set; }
        public decimal Other_Charges { get; set; }
        [ Required]
        public string Rera_Fee { get; set; }
        public string? Rera_Fee_Description { get; set; }

    }
}
