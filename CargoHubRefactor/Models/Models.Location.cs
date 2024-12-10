using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

public class Location
{
    public int LocationId { get; set; }
    public int WarehouseId { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    [NotMapped]
    public Dictionary<string, int>? ItemAmounts { get; set; }
    public string ItemAmountsString {
        get => JsonSerializer.Serialize(ItemAmounts); 
        set
        {
            try
            {
                if (string.IsNullOrEmpty(value))
                {
                    // If the value is null or empty, set an empty dictionary
                    ItemAmounts = new Dictionary<string, int>();
                }
                else
                {
                    // Handle the case where the value is a JSON array (empty or not)
                    ItemAmounts = JsonSerializer.Deserialize<Dictionary<string, int>>(value) ?? new Dictionary<string, int>(); // Default to empty dictionary if deserialization fails
                }
            }
            catch (JsonException ex)
            {
                // Log the exception and the value that caused the error
                Console.WriteLine($"Error deserializing Locations field: {ex.Message}");
                Console.WriteLine($"Invalid Locations value: {value}");
                ItemAmounts = new Dictionary<string, int>();  // Default to an empty dictionary
            }
        }
    }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Warehouse Warehouse { get; set; }
}
