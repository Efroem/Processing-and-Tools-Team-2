using System.Collections.Generic;
using System.Threading.Tasks;

public interface IInventoryService
{
    Task<IEnumerable<Inventory>> GetInventoriesAsync();
    Task<Inventory?> GetInventoryByIdAsync(int id);
    Task<(string message, Inventory? returnedInventory)> AddInventoryAsync(Inventory Inventory);
    Task<(string message, Inventory? returnedInventory)> UpdateInventoryAsync(int id, Inventory Inventory);
    Task<string> DeleteInventoryAsync(int id);
}
