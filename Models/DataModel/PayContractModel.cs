using System.ComponentModel.DataAnnotations;

namespace RenadWebApp.Models.DataModel
{
    public class PayContractModel
    {
        public string ContractPay { get; set; }
        public int Id { get; set; }
        public string? PaymentNumber { get; set; }
        public int? Payments { get; set; } = 0;

        [DataType(DataType.Date)]

        public DateTime? DateForPayment { get; set; }

        [DataType(DataType.Date)]

        public DateTime? DueDate { get; set; }
        public int? LateTime1 { get; set; }
        public int? RatioForEng { get; set; }
        public string? DueEngRatio { get; set; }
        public int? PaymentTax { get; set; }
        public string? DueTax { get; set; }
        public int? PayMentValue { get; set; } = 0;
        public int? Difference { get; set; } = 0;
    }
}
