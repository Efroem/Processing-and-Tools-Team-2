public class ShipmentItem
{
    public int ShipmentItemId { get; set; }
    public int ShipmentId { get; set; }
    public int ItemId { get; set; }
    public int Amount { get; set; }

    public Shipment Shipment { get; set; }
    public Item Item { get; set; }
}
