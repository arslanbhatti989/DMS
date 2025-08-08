using Microsoft.AspNetCore.Identity;

namespace DMS.Models
{
    public class Users : IdentityUser
    {
        public string ProfilePicture { get; set; } = string.Empty;
        public string ParentId { get; set; } = string.Empty;
        public string PlainPassword { get; set; } = string.Empty;
        public string? Role { get; set; }
        public DateTime CreatedDate { get;set; }
        public string? Name { get; set; }
        public string? Commission { get; set; }
        public DateTime? DOB { get; set; }
        public string? PassportPath { get; set; }
        public string? FundsPath { get;set;}
        public string? EmiratesIdPath {  get; set; }
    }
}
