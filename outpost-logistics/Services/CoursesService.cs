using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using outpost_logistics.Data;
using outpost_logistics.Models;

namespace outpost_logistics.Services
{
    public class CoursesService : ICoursesService
    {
        private readonly ApplicationDbContext _context;
        private readonly IVehiclesService _vehiclesService;

        public CoursesService(ApplicationDbContext context, IVehiclesService vehiclesService)
        {
            _context = context;
            _vehiclesService = vehiclesService;
        }

        public Course FindById(int? id)
        {
            return _context.Courses
                .FirstOrDefault(m => m.Id == id);
        }

        public Course FindByIdIncludeVehicle(int? id)
        {
            return _context.Courses
                .Include(c => c.Vehicle)
                .FirstOrDefault(m => m.Id == id);
        }

        public List<Course> Courses()
        {
            return _context.Courses.ToList();
        }

        public List<Course> CoursesWithVehicles()
        {
            return _context.Courses.Include(c => c.Vehicle).ToList();
        }

        public void AddCourse(Course course)
        {
            _context.Add(course);
            _context.SaveChanges();
        }

        public void UpdateCourse(Course course)
        {
            _context.Update(course);
            _context.SaveChanges();
        }

        public void DeleteById(int? id)
        {
            var course = FindById(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
            }

            _context.SaveChanges();
        }

        public bool ExistsById(int? id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }

        public List<AvailableVehicleViewModel> FindAvailableVehicles(DateTime courseStart, int distance, int? currentCourseId)
        {
            List<AvailableVehicleViewModel> availableVehicles = new List<AvailableVehicleViewModel>();
            _context.Vehicles?.ToList().ForEach(vehicle =>
            {
                availableVehicles.Add(new AvailableVehicleViewModel()
                {
                    Id = vehicle.Id,
                    LicensePlate = vehicle.LicensePlate,
                    EstimatedCourseEnd = CalculateCourseEnd(vehicle, courseStart, distance)
                });
            });
            return availableVehicles
                .Where(vehicle => IsVehicleAvailable(vehicle.Id, courseStart, vehicle.EstimatedCourseEnd, currentCourseId)).ToList();
        }

        public DateTime CalculateCourseEnd(int vehicleId, DateTime courseStart, int distance)
        {
            Vehicle vehicle = _vehiclesService.FindById(vehicleId);
            return CalculateCourseEnd(vehicle, courseStart, distance);
        }

        public DateTime CalculateCourseEnd(Vehicle vehicle, DateTime courseStart, int distance)
        {
            TimeSpan timeOfTravel = TimeSpan.FromHours(1.0 * distance / vehicle.MaxSpeed);
            int numberOfBreaks = (int)timeOfTravel.Divide(vehicle.MaxContinuousWorkTime);
            DateTime courseEnd = courseStart
                // Add time of travel
                .Add(timeOfTravel)
                //Add time of breaks
                .Add(vehicle.MinRequiredBreakTime.Multiply(numberOfBreaks));
            // Round up to full minutes
            if (courseEnd.Second > 0 || courseEnd.Microsecond > 0)
            {
                courseEnd = courseEnd.AddMinutes(1);
            }
            return new DateTime(courseEnd.Year, courseEnd.Month, courseEnd.Day,
                         courseEnd.Hour, courseEnd.Minute, 0, courseEnd.Kind);
        }

        private bool IsVehicleAvailable(int id, DateTime courseStart, DateTime estimatedCourseEnd, int? currentCourseId)
        {
            bool isAvailable = _context.Courses
                    .Where(course => course.Id != currentCourseId && course.VehicleId == id && (course.StartDate <= estimatedCourseEnd && course.EndDate >= courseStart)).IsNullOrEmpty();
            return isAvailable;
        }
    }
}
