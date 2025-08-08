using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DMS.Models
{
    public class BaseClass
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public string Id { get; set; } = string.Empty;
        public DateTime Created_At { get; set; }= DateTime.Now;
        public DateTime Updated_At { get; set; } = DateTime.Now;
        public string? Created_By { get; set; } = string.Empty;
        public string? Updated_By { get; set; } =string.Empty;
        public bool? IsDeleted { get; set; } = false;

    }
}
