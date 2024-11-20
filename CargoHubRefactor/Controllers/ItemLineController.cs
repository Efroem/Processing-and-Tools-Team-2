using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

[Route("api/v1/Item_Lines")]
[ApiController]
public class ItemLineController : ControllerBase
{
    private readonly IItemLineService _itemLineService;

    public ItemLineController(IItemLineService itemLineService)
    {
        _itemLineService = itemLineService;
    }

    [HttpGet]
    public async Task<ActionResult> GetItemLines()
    {
        var item_lines = _itemLineService.GetItemLinesAsync();
        if (item_lines == null)
        {
            return NotFound("No item lines found.");
        }

        return Ok(item_lines);
    }

    [HttpGet("{lineId}")]
    public async Task<ActionResult> GetItemLineById(int lineId)
    {
        var item_line = await _itemLineService.GetItemLineByIdAsync(lineId);
        if (item_line == null)
        {
            return NotFound($"Item Line with ID {lineId} not found.");
        }

        return Ok(item_line);
    }

    [HttpPost]
    public async Task<ActionResult> AddItemLine([FromBody] ItemLine itemLine)
    {
        var result = await _itemLineService.AddItemLineAsync(itemLine);
        if (result.message.StartsWith("Error"))
        {
            return BadRequest(result.message);
        }
        return Ok(result.returnedItemLine);
    }

    [HttpPut("{lineId}")]
    public async Task<ActionResult> UpdateItemLine(int lineId, [FromBody] ItemLine itemLine)
    {
        var result = await _itemLineService.UpdateItemLineAsync(lineId, itemLine);
        if (result.message.StartsWith("Error"))
        {
            return BadRequest(result.message);
        }
        return Ok(result.returnedItemLine);
    }

    [HttpDelete("{lineId}")]
    public async Task<ActionResult> DeleteItemLine(int lineId)
    {
        var result = await _itemLineService.DeleteItemLineAsync(lineId);
        if (result.StartsWith("Error"))
        {
            return NotFound(result);
        }
        return Ok(result);
    }
}
