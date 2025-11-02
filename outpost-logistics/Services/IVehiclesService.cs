using outpost_logistics.Models;

namespace outpost_logistics.Services
{
    public interface IVehiclesService
    {
        List<Vehicle> Vehicles();
        Vehicle FindById(int? id);
        void AddVehicle(Vehicle vehicle);
        void UpdateVehicle(Vehicle vehicle);
        void DeleteById(int? id);
        bool ExistsById(int? id);
    }
}
