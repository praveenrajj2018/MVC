using Microsoft.AspNetCore.Mvc;
using LoginApp.Models;
using LoginApp.Services;
using LoginApp.ViewModels; // Add this line for using MeasurementViewModel
using System;
using System.Collections.Generic;
using System.Linq;
using Serilog;

namespace LoginApp.Controllers
{
    public class MeasurementsController : Controller
    {
        private readonly MeasurementService _measurementService;

        public MeasurementsController(MeasurementService measurementService)
        {
            _measurementService = measurementService;
        }

        public IActionResult Measurement()
        {
            try
            {
                var measurements = _measurementService.GetAllMeasurements();
                var measurementViewModels = measurements.Select(m => new MeasurementViewModel
                {
                    Id = m.Id,
                    MeasurementName = m.MeasurementName,
                    Weight = m.Weight,
                    OG_L = m.OG_L,
                    Source = m.Source
                }).ToList();

                return View("~/Views/Home/Measurement.cshtml", measurementViewModels);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while fetching measurements.");
                ViewBag.ErrorMessage = "An error occurred while fetching measurements. Please try again later.";
                return View("~/Views/Home/Measurement.cshtml", new List<MeasurementViewModel>());
            }
        }

        [HttpPost]
        public IActionResult AddMeasurement([FromBody] MeasurementViewModel measurementViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var measurement = new Measurement
                    {
                        MeasurementName = measurementViewModel.MeasurementName,
                        Weight = measurementViewModel.Weight,
                        OG_L = measurementViewModel.OG_L,
                        Source = measurementViewModel.Source
                    };

                    _measurementService.AddMeasurement(measurement);
                    return Json(new { success = true, measurement });
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "An error occurred while adding the measurement.");
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
                var measurement = _measurementService.GetMeasurementById(id);
                if (measurement == null)
                {
                    return Json(new { success = false, message = "Measurement not found." });
                }
                var measurementViewModel = new MeasurementViewModel
                {
                    Id = measurement.Id,
                    MeasurementName = measurement.MeasurementName,
                    Weight = measurement.Weight,
                    OG_L = measurement.OG_L,
                    Source = measurement.Source
                };
                return Json(new { success = true, measurement = measurementViewModel });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while fetching the measurement.");
                return Json(new { success = false, message = "An error occurred while fetching the measurement. Please try again later." });
            }
        }

        [HttpPut]
        public IActionResult UpdateMeasurement([FromBody] MeasurementViewModel measurementViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var measurement = new Measurement
                    {
                        Id = measurementViewModel.Id,
                        MeasurementName = measurementViewModel.MeasurementName,
                        Weight = measurementViewModel.Weight,
                        OG_L = measurementViewModel.OG_L,
                        Source = measurementViewModel.Source
                    };

                    _measurementService.UpdateMeasurement(measurement);
                    return Json(new { success = true, measurement });
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "An error occurred while updating the measurement.");
                    return Json(new { success = false, message = "An error occurred while updating the measurement. Please try again later." });
                }
            }
            return Json(new { success = false, message = "Validation failed.", errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
        }

        [HttpPut]
        public IActionResult UpdateAllMeasurements([FromBody] List<MeasurementViewModel> measurementViewModels)
        {
            if (measurementViewModels == null || !measurementViewModels.Any())
            {
                return Json(new { success = false, message = "No measurements to update." });
            }

            try
            {
                var measurements = measurementViewModels.Select(m => new Measurement
                {
                    Id = m.Id,
                    MeasurementName = m.MeasurementName,
                    Weight = m.Weight,
                    OG_L = m.OG_L,
                    Source = m.Source
                }).ToList();

                _measurementService.UpdateAllMeasurements(measurements);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while updating the measurements.");
                return Json(new { success = false, message = "An error occurred while updating the measurements. Please try again later." });
            }
        }

        [HttpDelete]
        public IActionResult DeleteMeasurement(int id)
        {
            try
            {
                _measurementService.DeleteMeasurement(id);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while deleting the measurement.");
                return Json(new { success = false, message = "An error occurred while deleting the measurement. Please try again later." });
            }
        }
    }
}