using System.ComponentModel.DataAnnotations;

namespace outpost_logistics.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Pole \"{0}\" jest wymagane")]
        [Display(Name = "Data początku kursu")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Pole \"{0}\" jest wymagane")]
        [Display(Name = "Przewidywana data zakończenia kursu")]
        public DateTime EndDate { get; set; } = DateTime.Now;

        [Display(Name = "Data utworzenia kursu")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Pole \"{0}\" jest wymagane")]
        [Display(Name = "Dystans (km)")]
        public int Distance { get; set; }

        [Required(ErrorMessage = "Pole \"{0}\" jest wymagane")]
        [Display(Name = "Szczegóły")]
        public string Description { get; set; } = string.Empty;

        public Vehicle? Vehicle { get; set; }

        [Required(ErrorMessage = "Pole \"{0}\" jest wymagane")]
        [Display(Name = "Pojazd")]
        public int VehicleId { get; set; }
    }
}
