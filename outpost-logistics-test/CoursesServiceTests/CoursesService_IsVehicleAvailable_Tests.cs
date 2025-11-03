using outpost_logistics.Services;

namespace outpost_logistics_test.CoursesServiceTests
{
    public class CoursesService_IsVehicleAvailable_Tests
    {
        public readonly MockData _mockData;

        public CoursesService_IsVehicleAvailable_Tests()
        {
            _mockData = new MockData();
        }

        [Theory]
        [MemberData(nameof(OverlappingCourseData))]
        public void IsVehicleAvailable_WhenOverlappingCourseExists_ShouldReturnFalse(int vehicleId, DateTime startDate, DateTime endDate)
        {
            var mockContext = _mockData.Context.Object;
            var vehiclesService = new VehiclesService(mockContext);
            var coursesService = new CoursesService(mockContext, vehiclesService);

            bool isAvailable = coursesService.IsVehicleAvailable(vehicleId, startDate, endDate, null);

            Assert.False(isAvailable);
        }

        public static IEnumerable<object[]> OverlappingCourseData =>
            new List<object[]>
            {
                new object[] { 1, new DateTime(2025, 5, 30, 0, 0, 0), new DateTime(2025, 6, 1, 3, 0, 0) },
                new object[] { 1, new DateTime(2025, 6, 1, 3, 0, 0), new DateTime(2025, 6, 8, 3, 0, 0) },
                new object[] { 1, new DateTime(2025, 6, 1, 2, 0, 0), new DateTime(2025, 6, 1, 4, 0, 0) },
                new object[] { 1, new DateTime(2025, 5, 30, 3, 0, 0), new DateTime(2025, 7, 8, 3, 0, 0) },
            };

        [Theory]
        [MemberData(nameof(OverlappingCurrentCourseData))]
        public void IsVehicleAvailable_WhenOverlapsOnlyCurrentlyOverwritenCourse_ShouldReturnTrue(int vehicleId, DateTime startDate, DateTime endDate, int currentCourseId)
        {
            var mockContext = _mockData.Context.Object;
            var vehiclesService = new VehiclesService(mockContext);
            var coursesService = new CoursesService(mockContext, vehiclesService);

            bool isAvailable = coursesService.IsVehicleAvailable(vehicleId, startDate, endDate, currentCourseId);

            Assert.True(isAvailable);
        }

        public static IEnumerable<object[]> OverlappingCurrentCourseData =>
            new List<object[]>
            {
                new object[] { 1, new DateTime(2025, 5, 30, 0, 0, 0), new DateTime(2025, 6, 1, 3, 0, 0), 1 },
                new object[] { 1, new DateTime(2025, 6, 1, 3, 0, 0), new DateTime(2025, 6, 8, 3, 0, 0), 1 },
                new object[] { 1, new DateTime(2025, 6, 1, 2, 0, 0), new DateTime(2025, 6, 1, 4, 0, 0), 1 },
                new object[] { 1, new DateTime(2025, 5, 30, 3, 0, 0), new DateTime(2025, 7, 8, 3, 0, 0), 1 },
            };

        [Theory]
        [MemberData(nameof(NoCourseOverlappingData))]
        public void IsVehicleAvailable_WhenNoCourseOverlaps_ShouldReturnTrue(int vehicleId, DateTime startDate, DateTime endDate)
        {
            var mockContext = _mockData.Context.Object;
            var vehiclesService = new VehiclesService(mockContext);
            var coursesService = new CoursesService(mockContext, vehiclesService);

            bool isAvailable = coursesService.IsVehicleAvailable(vehicleId, startDate, endDate, null);

            Assert.True(isAvailable);
        }

        public static IEnumerable<object[]> NoCourseOverlappingData =>
            new List<object[]>
            {
                new object[] { 1, new DateTime(2024, 5, 30, 0, 0, 0), new DateTime(2024, 6, 1, 3, 0, 0) }
            };
    }
}