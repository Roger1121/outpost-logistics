using Microsoft.EntityFrameworkCore;
using outpost_logistics.Data;
using outpost_logistics.Models;

namespace outpost_logistics.Services
{
    public class VehiclesService : IVehiclesService
    {
        private readonly ApplicationDbContext _context;

        public VehiclesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Vehicle FindById(int? id)
        {
            return _context.Vehicles
                .FirstOrDefault(m => m.Id == id);
        }

        public List<Vehicle> Vehicles()
        {
            return _context.Vehicles.ToList();
        }

        public void AddVehicle(Vehicle vehicle)
        {
            _context.Add(vehicle);
            _context.SaveChanges();
        }

        public void UpdateVehicle(Vehicle vehicle)
        {
            _context.Update(vehicle);
            _context.SaveChanges();
        }

        public void DeleteById(int? id)
        {
            var vehicle = FindById(id);
            if (vehicle != null)
            {
                _context.Vehicles.Remove(vehicle);
            }

            _context.SaveChanges();
        }

        public bool ExistsById(int? id)
        {
            return _context.Vehicles.Any(e => e.Id == id);
        }
    }
}
