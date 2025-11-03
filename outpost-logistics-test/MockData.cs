using Microsoft.EntityFrameworkCore;
using Moq;
using outpost_logistics.Data;
using outpost_logistics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace outpost_logistics_test
{
    public class MockData
    {
        public Mock<ApplicationDbContext> Context;
        public List<Course> courses;
        public List<Vehicle> vehicles;

        public MockData()
        {
            vehicles = new List<Vehicle>()
            { 
                new Vehicle(){ Id = 1, LicensePlate = "BIA 111111", MaxSpeed = 90, MaxContinuousWorkTime = new TimeSpan(8, 0, 0), MinRequiredBreakTime = new TimeSpan(2, 0, 0)},
                new Vehicle(){ Id = 2, LicensePlate = "BIA 222222", MaxSpeed = 100, MaxContinuousWorkTime = new TimeSpan(9, 0, 0), MinRequiredBreakTime = new TimeSpan(1, 0, 0)},
                new Vehicle(){ Id = 3, LicensePlate = "BIA 333333", MaxSpeed = 50, MaxContinuousWorkTime = new TimeSpan(10, 0, 0), MinRequiredBreakTime = new TimeSpan(5, 0, 0)},
                new Vehicle(){ Id = 4, LicensePlate = "BIA 444444", MaxSpeed = 80, MaxContinuousWorkTime = new TimeSpan(7, 0, 0), MinRequiredBreakTime = new TimeSpan(2, 0, 0)},
                new Vehicle(){ Id = 5, LicensePlate = "BIA 555555", MaxSpeed = 60, MaxContinuousWorkTime = new TimeSpan(12, 0, 0), MinRequiredBreakTime = new TimeSpan(3, 0, 0)}
            };
            courses = new List<Course>()
            {
                new Course(){ Id = 1, Description = "Dostawa cebuli do Biedronki", CreatedDate = DateTime.Now, Distance = 720, StartDate = new DateTime(2025, 6, 1, 0, 0, 0), EndDate = new DateTime(2025, 6, 1, 8, 0, 0), VehicleId = 1},
                new Course(){ Id = 2, Description = "Transport storczyków", CreatedDate = DateTime.Now, Distance = 1000, StartDate = new DateTime(2025, 6, 4, 9, 0, 0), EndDate = new DateTime(2025, 6, 4, 20, 0, 0), VehicleId = 2},
                new Course(){ Id = 3, Description = "Transport zastępczy z AliExpress", CreatedDate = DateTime.Now, Distance = 1500, StartDate = new DateTime(2025, 6, 3, 0, 0, 0), EndDate = new DateTime(2025, 6, 4, 7, 0, 0), VehicleId = 5}
            };

            var mockVehicles = new Mock<DbSet<Vehicle>>();
            mockVehicles.As<IQueryable<Vehicle>>().Setup(m => m.Provider).Returns(vehicles.AsQueryable().Provider);
            mockVehicles.As<IQueryable<Vehicle>>().Setup(m => m.Expression).Returns(vehicles.AsQueryable().Expression);
            mockVehicles.As<IQueryable<Vehicle>>().Setup(m => m.ElementType).Returns(vehicles.AsQueryable().ElementType);
            mockVehicles.As<IQueryable<Vehicle>>().Setup(m => m.GetEnumerator()).Returns(vehicles.AsQueryable().GetEnumerator());

            var mockCourses = new Mock<DbSet<Course>>();
            mockCourses.As<IQueryable<Course>>().Setup(m => m.Provider).Returns(courses.AsQueryable().Provider);
            mockCourses.As<IQueryable<Course>>().Setup(m => m.Expression).Returns(courses.AsQueryable().Expression);
            mockCourses.As<IQueryable<Course>>().Setup(m => m.ElementType).Returns(courses.AsQueryable().ElementType);
            mockCourses.As<IQueryable<Course>>().Setup(m => m.GetEnumerator()).Returns(courses.AsQueryable().GetEnumerator());

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.SetupGet(c => c.Courses).Returns(mockCourses.Object);
            mockContext.SetupGet(c => c.Vehicles).Returns(mockVehicles.Object);
            Context = mockContext;
        }
    }
}
