using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

public class InventoryService : IInventoryService
{
    private readonly CargoHubDbContext _context;

    public InventoryService(CargoHubDbContext context)
    {
        _context = context;
    }

    public async Task<List<Inventory>> GetInventoriesAsync()
    {
        List<Inventory> inventoryList = await _context.Inventories.ToListAsync();
        return inventoryList != null ? inventoryList : new List<Inventory>();
    }

    public async Task<Inventory?> GetInventoryByIdAsync(int id)
    {
        return await _context.Inventories.FindAsync(id);
    }

    public async Task<(string message, Inventory? returnedInventory)> AddInventoryAsync (Inventory inventory)
    {
        int nextId;

        if (string.IsNullOrWhiteSpace(inventory.Description))
            return ("Error: 'Description' field must be filled in.", null);

        if (inventory.ItemReference == null)
            return ("Error: 'ItemReference' must be filled in.", null);

        if (inventory.Description == null)
            return ("Error: 'Description' must be filled in.", null);

        if (inventory.TotalOnHand < 0)
            return ("Error: 'TotalOnHand' cannot be negative.", null);

        if (inventory.TotalExpected < 0)
            return ("Error: 'TotalExpected' cannot be negative.", null);

        if (inventory.TotalOrdered < 0)
            return ("Error: 'TotalOrdered' cannot be negative.", null);

        if (inventory.TotalAllocated < 0)
            return ("Error: 'TotalAllocated' cannot be negative.", null);

        if (inventory.TotalAvailable < 0)
            return ("Error: 'TotalAvailable' cannot be negative.", null);

        if (_context.Inventories.Any())
        {
            nextId = _context.Inventories.Max(c => c.InventoryId) + 1;
        }
        else
        {
            nextId = 1;
        }

        var _Inventory = new Inventory
        {
            InventoryId = nextId,
            ItemId = inventory.ItemId,
            Description = inventory.Description,
            ItemReference = inventory.ItemReference,
            LocationsList = inventory.LocationsList,
            TotalOnHand = inventory.TotalOnHand,
            TotalExpected = inventory.TotalExpected,
            TotalAllocated = inventory.TotalAllocated,
            TotalAvailable = inventory.TotalAvailable,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        if (!_Inventory.LocationsList.IsNullOrEmpty()) {
            int amountPerLocation = _Inventory.LocationsList.Count / _Inventory.TotalOnHand;
            int remainder = _Inventory.LocationsList.Count % _Inventory.TotalOnHand;
            for (int i = 0; i < _Inventory.LocationsList.Count;) {
                int locationId = _Inventory.LocationsList[i];
                var location = await _context.Locations.FindAsync(locationId);
                if (location != null) {
                    if (location.ItemAmounts.ContainsKey(_Inventory.ItemId)) {
                        location.ItemAmounts[_Inventory.ItemId] += remainder == 0 ? amountPerLocation : amountPerLocation + remainder;
                    }
                    else {
                        location.ItemAmounts.Add(_Inventory.ItemId, remainder == 0 ? amountPerLocation : amountPerLocation + remainder);
                    }
                }
                
            }
        }

        await _context.Inventories.AddAsync(_Inventory);
        await _context.SaveChangesAsync();

        return ("", _Inventory);
    }


    public async Task<(string message, Inventory? returnedInventory)> UpdateInventoryAsync(int inventoryId, Inventory Inventory)
    {
        var inventory = await _context.Inventories.FindAsync(inventoryId);
        if (inventory == null)
        {
            return ("Error: Item Group not found.", null);
        }

        // Validate that all fields are filled in
        if (string.IsNullOrWhiteSpace(Inventory.Description))
            return ("Error: 'Description' field must be filled in.", null);
        
        if (string.IsNullOrWhiteSpace(Inventory.ItemReference))
            return ("Error: 'ItemReference' field must be filled in.", null);

        if (Inventory.TotalOnHand < 0)
            return ("Error: 'TotalOnHand' cannot be negative.", null);

        if (Inventory.TotalExpected < 0)
            return ("Error: 'TotalExpected' cannot be negative.", null);

        if (Inventory.TotalOrdered < 0)
            return ("Error: 'TotalOrdered' cannot be negative.", null);

        if (Inventory.TotalAllocated < 0)
            return ("Error: 'TotalAllocated' cannot be negative.", null);

        if (Inventory.TotalAvailable < 0)
            return ("Error: 'TotalAvailable' cannot be negative.", null);

        inventory.ItemId = Inventory.ItemId;
        inventory.Description = Inventory.Description;
        inventory.ItemReference = Inventory.ItemReference;
        inventory.Locations = inventory.Locations;
        inventory.TotalOnHand = Inventory.TotalOnHand;
        inventory.TotalExpected = Inventory.TotalExpected;
        inventory.TotalAllocated = Inventory.TotalAllocated;
        inventory.TotalAvailable = Inventory.TotalAvailable;

        inventory.UpdatedAt = DateTime.Now; // Set UpdatedAt to current time

        await _context.SaveChangesAsync();
        return ("Inventory successfully updated.", inventory);
    }

    public async Task<bool> DeleteInventoryAsync(int inventoryId)
    {
        var inventory = await _context.Inventories.FindAsync(inventoryId);
        if (inventory == null)
        {
            return false;
        }

        _context.Inventories.Remove(inventory);
        await _context.SaveChangesAsync();
        return true;
    }
}
