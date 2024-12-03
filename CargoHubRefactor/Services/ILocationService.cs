using System.Collections.Generic;
using System.Threading.Tasks;

public interface ILocationService
{
    Task<Location> GetLocationAsync(int locationId);
    Task<IEnumerable<Location>> GetLocationsAsync();
    Task<Location> AddLocationAsync(string name, string code, int warehouseId);
    Task<Location> UpdateLocationAsync(int id, string name, string code, int warehouseId);
    Task<bool> DeleteLocationAsync(int locationId);
    Task<IEnumerable<Location>> GetLocationsByWarehouseAsync(int warehouseId);
    Task<bool> IsValidLocationNameAsync(string name);}
