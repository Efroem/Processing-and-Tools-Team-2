using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class TransferService : ITransferService
{
    private readonly CargoHubDbContext _context;

    public TransferService(CargoHubDbContext context)
    {
        _context = context;
    }

    public async Task<List<Transfer>> GetAllTransfersAsync()
    {
        return await _context.Transfers
            .Include(t => t.FromWarehouse)
            .Include(t => t.ToWarehouse)
            .ToListAsync();
    }

    public async Task<Transfer> GetTransferByIdAsync(int id)
    {
        return await _context.Transfers
            .Include(t => t.FromWarehouse)
            .Include(t => t.ToWarehouse)
            .FirstOrDefaultAsync(t => t.TransferId == id);
    }

    public async Task<(string message, Transfer? transfer)> AddTransferAsync(Transfer transfer)
    {
        // Validate required fields
        if (string.IsNullOrWhiteSpace(transfer.Reference))
            return ("Error: 'Reference' field must be filled in.", null);
        if (transfer.TransferFrom == transfer.TransferTo)
            return ("Error: 'TransferFrom' and 'TransferTo' must be different.", null);
        if (!await _context.Warehouses.AnyAsync(w => w.WarehouseId == transfer.TransferFrom))
            return ($"Error: Warehouse with ID {transfer.TransferFrom} does not exist.", null);
        if (!await _context.Warehouses.AnyAsync(w => w.WarehouseId == transfer.TransferTo))
            return ($"Error: Warehouse with ID {transfer.TransferTo} does not exist.", null);

        // Check for duplicate references
        if (await _context.Transfers.AnyAsync(t => t.Reference == transfer.Reference))
        {
            return ("Error: A transfer with this reference already exists.", null);
        }

        // Set timestamps
        transfer.CreatedAt = DateTime.UtcNow;
        transfer.UpdatedAt = DateTime.UtcNow;

        // Add the transfer
        _context.Transfers.Add(transfer);
        await _context.SaveChangesAsync();
        return ("Transfer successfully created.", transfer);
    }

    public async Task<string> UpdateTransferAsync(int id, Transfer updatedTransfer)
    {
        var transfer = await _context.Transfers.FindAsync(id);
        if (transfer == null)
        {
            return "Error: Transfer not found.";
        }

        // Validate required fields
        if (string.IsNullOrWhiteSpace(updatedTransfer.Reference))
            return "Error: 'Reference' field must be filled in.";
        if (updatedTransfer.TransferFrom == updatedTransfer.TransferTo)
            return "Error: 'TransferFrom' and 'TransferTo' must be different.";
        if (!await _context.Warehouses.AnyAsync(w => w.WarehouseId == updatedTransfer.TransferFrom))
            return $"Error: Warehouse with ID {updatedTransfer.TransferFrom} does not exist.";
        if (!await _context.Warehouses.AnyAsync(w => w.WarehouseId == updatedTransfer.TransferTo))
            return $"Error: Warehouse with ID {updatedTransfer.TransferTo} does not exist.";

        // Check for duplicate references, excluding the current transfer
        if (await _context.Transfers.AnyAsync(t => t.Reference == updatedTransfer.Reference && t.TransferId != id))
        {
            return "Error: A transfer with this reference already exists.";
        }

        // Update the transfer
        transfer.Reference = updatedTransfer.Reference;
        transfer.TransferFrom = updatedTransfer.TransferFrom;
        transfer.TransferTo = updatedTransfer.TransferTo;
        transfer.TransferStatus = updatedTransfer.TransferStatus;
        transfer.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return "Transfer successfully updated.";
    }

    public async Task<string> DeleteTransferAsync(int id)
    {
        var transfer = await _context.Transfers.FindAsync(id);
        if (transfer == null)
        {
            return "Error: Transfer not found.";
        }

        _context.Transfers.Remove(transfer);
        await _context.SaveChangesAsync();
        return "Transfer successfully deleted.";
    }
}
