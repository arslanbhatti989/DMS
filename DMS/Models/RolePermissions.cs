using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DMS.Models
{
    public class ParentClass
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(450)]
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
    public class RolePermissions : ParentClass
    {

        public string ModelloName { get; set; }
        public bool All { get; set; }
        public bool Add { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }
        public bool Readonly { get; set; }
        public DateTime AssignedDate { get; set; }
        public string RoleId { get; set; }  // IdentityRole Id (string)
        public IdentityRole Role { get; set; }  // Reference to IdentityRole
    }
}
