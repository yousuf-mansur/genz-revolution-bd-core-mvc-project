namespace GenZRevolutionBD.Models
{
    public class SuperPower
    {
        public  int SuperPowerId { get; set; }

        public int SuperHeroId { get; set; }
        public SuperHero? SuperHero { get; set; }

        public int PowerId {  get; set; }
        public Power? Power { get; set; }
    }
}
