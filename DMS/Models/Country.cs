using System.ComponentModel.DataAnnotations;

namespace DMS.Models
{
    public class Country
    {
        [Key]
        public int Country_Id { get; set; }
        public string? Country_Name { get; set; }
        public string? Country_Code { get; set; }
        public string? Phone_Code { get; set; }
        public string? Currency_Code { get; set; }
        public bool Status_Active { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Update_At { get; set; }
        public string? Created_By { get; set; }
        public string? Updated_By { get; set; }

    }
}
