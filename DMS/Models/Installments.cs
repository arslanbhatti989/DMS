using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMS.Models
{
    public class Installments :BaseClass
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
       
    }
}
