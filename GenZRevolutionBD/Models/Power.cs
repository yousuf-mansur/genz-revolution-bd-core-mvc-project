using System.ComponentModel.DataAnnotations.Schema;

namespace GenZRevolutionBD.Models
{
    public class Power
    {
        public int PowerId { get; set; }
        public required string PowerName { get; set; }

        public virtual ICollection<SuperPower> SuperPower { get; set; } = new List<SuperPower>();
    }
}
