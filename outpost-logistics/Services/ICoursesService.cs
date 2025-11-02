using outpost_logistics.Models;

namespace outpost_logistics.Services
{
    public interface ICoursesService
    {
        List<Course> Courses();
        List<Course> CoursesWithVehicles();
        Course FindById(int? id);
        Course FindByIdIncludeVehicle(int? id);
        void AddCourse(Course course);
        void UpdateCourse(Course course);
        void DeleteById(int? id);
        bool ExistsById(int? id);
        List<AvailableVehicleViewModel> FindAvailableVehicles(DateTime courseStart, int distance, int? currentCourseId);
        DateTime CalculateCourseEnd(Vehicle vehicle, DateTime courseStart, int distance);
        DateTime CalculateCourseEnd(int vehicleId, DateTime courseStart, int distance);
    }
}
