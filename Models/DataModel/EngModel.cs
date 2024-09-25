using System.ComponentModel.DataAnnotations;

namespace RenadWebApp.Models.DataModel
{
    public class EngModel
    {
        public int Id { get; set; }
        public string? EngName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Offices { get; set; }
        public string? Connection { get; set; }
        public string? CompOrFree { get; set; }
        public string? Meeting { get; set; }
        [DataType(DataType.Date)]

        public DateTime? Created { get; set; }
        [DataType(DataType.Date)]

        public DateTime? TimeOfMeeting { get; set; }
        [DataType(DataType.Date)]

        public DateTime? LastCommunication { get; set; }

        public virtual ICollection<EngContract> EngContract { get; set; }


    }
}
