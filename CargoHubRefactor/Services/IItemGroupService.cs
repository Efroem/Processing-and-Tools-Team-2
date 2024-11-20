using System.Collections.Generic;
using System.Threading.Tasks;

public interface IItemGroupService
{
    Task<IEnumerable<ItemGroup>> GetItemGroupsAsync();
    Task<ItemGroup?> GetItemGroupByIdAsync(int id);
    Task<(string message, ItemGroup? returnedItemGroup)> AddItemGroupAsync(ItemGroup itemGroup);
    Task<(string message, ItemGroup? returnedItemGroup)> UpdateItemGroupAsync(int id, ItemGroup itemGroup);
    Task<string> DeleteItemGroupAsync(int id);
}
