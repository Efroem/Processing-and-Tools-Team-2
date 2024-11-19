using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/v1/Locations")]
public class LocationController : ControllerBase
{
    private readonly ILocationService _locationService;

    public LocationController(ILocationService locationService)
    {
        _locationService = locationService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetLocation(int id)
    {
        var location = await _locationService.GetLocationAsync(id);
        if (location == null)
        {
            return NotFound($"Location with ID: {id} was not found");
        }
        return Ok(location);
    }

    [HttpGet]
    public async Task<IActionResult> GetLocations()
    {
        var locations = await _locationService.GetLocationsAsync();
        if (locations == null)
        {
            return NotFound($"No locations were found");
        }
        return Ok(locations);
    }

    [HttpGet("warehouse/{warehouseId}")]
    public async Task<IActionResult> GetLocationsByWarehouse(int warehouseId)
    {
        var locations = await _locationService.GetLocationsByWarehouseAsync(warehouseId);
        return Ok(locations);
    }

    [HttpPost]
    public async Task<IActionResult> AddLocation([FromBody] Location location)
    {
        if (location == null || string.IsNullOrEmpty(location.Name) || string.IsNullOrEmpty(location.Code))
        {
            return BadRequest("Location name and code are required.");
        }

        var createdLocation = await _locationService.AddLocationAsync(location.Name, location.Code, location.WarehouseId);
        return Ok(createdLocation);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLocation(int id, [FromBody] Location location)
    {
        if (id != location.LocationId || string.IsNullOrEmpty(location.Name) || string.IsNullOrEmpty(location.Code))
        {
            return BadRequest("Invalid location data.");
        }

        var updatedLocation = await _locationService.UpdateLocationAsync(id, location.Name, location.Code, location.WarehouseId);
        if (updatedLocation == null)
        {
            return NotFound();
        }

        return Ok(updatedLocation);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLocation(int id)
    {
        var result = await _locationService.DeleteLocationAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return Ok($"Succesfully deleted location with ID: {id}");
    }
}
