using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMS.Models
{
    public class Company
    {
        [Key]
        public int Company_Id { get; set; }
        public string? Company_Name { get; set; }
        public string? Trade_License_Number { get; set; }
        public DateTime License_Expiry_Date { get; set; }
        [ForeignKey("Country")]
        public int? Country_Id { get; set; }
        public Country? Country { get; set; }
        [ForeignKey("City")]
        public int? City_Id { get; set; }
        public City? City { get; set; }
        public string? Address_Line_1 { get; set; }
        public string? Address_Line_2 { get; set; }
        public string? Zip_Code { get; set; }
        public string? Funds { get; set; }
        public string? ProofOfFundsPath { get; set; }
        public int Ownership { get; set; }
        public string? Contact_Person_Name { get; set; }
        public string? Contact_Person_Designation { get; set; }
        public string? Contact_Person_Passport {  get; set; }
        public string? Contact_Person_Emirates_Id { get; set; }
        public string? Contact_Person_Email { get; set; }
        public string? Contact_Person_Phone { get; set; }
        [ForeignKey("Unit")]
        public int? Unit_Id { get; set; }
        public Units? Unit { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Update_At { get; set; }
        public string? Created_By { get; set; }
        public string? Updated_By { get; set; }
    }
}
