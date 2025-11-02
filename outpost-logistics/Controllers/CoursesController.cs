using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using outpost_logistics.Commons;
using outpost_logistics.Data;
using outpost_logistics.Models;
using outpost_logistics.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace outpost_logistics.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private readonly ICoursesService _coursesService;
        private readonly IVehiclesService _vehiclesService;

        public CoursesController(ICoursesService coursesService, IVehiclesService vehiclesService)
        {
            _coursesService = coursesService;
            _vehiclesService = vehiclesService;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            return View(_coursesService.CoursesWithVehicles());
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = _coursesService.FindByIdIncludeVehicle(id);
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
            return Json(_coursesService.FindAvailableVehicles(courseStart, distance, currentCourseId));
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
                Vehicle vehicle = _vehiclesService.FindById(course.VehicleId);
                course.EndDate = _coursesService.CalculateCourseEnd(vehicle, course.StartDate, course.Distance);
                _coursesService.AddCourse(course);
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

            var course = _coursesService.FindById(id);
            if (course == null)
            {
                return NotFound();
            }
            ViewBag.VehicleId = _coursesService.FindAvailableVehicles(course.StartDate, course.Distance, course.Id)
                .Select(v => new SelectListItemWithDate
                {
                    Value = v.Id.ToString(),
                    Text = v.LicensePlate,
                    DataEstimatedCourseEnd = v.EstimatedCourseEnd,
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
                    course.EndDate = _coursesService.CalculateCourseEnd(course.VehicleId, course.StartDate, course.Distance);
                    _coursesService.UpdateCourse(course);
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

            var course = _coursesService.FindByIdIncludeVehicle(id);
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
            _coursesService.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _coursesService.ExistsById(id);
        }
    }
}
