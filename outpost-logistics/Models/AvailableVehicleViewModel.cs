using System.ComponentModel.DataAnnotations;

namespace outpost_logistics.Models
{
    public class AvailableVehicleViewModel
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; } = string.Empty;
        public DateTime EstimatedCourseEnd{ get; set; }
    }
}
