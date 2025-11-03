using outpost_logistics.Services;

namespace outpost_logistics_test.CoursesServiceTests
{
    public class CoursesService_CalculateCourseEnd_Tests
    {
        public readonly MockData _mockData;

        public CoursesService_CalculateCourseEnd_Tests()
        {
            _mockData = new MockData();
        }

        [Theory]
        [MemberData(nameof(CourseData))]
        public void CalculateCourseEnd_WhenCalculatingCourseEnd_ShouldMatchExpected(int vehicleId, DateTime startDate, int distance, DateTime expectedEnd)
        {
            var mockContext = _mockData.Context.Object;
            var vehiclesService = new VehiclesService(mockContext);
            var coursesService = new CoursesService(mockContext, vehiclesService);

            DateTime calculatedCourseEnd = coursesService.CalculateCourseEnd(vehicleId, startDate, distance);

            Assert.Equal(expectedEnd, calculatedCourseEnd);
        }

        public static IEnumerable<object[]> CourseData =>
            new List<object[]>
            {
                new object[] { 1, new DateTime(2025, 6, 1, 0, 0, 0), 720, new DateTime(2025, 6, 1, 8, 0, 0) },
                new object[] { 2, new DateTime(2025, 6, 4, 9, 0, 0), 1000, new DateTime(2025, 6, 4, 20, 0, 0) },
                new object[] { 5, new DateTime(2025, 6, 3, 0, 0, 0), 1500, new DateTime(2025, 6, 4, 7, 0, 0) },
            };
    }
}