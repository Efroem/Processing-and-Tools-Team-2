public class TransferItem
{
    public int TransferItemId { get; set; }
    public int TransferId { get; set; }
    public int ItemId { get; set; }
    public int Amount { get; set; }

    public Transfer Transfer { get; set; }
    public Item Item { get; set; }
}
