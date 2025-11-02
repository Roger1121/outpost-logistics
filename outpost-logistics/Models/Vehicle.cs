using System.ComponentModel.DataAnnotations;

namespace outpost_logistics.Models
{
    public class Vehicle
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Pole \"{0}\" jest wymagane")]
        [Display(Name = "Numer tablicy rejestracyjnej")]
        public string LicensePlate {  get; set; } = string.Empty;

        [Required(ErrorMessage = "Pole \"{0}\" jest wymagane")]
        [Display(Name = "Prędkość maksymalna (km/h)")]
        public int MaxSpeed { get; set; }

        [Required(ErrorMessage = "Pole \"{0}\" jest wymagane")]
        [DataType(DataType.Time)]
        [Display(Name = "Maksymalna długość ciągłej pracy pojazdu (hh:mm)")]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan MaxContinuousWorkTime { get; set; }

        [Required(ErrorMessage = "Pole \"{0}\" jest wymagane")]
        [DataType(DataType.Time)]
        [Display(Name = "Minimalna długość wymaganej przerwy (hh:mm)")]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan MinRequiredBreakTime { get; set; }

        public virtual ICollection<Course>? Courses { get; set; }
    }
}
