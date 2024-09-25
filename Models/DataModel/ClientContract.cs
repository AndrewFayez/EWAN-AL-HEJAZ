namespace RenadWebApp.Models.DataModel
{
    public class ClientContract
    {
        public int ContractId { get; set; }
        public virtual ContractModel Contract { get; set; }

        public int ClientId { get; set; }
        public virtual ClientModels Client { get; set; }
    }
}
