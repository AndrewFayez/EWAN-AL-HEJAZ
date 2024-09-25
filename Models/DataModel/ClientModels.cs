
using RenadWebApp.Models.DataModel.FinaicalModels;
using System.ComponentModel.DataAnnotations;

namespace RenadWebApp.Models.DataModel
{
    public class ClientModels
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? NumberOfProject { get; set; }
        public int? TotalAmount { get; set; } = 0;
        public int? RestFromAmount { get; set; } = 0;
        public string? Approved { get; set;}
        public int? AmountLate { get; set; } = 0;


        [DataType(DataType.Date)]

        public DateTime? CreatedDate { get; set; }
        public virtual ICollection<ClientContract> ClientContract { get; set; }



    }
}
