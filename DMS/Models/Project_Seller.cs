using System.ComponentModel.DataAnnotations;

namespace DMS.Models
{
    public class Project_Seller
    {
        [Key]
        public int Project_Seller_Id { get; set; }
        public string? Seller_Company_Address { get; set; }
        public DateTime? Seller_Company_Licence_Expiry { get; set; }
        public string? Seller_Company_License_Number { get; set; }
        public string? Seller_Company_Name { get; set; }
        public string? Authorized_Signature_Name {  get; set; }
        public string? Authorized_Signature_Designation {  get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Update_At { get; set; }
        public string? Created_By { get; set; }
        public string? Updated_By { get; set; }

    }
}
