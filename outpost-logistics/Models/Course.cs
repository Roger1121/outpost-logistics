using System.ComponentModel.DataAnnotations;

namespace outpost_logistics.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Data początku kursu")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "Przewidywana data zakończenia kursu")]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Data utworzenia kursu")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [Display(Name = "Odległośc do pokonania")]
        public int Distance { get; set; }

        [Required]
        [Display(Name = "Szczegóły")]
        public string Description { get; set; } = string.Empty;

        [Required]
        public required Vehicle Vehicle { get; set; }

        public int VehicleId { get; set; }
    }
}
