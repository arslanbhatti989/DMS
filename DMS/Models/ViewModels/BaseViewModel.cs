namespace DMS.Models.ViewModels
{
    public class BaseViewModel
    {
        public DateTime Created_At { get; set; } = DateTime.Now;
        public DateTime Update_At { get; set; } = DateTime.Now;
        public string? Created_By { get; set; } = string.Empty;
        public string? Updated_By { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;
    }
}
