using System.Collections.Generic;
using System.Threading.Tasks;

public interface IWarehouseService
{
    Task<List<Warehouse>> GetAllWarehousesAsync();
    Task<Warehouse> GetWarehouseByIdAsync(int id);
    Task<(string message, Warehouse? warehouse)> AddWarehouseAsync(WarehouseDto warehouseDto);
    Task<string> UpdateWarehouseAsync(int id, WarehouseDto warehouseDto);
    Task<string> DeleteWarehouseAsync(int id);
}
