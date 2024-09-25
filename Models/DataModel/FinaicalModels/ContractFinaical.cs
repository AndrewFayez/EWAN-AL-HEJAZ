namespace RenadWebApp.Models.DataModel.FinaicalModels
{
    public class ContractFinaical
    {
        public int ContractId { get; set; }
        public virtual ContractModel Contract { get; set; }

        public int FinaicalId { get; set; }
        public virtual FinaicalRequest Finaical { get; set; }
    }
}
