
using RenadWebApp.Models.DataModel.FinaicalModels;
using System.ComponentModel.DataAnnotations;

namespace RenadWebApp.Models.DataModel
{
    public class ContractModel
    {
        public int Id { get; set; }
        public string? ProjectName { get; set; }
        public string? ContractNumber { get; set; }
        [DataType(DataType.Date)]

        public DateTime? CreatedDate { get; set; }
        public int? Duration { get; set; }
        [DataType(DataType.Date)]

        public DateTime? DeliveryDate { get; set; }
        public string? ProjectBy { get; set; }
       
        public int? RatioForEng { get; set; }
        public int? TotalRatioForEng { get; set; }

        public int? TotalOfContract { get; set; }
        public int? TotalTax { get; set; } = 0;
        public int? TotalAmount { get; set; } = 0;

        
        public int? RestOfContractShow { get; set; } = 0;
        public int? RestOfEngShow { get; set; } = 0;
        public int? RestOfTAxShow { get; set; } = 0;
        public string? Approved { get; set; }
        public int? AmountLate { get; set; } = 0;



        public virtual ICollection<ContractPayment> ContractPayment { get; set; }
        public virtual ICollection<ContractFinaical> ContractFinaical { get; set; }

        public virtual ICollection<EngContract> EngContract { get; set; }
        public virtual ICollection<ClientContract> ClientContract { get; set; }


    }
}
