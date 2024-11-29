using LoginApp.Data;
using LoginApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Serilog;
using Microsoft.EntityFrameworkCore;

namespace LoginApp.Services
{
    public class MeasurementService
    {
        private readonly ApplicationDbContext _context;

        public MeasurementService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Measurement> GetAllMeasurements()
        {
            try
            {
                return _context.Measurements.ToList();
            }
            catch (InvalidOperationException ex)
            {
                Log.Error(ex, "An invalid operation occurred while fetching all measurements.");
                throw;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while fetching all measurements.");
                throw;
            }
        }

        public Measurement GetMeasurementById(int id)
        {
            try
            {
                return _context.Measurements.Find(id);
            }
            catch (InvalidOperationException ex)
            {
                Log.Error(ex, "An invalid operation occurred while fetching the measurement by ID.");
                throw;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while fetching the measurement by ID.");
                throw;
            }
        }

        public void AddMeasurement(Measurement measurement)
        {
            try
            {
                _context.Measurements.Add(measurement);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                Log.Error(ex, "A database update error occurred while adding the measurement.");
                throw;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while adding the measurement.");
                throw;
            }
        }

        public void UpdateMeasurement(Measurement measurement)
        {
            try
            {
                _context.Measurements.Update(measurement);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                Log.Error(ex, "A database update error occurred while updating the measurement.");
                throw;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while updating the measurement.");
                throw;
            }
        }

        public void UpdateAllMeasurements(List<Measurement> measurements)
        {
            try
            {
                foreach (var measurement in measurements)
                {
                    _context.Measurements.Update(measurement);
                }
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                Log.Error(ex, "A database update error occurred while updating the measurements.");
                throw;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while updating the measurements.");
                throw;
            }
        }

        public void DeleteMeasurement(int id)
        {
            try
            {
                var measurement = _context.Measurements.Find(id);
                if (measurement != null)
                {
                    _context.Measurements.Remove(measurement);
                    _context.SaveChanges();
                }
            }
            catch (DbUpdateException ex)
            {
                Log.Error(ex, "A database update error occurred while deleting the measurement.");
                throw;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while deleting the measurement.");
                throw;
            }
        }
    }
}
