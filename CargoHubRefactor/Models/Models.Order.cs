using System;
using System.Collections.Generic;

public class Order
{
    public int Id { get; set; }
    public int SourceId { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime RequestDate { get; set; }
    public string Reference { get; set; }
    public string ReferenceExtra { get; set; }
    public string OrderStatus { get; set; }
    public string Notes { get; set; }
    public string ShippingNotes { get; set; }
    public string PickingNotes { get; set; }
    public int WarehouseId { get; set; }
    public int ShipTo { get; set; }
    public int BillTo { get; set; }
    public int ShipmentId { get; set; }
    public double TotalAmount { get; set; }
    public double TotalDiscount { get; set; }
    public double TotalTax { get; set; }
    public double TotalSurcharge { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Navigation Properties
    public Warehouse Warehouse { get; set; }
    public Client ShipToClient { get; set; }
    public Client BillToClient { get; set; }
    public List<OrderItem> Items { get; set; } = new List<OrderItem>();
}
