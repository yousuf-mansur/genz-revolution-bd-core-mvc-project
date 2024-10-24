using System.ComponentModel.DataAnnotations.Schema;

namespace GenZRevolutionBD.Models
{
    public class SuperHero
    {
        public int SuperHeroId { get; set; }
        public string SuperHeroName { get; set; }
        public DateTime DateOfDeath { get; set; }
        public int Age { get; set; }
        public string? Picture {  get; set; }

        public virtual ICollection<SuperPower> SuperPower { get; set; }=new List<SuperPower>();

    }
}
