using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DMS.Models
{
    public class Project : BaseClass
    {
        [Key]
        public int Project_Id { get; set; }

        [ForeignKey("Country")]
        public int? Country_Id { get; set; }

        [ForeignKey("City")]
        public int? City_Id { get; set; }
        public Project_Seller? Project_Seller { get; set; }

        [ForeignKey("Project_Seller")]
        public int? Project_Seller_Id { get; set; }

        public string? Project_Name { get; set; }

        
        public string? Project_Address { get; set; }

         
        public string? Project_Used { get; set; } // e.g. Mixed, Residential, Commercial

        public int Total_Floors { get; set; }

        public int Total_Units { get; set; }

        public decimal Project_Land_Area { get; set; }

        public decimal Constructed_Area { get; set; }

        
        public string? Plot_Number { get; set; }

        public decimal Saleable_Area { get; set; }
         
        public string? Construction_Status { get; set; } // Planned, Under Construction, Completed

        public DateTime? Completion_Date { get; set; }


        // Navigation properties (optional)
        public Country? Country { get; set; }
        public City? City { get; set; } 

    }
     
}
