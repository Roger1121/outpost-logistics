using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using outpost_logistics.Commons;
using outpost_logistics.Data;
using outpost_logistics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace outpost_logistics.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Courses.Include(c => c.Vehicle);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Vehicle)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AvailableVehicles(DateTime courseStart, int distance, int? currentCourseId)
        {
            return Json(FindAvailableVehicles(courseStart, distance, currentCourseId));
        }

        private List<AvailableVehicleViewModel> FindAvailableVehicles(DateTime courseStart, int distance, int? currentCourseId)
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

        private bool IsVehicleAvailable(int id, DateTime courseStart, DateTime estimatedCourseEnd, int? currentCourseId)
        {
            bool isAvailable = _context.Courses
                    .Where(course => course.Id != currentCourseId && course.VehicleId == id && (course.StartDate <= estimatedCourseEnd && course.EndDate >= courseStart)).IsNullOrEmpty();
            return isAvailable;
        }

        private DateTime CalculateCourseEnd(Vehicle vehicle, DateTime courseStart, int distance)
        {
            TimeSpan timeOfTravel = TimeSpan.FromHours(1.0 * distance / vehicle.MaxSpeed);
            int numberOfBreaks = (int) timeOfTravel.Divide(vehicle.MaxContinuousWorkTime);
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

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StartDate,Distance,Description,VehicleId")] Course course)
        {
            if (ModelState.IsValid)
            {
                Vehicle vehicle = _context.Vehicles.Find(course.VehicleId);
                course.EndDate = CalculateCourseEnd(vehicle, course.StartDate, course.Distance);
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            ViewData["VehicleId"] = FindAvailableVehicles(course.StartDate, course.Distance, course.Id)
                .Select(v => new SelectListItemWithDate
                {
                    Value = v.Id.ToString(),
                    Text = v.LicensePlate,
                    DataEstimatedCourseEnd = course.EndDate,
                }).ToList();
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StartDate,EndDate,CreatedDate,Distance,Description,VehicleId")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Vehicle vehicle = _context.Vehicles.Find(course.VehicleId);
                    course.EndDate = CalculateCourseEnd(vehicle, course.StartDate, course.Distance);
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Vehicle)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
    }
}
