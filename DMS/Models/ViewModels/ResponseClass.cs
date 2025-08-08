namespace DMS.Models.ViewModels
{
    public class ResponseClass<T>
    {
        public bool CanAdd { get; set; } = false;
        public bool CanEdit { get; set; } = false;
        public bool CanDelete { get; set; } = false;
        public bool Other { get; set; } = false;
        public bool HasAccess { get; set; } = false;
        public string? Message { get; set; } = string.Empty;
        
        public List<T?> Items { get; set; } = new();
        public T? Item { get; set; }
    }
}
