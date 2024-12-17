public class LocationItem
{
    public string ItemId { get; set; }
    public int Amount { get; set; }
    public string Classification { get; set; } = "None";

    public int Height { get; set; } = 0;
    public int Width { get; set; } = 0;
    public int Depth { get; set; } = 0;
}
