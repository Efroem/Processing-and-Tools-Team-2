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

    public async Task<Location> UpdateLocationItemsAsync(int id, List<LocationItem> LocationItems)
    {
        var location = await _context.Locations.FirstOrDefaultAsync(l => l.LocationId == id);
        if (location == null)
        {
            return null;
        }
        foreach (LocationItem ItemToAdd in LocationItems) {
            var inventory = await _context.Inventories.FirstOrDefaultAsync(i => i.ItemId == ItemToAdd.ItemId);
            
            var warehouse = await _context.Warehouses.FindAsync(location.WarehouseId);
            List<string> RestrictedClassifications = warehouse.RestrictedClassificationsList;


            if (inventory == null||
                location.MaxHeight != 0 && ItemToAdd.Height > location.MaxHeight ||
                location.MaxWidth != 0 && ItemToAdd.Width > location.MaxWidth ||
                location.MaxDepth != 0 && ItemToAdd.Depth > location.MaxDepth ||
                RestrictedClassifications.Contains(ItemToAdd.Classification) 
            ) continue;

            inventory.LocationsList.Add(location.LocationId);
            
            if (location.ItemAmounts.ContainsKey(ItemToAdd.ItemId)) {
                location.ItemAmounts[ItemToAdd.ItemId] += ItemToAdd.Amount;
            }
            else {
                location.ItemAmounts.Add(ItemToAdd.ItemId, ItemToAdd.Amount);
            }
        }
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
     public async Task<bool> IsValidLocationNameAsync(string name)
    {
        var regex = new System.Text.RegularExpressions.Regex(@"^Row: ([A-Z]), Rack: (\d+), Shelf: (\d+)$");
        var match = regex.Match(name);

        if (!match.Success)
        {
            return false;
        }

        var row = match.Groups[1].Value;
        var rack = int.Parse(match.Groups[2].Value);
        var shelf = int.Parse(match.Groups[3].Value);

        if (row.Length != 1 || row[0] < 'A' || row[0] > 'Z')
        {
            return false; 
        }

        if (rack < 1 || rack > 100)
        {
            return false; 
        }

        if (shelf < 0 || shelf > 10)
        {
            return false; 
        }

        return true;
    }

}
