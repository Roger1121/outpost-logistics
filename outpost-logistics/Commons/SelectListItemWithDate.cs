using Microsoft.AspNetCore.Mvc.Rendering;

namespace outpost_logistics.Commons
{
    public class SelectListItemWithDate : SelectListItem
    {
        public DateTime DataEstimatedCourseEnd { get; set; }
    }
}
