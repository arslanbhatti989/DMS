using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMS.Models
{
    public class Installments
    {
        [Key]
        public int Installment_Id { get; set; }
        [ForeignKey("Payment_Plans")]
        public int? Payment_Plan_Id { get; set; }
        public Payment_Plans? Payment_Plans { get; set; }
        public string? Installment_Name { get; set; }
        public DateTime DueDate {  get; set; }
        public decimal Amount { get; set; }
        public int Sequence_Number {  get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Update_At { get; set; }
        public string? Created_By { get; set; }
        public string? Updated_By { get; set; }
    }
}
