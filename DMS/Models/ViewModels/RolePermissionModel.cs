namespace DMS.Models.ViewModels
{
    public class RolePermissionModel
    {
        public int Id { get; set; }
        public string ModuleName { get; set; } = string.Empty;
        public bool All { get; set; } = false;
        public bool Add { get; set; } = false;
        public bool Edit { get; set; } = false;
        public bool Delete { get; set; } = false;
        public bool Readonly { get; set; } = false;
        public bool IsDelete { get; set; } = false;
        public DateTime AssignedDate { get; set; } = DateTime.Now;
        public string RoleId { get; set; } = string.Empty;
        public List<string> SelectedMenuNames { get; set; } = new List<string>();
    }
}
