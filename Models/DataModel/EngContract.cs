namespace RenadWebApp.Models.DataModel
{
    public class EngContract
    {
        public int ContractId { get; set; }
        public virtual ContractModel Contract { get; set; }

        public int EngId { get; set; }
        public virtual EngModel Eng { get; set; }
    }
}
