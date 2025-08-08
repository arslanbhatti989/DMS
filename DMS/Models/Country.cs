using System.ComponentModel.DataAnnotations;

namespace DMS.Models
{
    public class Country : BaseClass
    {
        [Key]
        public int Country_Id { get; set; } = 0;
        public string? Country_Name { get; set; } = string.Empty;
        public string? Country_Code { get; set; } = string.Empty;
        public string? Phone_Code { get; set; } = string.Empty;
        public string? Currency_Code { get; set; } = string.Empty;
        public bool Status_Active { get; set; } = false;
       

    }
}
