using System.Collections.Generic;
using System.Threading.Tasks;

public interface IItemLineService
{
    Task<IEnumerable<ItemLine>> GetItemLinesAsync();
    Task<ItemLine?> GetItemLineByIdAsync(int id);
    Task<(string message, ItemLine? returnedItemLine)> AddItemLineAsync(ItemLine itemLine);
    Task<(string message, ItemLine? returnedItemLine)> UpdateItemLineAsync(int id, ItemLine itemLine);
    Task<string> DeleteItemLineAsync(int id);
}
