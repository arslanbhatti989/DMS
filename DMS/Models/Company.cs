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
    public class CompanyDocument : BaseClass
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string DocumentId { get; set; } = string.Empty; // (Primary Key) (Auto Increment) (Identity)
        [ForeignKey("Company")]
        public int CompanyId { get; set; } = 0;
        [ForeignKey("Project")]
        public int ProjectId { get; set; } = 0;      //(Project Seller ka table)
        [ForeignKey("DocumentType")]
        public string DocumentTypeId { get; set; } = string.Empty;       //(nichy mention hai)
        public string Title { get; set; } = string.Empty;
        [StringLength(500)]
        public string Description { get; set; } = string.Empty; //        (varchar MAX)
        public DateTime DocumentIssueDate { get; set; } = DateTime.Now;
        public DateTime DocumentExpiryDate { get; set; } = DateTime.Now;
        public string FilePath1 { get; set; } = string.Empty;
        public string FilePath2 { get; set; } = string.Empty;
        public string FilePath3 { get; set; } = string.Empty;
        public DateTime ReviewedDate { get; set; } = DateTime.Now;
        public string ReviewedBy { get; set; } = string.Empty;
        public string ApprovedBy { get; set; } = string.Empty;
        public DateTime ApprovedDate { get; set; } = DateTime.Now;
        public bool Status { get; set; }

        public Company Company { get; set; } = new Company();
        public Project Project { get; set; } = new Project();
        public DocumentType DocumentType { get; set; } = new DocumentType();
    }
    public class DocumentType : BaseClass
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string DocumentTypeId { get; set; } = string.Empty; // (Primary Key) (Auto Increment) (Identity)
        public string TypeName { get; set; } = string.Empty;
    }
}
