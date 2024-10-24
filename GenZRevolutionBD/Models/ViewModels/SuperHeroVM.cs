using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenZRevolutionBD.Models.ViewModels
{
    public class SuperHeroVM
    {
        public int ID { get; set; }
        [Display(Name ="Name")]
        public string SuperHeroName { get; set; }
        [Required, Display(Name = "Date Of Death"), Column(TypeName = "date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfDeath { get; set; }
        public int Age { get; set; }
        public string? Picture {  get; set; }
        public IFormFile? PictureFile { get; set; }
        
        public List<int> PowerList { get; set; } = new List<int>();
    }
}
