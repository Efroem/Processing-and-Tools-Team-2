using System.Collections.Generic;
using System.Threading.Tasks;

public interface IInventoryService
{
    Task<List<Inventory>> GetInventoriesAsync();
    Task<Inventory?> GetInventoryByIdAsync(int id);
    Task<(string message, Inventory? returnedInventory)> AddInventoryAsync(Inventory Inventory);
    Task<(string message, Inventory? returnedInventory)> UpdateInventoryAsync(int id, Inventory Inventory);
    Task<bool> DeleteInventoryAsync(int id);
}
