using System.Collections.Generic;
using System.Threading.Tasks;

public interface IItemTypeService
{
    Task<IEnumerable<ItemType>> GetItemTypesAsync();
    Task<ItemType?> GetItemTypeByIdAsync(int id);
    Task<(string message, ItemType? returnedItemType)> AddItemTypeAsync(ItemType itemtype);
    Task<(string message, ItemType? returnedItemType)> UpdateItemTypeAsync(int id, ItemType itemType);
    Task<string> DeleteItemTypeAsync(int id);
}
