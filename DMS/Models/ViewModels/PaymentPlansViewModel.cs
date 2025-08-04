using System.ComponentModel.DataAnnotations.Schema;

namespace DMS.Models.ViewModels
{
    public class PaymentPlansViewModel
    {
        public int Payment_Plan_Id { get; set; } 
        public int? Project_Id { get; set; }
        public Project? Project { get; set; }
        public string? Plan_Name { get; set; }
        public bool Plan_Status { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Update_At { get; set; }
        public string? Created_By { get; set; }
        public string? Updated_By { get; set; }
        public int TotalInstallments { get; set; }
        public List<InstallmentViewModel>? InstallmentsList { get; set; }
    }
    public class InstallmentViewModel
    {
        public int Installment_Id { get; set; } 
        public int? Payment_Plan_Id { get; set; }
        public Payment_Plans? Payment_Plans { get; set; }
        public string? Installment_Name { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Amount { get; set; }
        public int Sequence_Number { get; set; }
    }
    public class InstallmentListViewModel
    {
        public Payment_Plans? Payment_Plans { get; set; }
        public List<Installments>? Installments { get; set; }
    }
}
