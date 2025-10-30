using System.ComponentModel.DataAnnotations;

namespace outpost_logistics.Models
{
    public class Vehicle
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Numer tablicy rejestracyjnej")]
        public string LicensePlate {  get; set; } = string.Empty;

        [Required]
        [Display(Name = "Prędkość maksymalna")]
        public int MaxSpeed { get; set; }

        [Required]
        [Display(Name = "Maksymalna długość ciągłej pracy pojazdu")]
        public TimeSpan MaxContinuousWorkTime { get; set; }

        [Required]
        [Display(Name = "Minimalna długość wymaganej przerwy")]
        public TimeSpan MinRequiredBreakTime { get; set; }


        public virtual ICollection<Course>? Courses { get; set; }
    }
}
