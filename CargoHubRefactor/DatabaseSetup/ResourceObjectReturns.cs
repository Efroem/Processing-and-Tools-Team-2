using System.Text.Json;

public class ResourceObjectReturns {
    public ItemGroup ReturnItemGroupObject(Dictionary<string, System.Text.Json.JsonElement> itemGroupJson) {
        ItemGroup returnItemGroupObject = new ItemGroup();
        string format = "yyyy-MM-dd HH:mm:ss";
        try {
            returnItemGroupObject = new ItemGroup {   
                // GroupId = itemGroupJson["id"].GetInt32(),
                Name = itemGroupJson["name"].GetString(),
                
                Description = itemGroupJson["description"].GetString(),

                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };
        } catch (Exception e) {
            Console.WriteLine($"GroupId: {itemGroupJson["id"].GetInt32()}\n {e}");
        }
        try {
            returnItemGroupObject.CreatedAt = DateTime.ParseExact(itemGroupJson["created_at"].GetString(), format, System.Globalization.CultureInfo.InvariantCulture);
            returnItemGroupObject.UpdatedAt = DateTime.ParseExact(itemGroupJson["updated_at"].GetString(), format, System.Globalization.CultureInfo.InvariantCulture);
        } catch (FormatException e) {
            // Do nothing
        }

        if (string.IsNullOrEmpty(returnItemGroupObject.Name)) 
        {
            throw new ArgumentException("Name cannot be null or empty");
        }

        return returnItemGroupObject;
    }

    public ItemLine ReturnItemLineObject(Dictionary<string, System.Text.Json.JsonElement> itemLineJson, Dictionary<int, List<int>> itemGroupRelations) {
        ItemLine returnItemLineObject = new ItemLine();
        int correspondingItemGroup = -1;
        Console.WriteLine($"ItemLine: {itemLineJson["id"].GetInt32()}\n corresponding Item Group: {correspondingItemGroup}");
        string format = "yyyy-MM-dd HH:mm:ss";
        foreach (KeyValuePair<int, List<int>> itemGroup in itemGroupRelations) {
            if(itemGroup.Value.Contains(itemLineJson["id"].GetInt32())) {
                correspondingItemGroup = itemGroup.Key;
                break;
            }
        }
        if (correspondingItemGroup == -1) return null;
                Console.WriteLine($"ItemLine: {itemLineJson["id"].GetInt32()}\n corresponding Item Group: {correspondingItemGroup}");
        try {
            returnItemLineObject = new ItemLine {   
                Name = itemLineJson["name"].GetString(),
                ItemGroup = correspondingItemGroup,
                Description = itemLineJson["description"].GetString(),

                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };
        } catch (Exception e) {
            Console.WriteLine($"GroupId: {itemLineJson["id"].GetInt32()}\n {e}");
        }
        try {
            returnItemLineObject.CreatedAt = DateTime.ParseExact(itemLineJson["created_at"].GetString(), format, System.Globalization.CultureInfo.InvariantCulture);
            returnItemLineObject.UpdatedAt = DateTime.ParseExact(itemLineJson["updated_at"].GetString(), format, System.Globalization.CultureInfo.InvariantCulture);
        } catch (FormatException e) {
            // Do nothing
        }

        if (string.IsNullOrEmpty(returnItemLineObject.Name)) 
        {
            throw new ArgumentException("Name cannot be null or empty");
        }

        return returnItemLineObject;
    }

    public ItemType ReturnItemTypeObject(Dictionary<string, System.Text.Json.JsonElement> itemTypeJson, Dictionary<int, List<int>> itemLineRelations) {
        ItemType returnItemTypeObject = new ItemType();
        int correspondingItemLine = -1;
        string format = "yyyy-MM-dd HH:mm:ss";
        foreach (KeyValuePair<int, List<int>> itemLine in itemLineRelations) {
            if(itemLine.Value.Contains(itemTypeJson["id"].GetInt32())) {
                correspondingItemLine = itemLine.Key;
                break;
            }
        }
        if (correspondingItemLine == -1) return null;
        try {
            returnItemTypeObject = new ItemType {   
                Name = itemTypeJson["name"].GetString(),
                ItemLine = correspondingItemLine,
                Description = itemTypeJson["description"].GetString(),

                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };
        } catch (Exception e) {
            Console.WriteLine($"GroupId: {itemTypeJson["id"].GetInt32()}\n {e}");
        }
        try {
            returnItemTypeObject.CreatedAt = DateTime.ParseExact(itemTypeJson["created_at"].GetString(), format, System.Globalization.CultureInfo.InvariantCulture);
            returnItemTypeObject.UpdatedAt = DateTime.ParseExact(itemTypeJson["updated_at"].GetString(), format, System.Globalization.CultureInfo.InvariantCulture);
        } catch (FormatException e) {
            // Do nothing
        }

        if (string.IsNullOrEmpty(returnItemTypeObject.Name)) 
        {
            throw new ArgumentException("Name cannot be null or empty");
        }

        return returnItemTypeObject;
    }

    public Supplier ReturnSupplierObject(Dictionary<string, System.Text.Json.JsonElement> supplierJson)
    {
        Supplier returnSupplierObject = new Supplier();
        string format = "yyyy-MM-dd HH:mm:ss"; // Define the expected date-time format

        try
        {
            returnSupplierObject = new Supplier
            {
                // Mapping the JSON fields to Supplier properties
                SupplierId = supplierJson["id"].GetInt32(),
                Code = supplierJson["code"].GetString(),
                Name = supplierJson["name"].GetString(),
                Address = supplierJson["address"].GetString(),
                AddressExtra = supplierJson["address_extra"].GetString(),
                City = supplierJson["city"].GetString(),
                ZipCode = supplierJson["zip_code"].GetString(),
                Province = supplierJson["province"].GetString(),
                Country = supplierJson["country"].GetString(),
                ContactName = supplierJson["contact_name"].GetString(),
                PhoneNumber = supplierJson["phonenumber"].GetString(),
                Reference = supplierJson["reference"].GetString(),
                CreatedAt = DateTime.UtcNow, // Default values; will be overridden below
                UpdatedAt = DateTime.UtcNow, // Default values; will be overridden below
            };
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error processing Supplier ID: {supplierJson["id"].GetInt32()}\n {e}");
        }

        // Parse created_at and updated_at with the specific format
        try
        {
            returnSupplierObject.CreatedAt = DateTime.ParseExact(
                supplierJson["created_at"].GetString(),
                format,
                System.Globalization.CultureInfo.InvariantCulture
            );
            returnSupplierObject.UpdatedAt = DateTime.ParseExact(
                supplierJson["updated_at"].GetString(),
                format,
                System.Globalization.CultureInfo.InvariantCulture
            );
        }
        catch (FormatException e)
        {
            Console.WriteLine($"Date parsing error for Supplier ID: {returnSupplierObject.SupplierId}\n {e}");
        }

        return returnSupplierObject;
    }

    public Warehouse ReturnWarehouseObject(Dictionary<string, System.Text.Json.JsonElement> warehouseJson)
    {
        Warehouse returnWarehouseObject = new Warehouse();
        string format = "yyyy-MM-dd HH:mm:ss"; // Define the expected date-time format

        try
        {
            returnWarehouseObject = new Warehouse
            {
                // Mapping the JSON fields to Warehouse properties
                WarehouseId = warehouseJson["id"].GetInt32(),
                Code = warehouseJson["code"].GetString(),
                Name = warehouseJson["name"].GetString(),
                Address = warehouseJson["address"].GetString(),
                Zip = warehouseJson["zip"].GetString(), // Mapping the "zip" field from the JSON to "ZipCode" in Warehouse
                City = warehouseJson["city"].GetString(),
                Province = warehouseJson["province"].GetString(),
                Country = warehouseJson["country"].GetString(),
                
                // Contact is a nested object, so we handle it separately
                ContactName = warehouseJson["contact"].GetProperty("name").GetString(),
                ContactPhone = warehouseJson["contact"].GetProperty("phone").GetString(),
                ContactEmail = warehouseJson["contact"].GetProperty("email").GetString(),

                // Date fields
                CreatedAt = DateTime.ParseExact(warehouseJson["created_at"].GetString(), format, null),
                UpdatedAt = DateTime.ParseExact(warehouseJson["updated_at"].GetString(), format, null),
            };
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error processing Warehouse ID: {warehouseJson["id"].GetInt32()}\n {e}");
        }
        // Parse created_at and updated_at with the specific format
        try
        {
            returnWarehouseObject.CreatedAt = DateTime.ParseExact(
                warehouseJson["created_at"].GetString(),
                format,
                System.Globalization.CultureInfo.InvariantCulture
            );
            returnWarehouseObject.UpdatedAt = DateTime.ParseExact(
                warehouseJson["updated_at"].GetString(),
                format,
                System.Globalization.CultureInfo.InvariantCulture
            );
        }
        catch (FormatException e)
        {
            Console.WriteLine($"Date parsing error for Supplier ID: {returnWarehouseObject.WarehouseId}\n {e}");
        }
        

        return returnWarehouseObject;
    }

    public Client ReturnClientObject(Dictionary<string, System.Text.Json.JsonElement> clientJson)
    {
        Client returnClientObject = new Client();
        string format = "yyyy-MM-dd HH:mm:ss"; // Define the expected date-time format

        try
        {
            returnClientObject = new Client
            {
                // Mapping the JSON fields to Client properties
                ClientId = clientJson["id"].GetInt32(),
                Name = clientJson["name"].GetString(),
                Address = clientJson["address"].GetString(),
                ZipCode = clientJson["zip_code"].GetString(), // Mapping the "zip_code" field from the JSON to "ZipCode" in Client
                City = clientJson["city"].GetString(),
                Province = clientJson["province"].GetString(),
                Country = clientJson["country"].GetString(),
                
                // Contact information mapping
                ContactName = clientJson["contact_name"].GetString(),
                ContactPhone = clientJson["contact_phone"].GetString(),
                ContactEmail = clientJson["contact_email"].GetString(),

                // Date fields
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error processing Client ID: {clientJson["id"].GetInt32()}\n {e}");
        }

        try
        {
            returnClientObject.CreatedAt = DateTime.ParseExact(
                clientJson["created_at"].GetString(),
                format,
                System.Globalization.CultureInfo.InvariantCulture
            );
            returnClientObject.UpdatedAt = DateTime.ParseExact(
                clientJson["updated_at"].GetString(),
                format,
                System.Globalization.CultureInfo.InvariantCulture
            );
        }
        catch (FormatException e)
        {
            Console.WriteLine($"Date parsing error for Supplier ID: {returnClientObject.ClientId}\n {e}");
        }

        return returnClientObject;
    }



    public Item ReturnItemObject(Dictionary<string, System.Text.Json.JsonElement> itemJson)
    {
        Item returnItemObject = new Item();
        string format = "yyyy-MM-dd HH:mm:ss"; // Define the expected date-time format

        try
        {
            returnItemObject = new Item
            {
                // Mapping the JSON fields to Item properties
                Uid = itemJson["uid"].GetString(),
                Code = itemJson["code"].GetString(),
                Description = itemJson["description"].GetString(),
                ShortDescription = itemJson["short_description"].GetString(),
                UpcCode = itemJson["upc_code"].GetString(),
                ModelNumber = itemJson["model_number"].GetString(),
                CommodityCode = itemJson["commodity_code"].GetString(),
                ItemLine = itemJson["item_line"].GetInt32(),
                ItemGroup = itemJson["item_group"].GetInt32(),
                ItemType = itemJson["item_type"].GetInt32(),
                UnitPurchaseQuantity = itemJson["unit_purchase_quantity"].GetInt32(),
                UnitOrderQuantity = itemJson["unit_order_quantity"].GetInt32(),
                PackOrderQuantity = itemJson["pack_order_quantity"].GetInt32(),
                SupplierId = itemJson["supplier_id"].GetInt32(),
                SupplierCode = itemJson["supplier_code"].GetString(),
                SupplierPartNumber = itemJson["supplier_part_number"].GetString(),
                CreatedAt = DateTime.UtcNow, // Default value; will be overridden below
                UpdatedAt = DateTime.UtcNow, // Default value; will be overridden below
            };
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error processing Item UID: {itemJson["uid"].GetString()}\n {e}");
        }

        // Parse created_at and updated_at with the specific format
        try
        {
            returnItemObject.CreatedAt = DateTime.ParseExact(
                itemJson["created_at"].GetString(),
                format,
                System.Globalization.CultureInfo.InvariantCulture
            );
            returnItemObject.UpdatedAt = DateTime.ParseExact(
                itemJson["updated_at"].GetString(),
                format,
                System.Globalization.CultureInfo.InvariantCulture
            );
        }
        catch (FormatException e)
        {
            Console.WriteLine($"Date parsing error for Item UID: {returnItemObject.Uid}\n {e}");
        }

        // Validate mandatory fields
        if (string.IsNullOrEmpty(returnItemObject.Uid))
        {
            throw new ArgumentException("Item UID cannot be null or empty");
        }

        return returnItemObject;
    }


}


