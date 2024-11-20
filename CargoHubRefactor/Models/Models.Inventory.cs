using System;
using System.ComponentModel.DataAnnotations.Schema;

public class Inventory
{
    public int InventoryId { get; set; }

    [ForeignKey("Item")] // Indicates this is a foreign key referencing Items
    public string ItemId { get; set; }

    public string Description { get; set; }
    public string ItemReference { get; set; }
    public int TotalOnHand { get; set; }
    public int TotalExpected { get; set; }
    public int TotalOrdered { get; set; }
    public int TotalAllocated { get; set; }
    public int TotalAvailable { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Item Item { get; set; }
}
