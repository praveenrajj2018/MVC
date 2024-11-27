using Microsoft.AspNetCore.Mvc;
using LoginApp.Models;
using LoginApp.Data;
using System.Linq;
using System;
using System.Collections.Generic;

namespace LoginApp.Controllers
{
    public class MeasurementsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MeasurementsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Measurement()
        {
            try
            {
                var measurements = _context.Measurements.ToList();
                return View("~/Views/Home/Measurement.cshtml", measurements);
            }
            catch (Exception ex)
            {
                // Log the error (you can use any logging framework)
                // Log.Error(ex, "An error occurred while fetching measurements.");
                ViewBag.ErrorMessage = "An error occurred while fetching measurements. Please try again later.";
                return View("~/Views/Home/Measurement.cshtml", new List<Measurement>());
            }
        }

        [HttpPost]
        public IActionResult AddMeasurement([FromBody] Measurement measurement)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Measurements.Add(measurement);
                    _context.SaveChanges();
                    return Json(new { success = true, measurement });
                }
                catch (Exception ex)
                {
                    // Log the error (you can use any logging framework)
                    // Log.Error(ex, "An error occurred while adding the measurement.");
                    return Json(new { success = false, message = "An error occurred while adding the measurement. Please try again later." });
                }
            }
            return Json(new { success = false, message = "Validation failed.", errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
        }

        [HttpGet]
        public IActionResult GetMeasurement(int id)
        {
            try
            {
                var measurement = _context.Measurements.Find(id);
                if (measurement == null)
                {
                    return Json(new { success = false, message = "Measurement not found." });
                }
                return Json(new { success = true, measurement });
            }
            catch (Exception ex)
            {
                // Log the error (you can use any logging framework)
                // Log.Error(ex, "An error occurred while fetching the measurement.");
                return Json(new { success = false, message = "An error occurred while fetching the measurement. Please try again later." });
            }
        }

        [HttpPut]
        public IActionResult UpdateMeasurement([FromBody] Measurement measurement)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Measurements.Update(measurement);
                    _context.SaveChanges();
                    return Json(new { success = true, measurement });
                }
                catch (Exception ex)
                {
                    // Log the error (you can use any logging framework)
                    // Log.Error(ex, "An error occurred while updating the measurement.");
                    return Json(new { success = false, message = "An error occurred while updating the measurement. Please try again later." });
                }
            }
            return Json(new { success = false, message = "Validation failed.", errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
        }

        [HttpPut]
        public IActionResult UpdateAllMeasurements([FromBody] List<Measurement> measurements)
        {
            if (measurements == null || !measurements.Any())
            {
                return Json(new { success = false, message = "No measurements to update." });
            }

            try
            {
                foreach (var measurement in measurements)
                {
                    _context.Measurements.Update(measurement);
                }
                _context.SaveChanges();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                // Log the error (you can use any logging framework)
                // Log.Error(ex, "An error occurred while updating the measurements.");
                return Json(new { success = false, message = "An error occurred while updating the measurements. Please try again later." });
            }
        }

        [HttpDelete]
        public IActionResult DeleteMeasurement(int id)
        {
            try
            {
                var measurement = _context.Measurements.Find(id);
                if (measurement == null)
                {
                    return Json(new { success = false, message = "Measurement not found." });
                }
                _context.Measurements.Remove(measurement);
                _context.SaveChanges();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                // Log the error (you can use any logging framework)
                // Log.Error(ex, "An error occurred while deleting the measurement.");
                return Json(new { success = false, message = "An error occurred while deleting the measurement. Please try again later." });
            }
        }
    }
}
