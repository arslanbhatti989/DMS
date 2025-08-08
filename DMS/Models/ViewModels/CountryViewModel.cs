using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMS.Models.ViewModels
{
    public class CountryViewModel : BaseViewModel
    {
        public int Country_Id { get; set; } = 0;
        [Required]
        public string Country_Name { get; set; } = string.Empty;
        [Required]

        public string Country_Code { get; set; } = string.Empty;
        [Required]

        public string Phone_Code { get; set; } = string.Empty;
        [Required]

        public string Currency_Code { get; set; } = string.Empty;
        [Required]

        public bool Status_Active { get; set; } = false;
       

    }
    public class CityViewModel : BaseViewModel
    {

        public int City_Id { get; set; }
        [Required]
        public int Country_Id { get; set; }
        [Required]
        public string? City_Name { get; set; }
        [Required]
        public bool Status_Active { get; set; }


    }
}
