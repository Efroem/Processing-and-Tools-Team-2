using System.Text.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Reflection;

public class SetupItems
{
    private readonly CargoHubDbContext _context;
    private readonly ResourceObjectReturns objectReturns = new ResourceObjectReturns();
    public SetupItems(CargoHubDbContext context)
    {
        _context = context;
    }
    public List<Dictionary<int, List<int>>> GetItemCategoryRelations()
    {
        List<Dictionary<int, List<int>>> ItemRelationsLists = new List<Dictionary<int, List<int>>>();
        Dictionary<int, List<int>> ItemLineRelations = new Dictionary<int, List<int>>();
        Dictionary<int, List<int>> ItemGroupRelations = new Dictionary<int, List<int>>();
        
        // Correct path to the data file
        string dataFilePath = "../CargoHub/data/";
        string itemDataString = File.ReadAllText($"{dataFilePath}items.json");
        string itemGroupDataString = File.ReadAllText($"{dataFilePath}item_groups.json");
        string itemLineDataString = File.ReadAllText($"{dataFilePath}item_lines.json");
        string itemTypeDataString = File.ReadAllText($"{dataFilePath}item_types.json");
        string supplierDataString = File.ReadAllText($"{dataFilePath}suppliers.json");
        string warehouseDataString = File.ReadAllText($"{dataFilePath}warehouses.json");
        string clientDataString = File.ReadAllText($"{dataFilePath}clients.json");
        // Deserialize the JSON string into a List of Dictionaries with JsonElement values
        List<Dictionary<string, JsonElement>> itemData = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(itemDataString);
        List<Item> itemObjData = JsonSerializer.Deserialize<List<Item>>(itemDataString);
        List<Dictionary<string, JsonElement>> itemGroupData = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(itemGroupDataString);
        List<Dictionary<string, JsonElement>> itemLineData = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(itemLineDataString);
        List<Dictionary<string, JsonElement>> itemTypeData = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(itemTypeDataString);
        List<Dictionary<string, JsonElement>> supplierData = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(supplierDataString);
        List<Dictionary<string, JsonElement>> warehouseData = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(warehouseDataString);
        List<Dictionary<string, JsonElement>> clientData = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(clientDataString);
        foreach (var item in itemData)
        {
            try
            {
                // Ensure the correct conversion from JsonElement to int using GetInt32
                int itemType = item["item_type"].GetInt32();  // GetInt32() method to safely convert
                int itemLine = item["item_line"].GetInt32();  // GetInt32() method to safely convert
                int itemGroup = item["item_group"].GetInt32();  // GetInt32() method to safely convert

                // Handling ItemTypeRelations
                if (!ItemGroupRelations.ContainsKey(itemGroup))
                {
                    ItemGroupRelations.Add(itemGroup, new List<int> { itemLine });
                }
                else
                {
                    ItemGroupRelations[itemGroup].Add(itemLine);
                }

                // Handling ItemLineRelations
                if (!ItemLineRelations.ContainsKey(itemLine))
                {
                    ItemLineRelations.Add(itemLine, new List<int> { itemType });
                }
                else
                {
                    ItemLineRelations[itemLine].Add(itemType);
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error converting JSON element: {ex.Message}");
            }
        }

        // _context.Database.EnsureDeleted();
        // _context.Database.EnsureCreated();
        
        // Load in Item Group
        foreach (var itemGroupJsonObject in itemGroupData) {
            ItemGroup itemGroup = objectReturns.ReturnItemGroupObject(itemGroupJsonObject);
            // PrintAllValues(itemGroup);
            try{
                _context.ItemGroups.Add(itemGroup);
                Console.WriteLine(itemGroup.Name != null ? itemGroup.Name : "null");
                _context.SaveChanges();
                // Console.WriteLine(_context.ItemGroups.FirstOrDefault(x => x.GroupId == itemGroup.GroupId));
            } catch (Exception ex) {
                // Console.WriteLine(ex);
            }
            
        }

        // Load in Item Line
        foreach (var itemLineJsonObject in itemLineData) {
            ItemLine itemLine = objectReturns.ReturnItemLineObject(itemLineJsonObject, ItemGroupRelations);
            if (itemLine == null) continue;
            PrintAllValues(itemLine);
            try{
                _context.ItemLines.Add(itemLine);
                _context.SaveChanges();
                // Console.WriteLine(_context.ItemGroups.FirstOrDefault(x => x.GroupId == itemGroup.GroupId));
            } catch (Exception ex) {
                // Console.WriteLine(ex);
            }
            // if (itemLineJsonObject["id"] == 20) break;
            
        }

        // Load in Item Type
        foreach (var itemTypeJsonObject in itemTypeData) {
            ItemType itemType = objectReturns.ReturnItemTypeObject(itemTypeJsonObject, ItemLineRelations);
            if (itemType == null) continue;
            PrintAllValues(itemType);
            try{
                _context.ItemTypes.Add(itemType);
                Console.WriteLine(itemType.Name != null ? itemType.Name : "null");
                _context.SaveChanges();
                // Console.WriteLine(_context.ItemGroups.FirstOrDefault(x => x.GroupId == itemGroup.GroupId));
            } catch (Exception ex) {
                // Console.WriteLine(ex);
            }
            // if (itemTypeJsonObject["id"] == 20) break;
            
        }

        foreach (var supplierJsonObject in supplierData) {
            Supplier supplier = objectReturns.ReturnSupplierObject(supplierJsonObject);
            if (supplier == null) continue;
            // PrintAllValues(supplier);
            try{
                _context.Suppliers.Add(supplier);
                Console.WriteLine(supplier.Name != null ? supplier.Name : "null");
                _context.SaveChanges();

            } catch (Exception ex) {
                // Console.WriteLine(ex);
            }
   
        }



        foreach (var warehouseJsonObject in warehouseData) {
            Warehouse warehouse = objectReturns.ReturnWarehouseObject(warehouseJsonObject);
            if (warehouse == null) continue;
            // PrintAllValues(supplier);
            try{
                _context.Warehouses.Add(warehouse);
                Console.WriteLine(warehouse.Name != null ? warehouse.Name : "null");
                _context.SaveChanges();

            } catch (Exception ex) {
                // Console.WriteLine(ex);
            }
   
        }

        foreach (var clientJsonObject in clientData) {
            Client client = objectReturns.ReturnClientObject(clientJsonObject);
            if (client == null) continue;
            // PrintAllValues(supplier);
            try{
                _context.Clients.Add(client);
                Console.WriteLine(client.Name != null ? client.Name : "null");
                _context.SaveChanges();

            } catch (Exception ex) {
                // Console.WriteLine(ex);
            }
   
        }

        
        Thread.Sleep(2500);

        // ================ FOR DEBUGGING ==============

        // var itemJson = new Dictionary<string, object>
        // {
        //     { "uid", "P000015" },
        //     { "code", "NaF53949W" },
        //     { "description", "Fundamental motivating moratorium" },
        //     { "short_description", "cultural" },
        //     { "upc_code", "6039750688200" },
        //     { "model_number", "Xp-0262" },
        //     { "commodity_code", "I-652-k1F" },
        //     { "item_line", 59 },
        //     { "item_group", 100 },
        //     { "item_type", 82 },
        //     { "unit_purchase_quantity", 19 },
        //     { "unit_order_quantity", 14 },
        //     { "pack_order_quantity", 23 },
        //     { "supplier_id", 35 },
        //     { "supplier_code", "SUP986" },
        //     { "supplier_part_number", "q-54475-zyc" },
        //     { "created_at", "2003-05-25 23:18:15" },
        //     { "updated_at", "2008-12-09 11:32:07" }
        // };

        // Item returnItemObject = new Item
        //     {
        //         // Mapping the JSON fields to Item properties
        //         Uid = Convert.ToString(itemJson["uid"]),
        //         Code = Convert.ToString(itemJson["code"]),
        //         Description = Convert.ToString(itemJson["description"]),
        //         ShortDescription = Convert.ToString(itemJson["short_description"]),
        //         UpcCode = Convert.ToString(itemJson["upc_code"]),
        //         ModelNumber = Convert.ToString(itemJson["model_number"]),
        //         CommodityCode = Convert.ToString(itemJson["commodity_code"]),
        //         ItemLine = Convert.ToInt32(itemJson["item_line"]),
        //         ItemGroup = Convert.ToInt32(itemJson["item_group"]),
        //         ItemType = Convert.ToInt32(itemJson["item_type"]),
        //         UnitPurchaseQuantity = Convert.ToInt32(itemJson["unit_purchase_quantity"]),
        //         UnitOrderQuantity = Convert.ToInt32(itemJson["unit_order_quantity"]),
        //         PackOrderQuantity = Convert.ToInt32(itemJson["pack_order_quantity"]),
        //         SupplierId = Convert.ToInt32(itemJson["supplier_id"]),
        //         SupplierCode = Convert.ToString(itemJson["supplier_code"]),
        //         SupplierPartNumber = Convert.ToString(itemJson["supplier_part_number"]),
        //         CreatedAt = DateTime.UtcNow, // Default value; will be overridden below
        //         UpdatedAt = DateTime.UtcNow, // Default value; will be overridden below
        //     };

        //     _context.Items.AddAsync(returnItemObject);
        //     // Console.WriteLine(supplier.Name != null ? supplier.Name : "null");
        //     _context.SaveChangesAsync();

        // ==================== ^^FOR DEBUGGING^^ ==========================

        // foreach (var itemJsonObject in itemData) {
        //     if (itemJsonObject["uid"] == "P000020") break;
        //     var itemLineExists = _context.ItemLines.Any(x => x.LineId == itemJsonObject["item_line"]);
        //     var itemGroupExists = _context.ItemGroups.Any(x => x.GroupId == itemJsonObject["item_group"]);
        //     var itemTypeExists = _context.ItemTypes.Any(x => x.TypeId == itemJsonObject["item_type"]);

        //     if (!itemLineExists || !itemGroupExists || !itemTypeExists)
        //     {
        //         Console.WriteLine("One or more foreign key constraints are invalid.");
        //     }
        //     Item item = objectReturns.ReturnItemObject(itemJsonObject);
        //     if (item == null) continue;
        //     Console.WriteLine($"Item ID: {itemJsonObject["uid"]}ItemLine: {item.ItemLine}, ItemGroup: {item.ItemGroup}, ItemType: {item.ItemType}, SupplierId: {item.SupplierId}");
        //     try{
        //         _context.Items.AddAsync(item);
        //         // Console.WriteLine(supplier.Name != null ? supplier.Name : "null");
        //         _context.SaveChangesAsync();

        //     } catch (Exception ex) {
        //         // Console.WriteLine(ex);
        //     }
   
        // }

        // Print out the counts of the dictionaries (for debugging purposes)
        Console.WriteLine($"ItemTypeRelations count: {ItemGroupRelations.Keys.Count}");
        Console.WriteLine($"ItemLineRelations count: {ItemLineRelations.Keys.Count}");

        // Return the list of dictionaries (you can modify this as needed)
        return ItemRelationsLists;
    }

    

    public static void PrintAllValues(object obj)
    {
        if (obj == null)
        {
            Console.WriteLine("Object is null.");
            return;
        }

        Type type = obj.GetType();
        PropertyInfo[] properties = type.GetProperties();

        foreach (var property in properties)
        {
            object value = property.GetValue(obj);
            Console.WriteLine($"{property.Name}: {value}");
        }
    }
}
