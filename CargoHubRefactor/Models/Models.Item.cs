using System;
using Models;

public class Item
{
    public int ItemId { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public string ShortDescription { get; set; }
    public string UpcCode { get; set; }
    public string ModelNumber { get; set; }
    public string CommodityCode { get; set; }
    public int ItemLine { get; set; }
    public int ItemGroup { get; set; }
    public int ItemType { get; set; }
    public int UnitPurchaseQuantity { get; set; }
    public int UnitOrderQuantity { get; set; }
    public int PackOrderQuantity { get; set; }
    public int SupplierId { get; set; }
    public string SupplierCode { get; set; }
    public string SupplierPartNumber { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public ItemLine Line { get; set; }
    public ItemGroup Group { get; set; }
    public ItemType Type { get; set; }
    public Supplier Supplier { get; set; }
}
