using System.ComponentModel.DataAnnotations.Schema;

namespace DMS.Models
{
    public class Person : BaseClass
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public string? UserId { get; set; }
        public Users? User { get; set; }
        public string? Person_Code { get; set; }
        public string? Person_Title { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [ForeignKey("NationalityCountry")]
        public int? NationalityCountry_Id { get; set; }
        public Country? NationalityCountry { get; set; }
        public string? Passport_Number { get;set; }
        public DateTime Passport_Expiry_Date {  get; set; }
        public string? Passport_Type { get; set; }
        public string? Emirates_Id_Number { get; set; }
        public DateTime? Emireate_Id_Expiry_Date { get; set; }   
        public string? Alternate_Phone { get; set; }
        public string? Marital { get; set; }
        public string? ContactPerson { get; set; }
        public string? Occupation { get; set; }
        public string? Employer { get; set; }
        public string? ProofOfFundsPath { get; set; }
        public string? Ownership { get; set;}
        [ForeignKey("PassportNationality")]
        public int? PassportNationality_Id { get; set; }
        public Country? PassportNationality { get; set; }
        [ForeignKey("PassportCountry")]
        public int? PassportCountry_Id { get; set; }
        public Country? PassportCountry { get; set; }
        public string? Address_Line_1 {  get; set; }
        public string? Address_Line_2 { get; set; }
        [ForeignKey("AddressCountry")]
        public int? Address_Country_Id { get; set; }
        public Country? AddressCountry { get; set; }
        [ForeignKey("City")]
        public int? City_Id { get; set; }
        public City? City {  get; set; }
        [ForeignKey("Unit")]
        public int? Unit_Id { get; set; }
        public Units? Unit { get; set; }
        public string? Zip_Code { get; set; }
        
    }
}
