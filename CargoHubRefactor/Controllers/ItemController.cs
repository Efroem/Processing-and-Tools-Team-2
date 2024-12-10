using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

[Route("api/v1/Items")]
[ApiController]
public class ItemController : ControllerBase
{
    private readonly IItemService _itemService;

    public ItemController(IItemService itemService)
    {
        _itemService = itemService;
    }

    [HttpGet]
    public async Task<ActionResult> GetItems()
    {
        var item_s = await _itemService.GetItemsAsync();
        if (item_s == null)
        {
            return NotFound("No item lines found.");
        }

        return Ok(item_s);
    }

    [HttpGet("{ItemId}")]
    public async Task<ActionResult> GetItemById(string ItemId)
    {
        var item_ = await _itemService.GetItemByIdAsync(ItemId);
        if (item_ == null)
        {
            return NotFound($"Item  with ID {ItemId} not found.");
        }

        return Ok(item_);
    }

    [HttpGet("{ItemId}/Locations/{LocationId}")]
    public async Task<ActionResult> GetItemAmountAtLocationById(string ItemId, int LocationId)
    {
        var item_ = await _itemService.GetItemAmountAtLocationByIdAsync(ItemId, LocationId);
        if (item_ == null)
        {
            return NotFound($"Item  with ID {ItemId} not found.");
        }

        return Ok(item_);
    }

    [HttpPost]
    public async Task<ActionResult> AddItem([FromBody] Item item)
    {
        var result = await _itemService.AddItemAsync(item);
        if (result.message.StartsWith("Error"))
        {
            return BadRequest(result.message);
        }
        return Ok(result.returnedItem);
    }

    [HttpPut("{ItemId}")]
    public async Task<ActionResult> UpdateItem(string ItemId, [FromBody] Item item)
    {
        var result = await _itemService.UpdateItemAsync(ItemId, item);
        if (result.message.StartsWith("Error"))
        {
            return BadRequest(result.message);
        }
        return Ok(result.returnedItem);
    }

    [HttpDelete("{ItemId}")]
    public async Task<ActionResult> DeleteItem(string ItemId)
    {
        var result = await _itemService.DeleteItemAsync(ItemId);
        if (result == false)
        {
            return NotFound("Error: Item does not exist");
        }
        return Ok("Item successfully deleted");
    }
}
