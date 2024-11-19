using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class LocationService : ILocationService
{
    private readonly CargoHubDbContext _context;

    public LocationService(CargoHubDbContext context)
    {
        _context = context;
    }

    public async Task<Location> GetLocationAsync(int locationId)
    {
        return await _context.Locations.FirstOrDefaultAsync(l => l.LocationId == locationId);
    }

    public async Task<IEnumerable<Location>> GetLocationsAsync()
    {
        return await _context.Locations.ToListAsync();
    }

    public async Task<Location> AddLocationAsync(string name, string code, int warehouseId)
    {
        int nextId;

        if (_context.Locations.Any())
        {
            nextId = _context.Locations.Max(l => l.LocationId) + 1;
        }
        else
        {
            nextId = 1;
        }

        var location = new Location
        {
            LocationId = nextId,
            Name = name,
            Code = code,
            WarehouseId = warehouseId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Locations.Add(location);
        await _context.SaveChangesAsync();

        return location;
    }

    public async Task<Location> UpdateLocationAsync(int id, string name, string code, int warehouseId)
    {
        var location = await _context.Locations.FirstOrDefaultAsync(l => l.LocationId == id);
        if (location == null)
        {
            return null;
        }

        location.Name = name;
        location.Code = code;
        location.WarehouseId = warehouseId;
        location.UpdatedAt = DateTime.UtcNow;

        _context.Locations.Update(location);
        await _context.SaveChangesAsync();

        return location;
    }

    public async Task<bool> DeleteLocationAsync(int locationId)
    {
        var location = await _context.Locations.FindAsync(locationId);
        if (location == null)
        {
             return false;
        }

        _context.Locations.Remove(location);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Location>> GetLocationsByWarehouseAsync(int warehouseId)
    {
        return await _context.Locations
                             .Where(l => l.WarehouseId == warehouseId)
                             .Include(l => l.Warehouse)
                             .ToListAsync();
    }
}
