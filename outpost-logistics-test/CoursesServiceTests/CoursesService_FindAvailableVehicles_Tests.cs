using outpost_logistics.Models;
using outpost_logistics.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace outpost_logistics_test.CoursesServiceTests
{
    public class CoursesService_FindAvailableVehicles_Tests
    {
        public readonly MockData _mockData;

        public CoursesService_FindAvailableVehicles_Tests()
        {
            _mockData = new MockData();
        }

        [Theory]
        [MemberData(nameof(CourseData))]
        public void FindAvailableVehicles_WhenFindWithGivenData_ShouldMatchExpected(DateTime courseStart, int distance, int? currentCourseId, List<AvailableVehicleViewModel> expectedVehicles)
        {
            var mockContext = _mockData.Context.Object;
            var vehiclesService = new VehiclesService(mockContext);
            var coursesService = new CoursesService(mockContext, vehiclesService);

            List<AvailableVehicleViewModel> vehiclesFound = coursesService.FindAvailableVehicles(courseStart, distance, currentCourseId);

            Assert.Equal(expectedVehicles, vehiclesFound);
        }

        public static IEnumerable<object[]> CourseData =>
            new List<object[]>
            {
                new object[] { new DateTime(2025, 5, 30, 0, 0, 0), 50000, null, new List<AvailableVehicleViewModel>(){
                        new AvailableVehicleViewModel() { Id = 3, LicensePlate = "BIA 333333", EstimatedCourseEnd = new DateTime(2025, 7, 31, 7, 0, 0)},
                        new AvailableVehicleViewModel() { Id = 4, LicensePlate = "BIA 444444", EstimatedCourseEnd = new DateTime(2025, 7, 2, 11, 0, 0)}
                    }
                },
                new object[] { new DateTime(2025, 6, 2, 0, 0, 0), 50000, null, new List<AvailableVehicleViewModel>(){
                        new AvailableVehicleViewModel() { Id = 1, LicensePlate = "BIA 111111", EstimatedCourseEnd = new DateTime(2025, 6, 30, 21, 34, 0)},
                        new AvailableVehicleViewModel() { Id = 3, LicensePlate = "BIA 333333", EstimatedCourseEnd = new DateTime(2025, 8, 3, 7, 0, 0)},
                        new AvailableVehicleViewModel() { Id = 4, LicensePlate = "BIA 444444", EstimatedCourseEnd = new DateTime(2025, 7, 5, 11, 0, 0)}
                    }
                },
                new object[] { new DateTime(2025, 6, 4, 10, 0, 0), 50000, null, new List<AvailableVehicleViewModel>(){
                        new AvailableVehicleViewModel() { Id = 1, LicensePlate = "BIA 111111", EstimatedCourseEnd = new DateTime(2025, 7, 3, 7, 34, 0)},
                        new AvailableVehicleViewModel() { Id = 3, LicensePlate = "BIA 333333", EstimatedCourseEnd = new DateTime(2025, 8, 5, 17, 0, 0)},
                        new AvailableVehicleViewModel() { Id = 4, LicensePlate = "BIA 444444", EstimatedCourseEnd = new DateTime(2025, 7, 7, 21, 0, 0)},
                        new AvailableVehicleViewModel() { Id = 5, LicensePlate = "BIA 555555", EstimatedCourseEnd = new DateTime(2025, 7, 17, 18, 20, 0)}
                    }
                },
                new object[] { new DateTime(2025, 6, 6, 0, 0, 0), 50000, null, new List<AvailableVehicleViewModel>(){
                        new AvailableVehicleViewModel() { Id = 1, LicensePlate = "BIA 111111", EstimatedCourseEnd = new DateTime(2025, 7, 4, 21, 34, 0)},
                        new AvailableVehicleViewModel() { Id = 2, LicensePlate = "BIA 222222", EstimatedCourseEnd = new DateTime(2025, 6, 29, 3, 0, 0)},
                        new AvailableVehicleViewModel() { Id = 3, LicensePlate = "BIA 333333", EstimatedCourseEnd = new DateTime(2025, 8, 7, 7, 0, 0)},
                        new AvailableVehicleViewModel() { Id = 4, LicensePlate = "BIA 444444", EstimatedCourseEnd = new DateTime(2025, 7, 9, 11, 0, 0)},
                        new AvailableVehicleViewModel() { Id = 5, LicensePlate = "BIA 555555", EstimatedCourseEnd = new DateTime(2025, 7, 19, 8, 20, 0)}
                    }
                },
            };
    }
}
