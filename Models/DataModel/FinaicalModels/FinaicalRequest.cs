using System.ComponentModel.DataAnnotations;

namespace RenadWebApp.Models.DataModel.FinaicalModels
{
    public class FinaicalRequest
    {
        public int Id { get; set; }
        public string? FinaicalNumber { get; set; }
        public string? ContractNumber { get; set; }
        public string? ClientName { get; set; }
        public string? Titel { get; set; }
        public string? Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime? CreatedDate { get; set; }
        public virtual ICollection<ContractFinaical> ContractFinaical { get; set; }

    }
}
