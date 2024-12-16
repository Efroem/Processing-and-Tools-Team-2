using System;
using System.ComponentModel.DataAnnotations.Schema;

public class TransferItem
{
    public int TransferItemId { get; set; }
    public int TransferId { get; set; }

    [ForeignKey("Item")] // Indicates this is a foreign key referencing Items
    public string ItemId { get; set; }
    public int Amount { get; set; }

    public Transfer Transfer { get; set; }
    public Item Item { get; set; }
}
