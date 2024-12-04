using System.Text.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

public class SetupItems
{
    private readonly CargoHubDbContext _context;

    public SetupItems(CargoHubDbContext context)
    {
        _context = context;
    }
    public List<Dictionary<int, List<int>>> GetItemCategoryRelations()
    {
        List<Dictionary<int, List<int>>> ItemRelationsLists = new List<Dictionary<int, List<int>>>();
        Dictionary<int, List<int>> ItemLineRelations = new Dictionary<int, List<int>>();
        Dictionary<int, List<int>> ItemTypeRelations = new Dictionary<int, List<int>>();
        
        // Correct path to the data file
        string dataFilePath = "../CargoHub/data/";
        string itemDataString = File.ReadAllText($"{dataFilePath}items.json");
        string itemGroupDataString = File.ReadAllText($"{dataFilePath}item_groups.json");
        string itemLineDataString = File.ReadAllText($"{dataFilePath}item_lines.json");
        string itemTypeDataString = File.ReadAllText($"{dataFilePath}item_types.json");
        // Deserialize the JSON string into a List of Dictionaries with JsonElement values
        List<Dictionary<string, JsonElement>> itemData = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(itemDataString);
        List<Dictionary<string, JsonElement>> itemGroupData = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(itemGroupDataString);
        List<Dictionary<string, JsonElement>> itemLineData = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(itemLineDataString);
        List<Dictionary<string, JsonElement>> itemTypeData = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(itemTypeDataString);
        foreach (var item in itemData)
        {
            try
            {
                // Ensure the correct conversion from JsonElement to int using GetInt32
                int itemType = item["item_type"].GetInt32();  // GetInt32() method to safely convert
                int itemLine = item["item_line"].GetInt32();  // GetInt32() method to safely convert
                int itemGroup = item["item_group"].GetInt32();  // GetInt32() method to safely convert

                // Handling ItemTypeRelations
                if (!ItemTypeRelations.ContainsKey(itemType))
                {
                    ItemTypeRelations.Add(itemType, new List<int> { itemLine });
                }
                else
                {
                    ItemTypeRelations[itemType].Add(itemLine);
                }

                // Handling ItemLineRelations
                if (!ItemLineRelations.ContainsKey(itemLine))
                {
                    ItemLineRelations.Add(itemLine, new List<int> { itemGroup });
                }
                else
                {
                    ItemLineRelations[itemLine].Add(itemGroup);
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error converting JSON element: {ex.Message}");
            }
        }

        foreach (var itemGroupJsonObject in itemGroupData) {
            ItemGroup itemGroup = ReturnItemGroupObject(itemGroupJsonObject);
            try{
                _context.ItemGroups.Add(itemGroup);
                Console.WriteLine(itemGroup.Name != null ? itemGroup.Name : "null");
                _context.SaveChanges();
                // Console.WriteLine(_context.ItemGroups.FirstOrDefault(x => x.GroupId == itemGroup.GroupId));
            } catch (Exception ex) {
                Console.WriteLine(ex);
            }
            
        }


        // Print out the counts of the dictionaries (for debugging purposes)
        Console.WriteLine($"ItemTypeRelations count: {ItemTypeRelations.Keys.Count}");
        Console.WriteLine($"ItemLineRelations count: {ItemLineRelations.Keys.Count}");

        // Return the list of dictionaries (you can modify this as needed)
        return ItemRelationsLists;
    }

    public ItemGroup ReturnItemGroupObject(Dictionary<string, System.Text.Json.JsonElement> itemGroupJson) {
        ItemGroup returnItemGroupObject = new ItemGroup();
        string format = "yyyy-MM-dd HH:mm:ss";
        try {
            returnItemGroupObject = new ItemGroup {   
                GroupId = itemGroupJson["id"].GetInt32(),
                Name = itemGroupJson["name"].GetString(),
                
                Description = itemGroupJson["description"].GetString(),
                CreatedAt = DateTime.ParseExact(itemGroupJson["created_at"].GetString(), format, System.Globalization.CultureInfo.InvariantCulture),
                UpdatedAt = DateTime.ParseExact(itemGroupJson["updated_at"].GetString(), format, System.Globalization.CultureInfo.InvariantCulture),
            };
        } catch (Exception e) {
            Console.WriteLine($"GroupId: {itemGroupJson["id"].GetInt32()}\n {e}");
        }
        if (string.IsNullOrEmpty(returnItemGroupObject.Name)) 
        {
            throw new ArgumentException("Name cannot be null or empty");
        }

        return returnItemGroupObject;
    }
}
