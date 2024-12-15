using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class OrderService : IOrderService
{
    private readonly CargoHubDbContext _context;

    public OrderService(CargoHubDbContext context)
    {
        _context = context;
    }

    public async Task<Order> GetOrderAsync(int orderId)
    {
        return await _context.Orders.FirstOrDefaultAsync(l => l.Id == orderId);
    }

    public async Task<IEnumerable<Order>> GetOrdersAsync()
    {
        return await _context.Orders.ToListAsync();
    }

    public async Task<Order> AddOrderAsync(int sourceId, DateTime orderDate, DateTime requestDate, string reference,
                                           string referenceExtra, string orderStatus, string notes,
                                           string shippingNotes, string pickingNotes, int warehouseId,
                                           int shipTo, int billTo, int shipmentId, double totalAmount,
                                           double totalDiscount, double totalTax, double totalSurcharge)
    {
        int nextId;

        if (_context.Orders.Any())
        {
            nextId = _context.Orders.Max(l => l.Id) + 1;
        }
        else
        {
            nextId = 1;
        }
        var order = new Order
        {
            Id = nextId,
            SourceId = sourceId,
            OrderDate = orderDate,
            RequestDate = requestDate,
            Reference = reference,
            ReferenceExtra = referenceExtra,
            OrderStatus = orderStatus,
            Notes = notes,
            ShippingNotes = shippingNotes,
            PickingNotes = pickingNotes,
            WarehouseId = warehouseId,
            ShipTo = shipTo,
            BillTo = billTo,
            ShipmentId = shipmentId,
            TotalAmount = totalAmount,
            TotalDiscount = totalDiscount,
            TotalTax = totalTax,
            TotalSurcharge = totalSurcharge,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return order;
    }

    public async Task<Order> UpdateOrderAsync(int id, int sourceId, DateTime orderDate, DateTime requestDate,
                                              string reference, string referenceExtra, string orderStatus,
                                              string notes, string shippingNotes, string pickingNotes,
                                              int warehouseId, int shipTo, int billTo, int shipmentId,
                                              double totalAmount, double totalDiscount, double totalTax,
                                              double totalSurcharge)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
        if (order == null)
        {
            return null;
        }

        order.SourceId = sourceId;
        order.OrderDate = orderDate;
        order.RequestDate = requestDate;
        order.Reference = reference;
        order.ReferenceExtra = referenceExtra;
        order.OrderStatus = orderStatus;
        order.Notes = notes;
        order.ShippingNotes = shippingNotes;
        order.PickingNotes = pickingNotes;
        order.WarehouseId = warehouseId;
        order.ShipTo = shipTo;
        order.BillTo = billTo;
        order.ShipmentId = shipmentId;
        order.TotalAmount = totalAmount;
        order.TotalDiscount = totalDiscount;
        order.TotalTax = totalTax;
        order.TotalSurcharge = totalSurcharge;
        order.UpdatedAt = DateTime.UtcNow;

        _context.Orders.Update(order);
        await _context.SaveChangesAsync();

        return order;
    }

    public async Task<bool> DeleteOrderAsync(int orderId)
    {
        var order = await _context.Orders.FindAsync(orderId);
        if (order == null)
        {
            return false;
        }

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Dictionary<string, List<Location>>> GetLocationsForOrderItemsAsync(int orderId)
    {
        // Get the order including its items
        var order = await _context.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == orderId);

        if (order == null || order.Items == null || !order.Items.Any())
            return null;

        // Extract item UIDs from the order
        var itemUids = order.Items.Select(i => i.ItemUid).ToList();

        // Fetch locations where these items exist
        var locations = await _context.Locations
            .Where(l => l.ItemAmounts.Keys.Any(uid => itemUids.Contains(uid)))
            .ToListAsync();

        // Group locations by item UID
        var groupedLocations = locations
            .SelectMany(l => l.ItemAmounts
                .Where(kvp => itemUids.Contains(kvp.Key))
                .Select(kvp => new { ItemUid = kvp.Key, Location = l }))
            .GroupBy(x => x.ItemUid)
            .ToDictionary(g => g.Key, g => g.Select(x => x.Location).ToList());

        return groupedLocations;
    }

}
