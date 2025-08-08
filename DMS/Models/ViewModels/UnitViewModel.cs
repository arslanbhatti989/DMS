using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMS.Models.ViewModels
{
    public class UnitViewModel
    {
        public int Unit_Id { get; set; }
        public string? UserId {  get; set; }
        public Project? Project { get; set; } 
        public int? Project_Id { get; set; }
        public int Floor_Number { get; set; } // will be a FK in future from Floor Table
        public string? Unit_Number { get; set; }
        public decimal Interal_Unit_Size_Sqft { get; set; }
        public decimal External_Unit_Size_Sqft { get; set; }
        public decimal Total_Size_Sqft { get; set; } 
        public int? Unit_Type_Id { get; set; }
        public UnitType? UnitType { get; set; }
        public string? Unit_View { get; set; } // Sea View, Park View
        public string? Status { get; set; } // Available, Sold, Reserved
        public decimal Price { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Update_At { get; set; }
        public string? Created_By { get; set; }
        public string? Updated_By { get; set; }

        public int Project_Seller_Admin_Fee_Id { get; set; } 
        public string? OQood_Fee_Description { get; set; }
        public decimal OQoob_Fee_Value { get; set; }
        public string? Admin_Fee_Description { get; set; }
        public decimal Admin_Fee_Value { get; set; }
        public decimal Other_Charges { get; set; }
        public string? Rera_Fee { get; set; }
        public string? Rera_Fee_Description { get; set; }
        public PaymentPlansViewModel? paymentPlans { get; set; }
        public List<PaymentPlansViewModel>? PaymentPlansList {  get; set; }
        public List<NewUnitFormViewModel>? UnitBuyers { get; set; }
        public int? UnitBuyers_Id { get; set; }
        public List<Person>? PersonList { get; set; }
        public List<Company>? CompanyList {  get; set; }
        public List<Country>? CountryList { get; set; }
        public List<City>? CityList { get; set; }
    }
    public class UnitBuyerViewModel
    {
        public int UnitBuyer_Id {  get; set; }
        public int Unit_Id { get; set; }
        public string? BuyerType { get; set; }
        public int? Person_Id { get; set; }
        public Person? Person { get; set; }
        public int? Company_Id { get; set; }
        public Company? Company { get; set; }
        public bool IsMainBuyer { get; set; }
    }
    public class NewUnitFormViewModel
    {
        public int? Person_Id { get; set; }
        public Person? Person { get; set; }
        public int? Company_Id { get; set; }
        public Company? Company { get; set; }
        public int UnitBuyer_Id { get; set; }
        public string? BuyerType { get; set; }
        public bool IsMainBuyer { get; set; }
        //personFields
        //public int Person_Id { get; set; } 
        public string? PersonUserId { get; set; }
        public Users? PersonUser { get; set; }
        public string? PersonPerson_Code { get; set; }
        public string? PersonPerson_Title { get; set; }
        public string? PersonFirstName { get; set; }
        public string? PersonEmail { get; set; }
        public string? PersonLastName { get; set; }
        public DateTime? PersonDOB { get; set; }
        public int? PersonNationalityCountry_Id { get; set; }
        public Country? PersonNationalityCountry { get; set; }
        public string? PersonPassport_Number { get; set; }
        public DateTime PersonPassport_Expiry_Date { get; set; }
        public string? PersonPassport_Type { get; set; }
        public string? PersonEmirates_Id_Number { get; set; }
        public DateTime? PersonEmireate_Id_Expiry_Date { get; set; }
        public string? PersonAlternate_Phone { get; set; }
        public string? PersonMarital { get; set; }
        public string? PersonContactPerson { get; set; }
        public string? PersonOccupation { get; set; }
        public string? PersonEmployer { get; set; }
        public string? PersonFundsPath { get; set; }
        public IFormFile? PersonFundsFile { get; set; }
        public string? PersonOwnership { get; set; }  
        public int? PersonPassportNationality_Id { get; set; }
        public Country? PersonPassportNationality { get; set; } 
        public int? PersonPassportCountry_Id { get; set; }
        public Country? PersonPassportCountry { get; set; }
        public string? PersonAddress_Line_1 { get; set; }
        public string? PersonAddress_Line_2 { get; set; } 
        public int? PersonAddress_Country_Id { get; set; }
        public Country? PersonAddressCountry { get; set; } 
        public int? PersonCity_Id { get; set; }
        public City? PersonCity { get; set; } 
        public int? Unit_Id { get; set; }
        public Units? PersonUnit { get; set; }
        public string? PersonZip_Code { get; set; }
        //PersonFields End

        //CompanyFields
        //public int Company_Id { get; set; }
        public string? Company_Name { get; set; }
        public string? CompanyTrade_License_Number { get; set; }
        public DateTime CompanyLicense_Expiry_Date { get; set; } 
        public int? CompanyCountry_Id { get; set; }
        public Country? CompanyCountry { get; set; } 
        public int? CompanyCity_Id { get; set; }
        public City? CompanyCity { get; set; }
        public string? CompanyAddress_Line_1 { get; set; }
        public string? CompanyAddress_Line_2 { get; set; }
        public string? CompanyZip_Code { get; set; }
        public string? CompanyFunds { get; set; }
        public string? CompanyProofOfFundsPath { get; set; }
        public IFormFile? CompanyProofOfFundsFile { get; set; }
        public int CompanyOwnership { get; set; }
        public string? CompanyContact_Person_Name { get; set; }
        public string? CompanyContact_Person_Designation { get; set; }
        public string? CompanyContact_Person_Passport { get; set; }
        public string? CompanyContact_Person_Emirates_Id { get; set; }
        public string? CompanyContact_Person_Email { get; set; }
        public string? CompanyContact_Person_Phone { get; set; }  
        //CompanyFields End
    }
    public class UserViewModel
    {
       

        [Required(ErrorMessage = "The Phone Number is Required")]
        public string PhoneNumber { get; set; }

        [Required]
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string ParentId { get; set; }
        [NotMapped]
        public bool IsEdit { get; set; } = false;
        [NotMapped]
        public bool IsAdmin { get; set; } = false;
        public bool IsProfile { get; set; } = false;
        [NotMapped]
        public IFormFile Picture { get; set; }
        [NotMapped]
        public string File { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$",
        ErrorMessage = "Password must be at least 8 characters long, at least one uppercase letter, one number, and one special character.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string? UserId { get; set; } 
        public string? Name { get; set; }
        public string? Role {  get; set; }
       
        public DateTime? DOB {  get; set; }
        public string? PassportPath { get; set; }
        public string? FundsPath { get; set; }
        public string? EmiratesIdPath { get; set; }

    }
    public class CountryCityListViewModel()
    {
        public List<Country>? Countries { get; set; }
        public List<City>? Cities { get; set; }
    }
}
