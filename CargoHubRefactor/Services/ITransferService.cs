using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITransferService
{
    Task<List<Transfer>> GetAllTransfersAsync();
    Task<Transfer> GetTransferByIdAsync(int id);
    Task<(string message, Transfer? transfer)> AddTransferAsync(Transfer transfer);
    Task<string> UpdateTransferAsync(int id, Transfer transfer);
    Task<string> DeleteTransferAsync(int id);
}
