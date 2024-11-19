using System.Collections.Generic;
using System.Threading.Tasks;

public interface IItemService
{
    Task<IEnumerable<Item>> GetItemsAsync();
    Task<Item?> GetItemByIdAsync(string uid);
    Task<(string message, Item? returnedItem)> AddItemAsync(Item item);
    Task<(string message, Item? returnedItem)> UpdateItemAsync(int id, Item item);
    Task<string> DeleteItemAsync(string uid);
}
