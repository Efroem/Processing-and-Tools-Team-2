namespace UnitTests;
using CargoHubRefactor;
using Microsoft.EntityFrameworkCore;

[TestClass]
public class UnitTest_Inventory
{
    private CargoHubDbContext _dbContext;

    [TestInitialize]
    public void Setup()
    {
        // Ensure Microsoft.EntityFrameworkCore and Microsoft.EntityFrameworkCore.InMemory are installed
        var options = new DbContextOptionsBuilder<CargoHubDbContext>()
            .UseInMemoryDatabase(databaseName: "TestCargoHubDatabase")
            .Options;

        _dbContext = new CargoHubDbContext(options);

        // Seed test data
        SeedDatabase(_dbContext);
    }

    private void SeedDatabase(CargoHubDbContext context)
    {
        context.ItemGroups.Add(new ItemGroup {
            GroupId = 1,
            Name = "dummy",
            Description = "Dummy",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow        
        });

        context.ItemTypes.Add(new ItemType {
            TypeId = 1,
            Name = "dummy",
            Description = "Dummy",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow        
        });

        context.ItemLines.Add(new ItemLine {
            LineId = 1,
            Name = "dummy",
            Description = "Dummy",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow        
        });

        context.Items.Add(new Item {
            Uid = "P000001",
            Code = "Dummy",
            Description = "dummy",
            ShortDescription = "dummy",
            UpcCode = "null",
            ModelNumber = "null",
            CommodityCode = "null",
            ItemLine = 1,
            ItemGroup = 1,
            ItemType = 1,
            UnitPurchaseQuantity = 1,
            UnitOrderQuantity = 1,
            PackOrderQuantity = 1,
            SupplierId = 1,
            SupplierCode = "null",
            SupplierPartNumber = "null",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });

        context.Inventories.Add(new Inventory { 
            InventoryId = 1,
            ItemId = "P000001",
            Description = "dummy",
            ItemReference = "dummy",
            TotalOnHand = 1,
            TotalExpected = 1,
            TotalOrdered = 1,
            TotalAllocated = 1,
            TotalAvailable = 1,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });
        context.SaveChanges();
    }

    // [TestInitialize]
    // public void Setup()
    // {
    //     // Configure DbContextOptions for the in-memory database
    //     var options = new DbContextOptionsBuilder<CargoHubDbContext>()
    //         .UseInMemoryDatabase(databaseName: "TestCargoHubDatabase")
    //         .Options;

    //     // Initialize the in-memory context
    //     _dbContext = new CargoHubDbContext(options);

    //     // Configure DbContextOptions for the real database (for example, SQL Server)
    //     var realDbOptions = new DbContextOptionsBuilder<CargoHubDbContext>()
    //         .UseSqlServer("CargoHubDb") // Real database connection string
    //         .Options;

    //     // Initialize the real database context
    //     _realDbContext = new CargoHubDbContext(realDbOptions);

    //     // Seed the in-memory database with data from the real database
    //     CopyDataFromRealDatabaseToTestDb();
    // }

    // private void CopyDataFromRealDatabaseToTestDb()
    // {
    //     // Query data from the real database (for each entity)
    //     var clients = _realDbContext.Clients.ToList();
    //     var inventories = _realDbContext.Inventories.ToList();
    //     var items = _realDbContext.Items.ToList();
    //     var itemGroups = _realDbContext.ItemGroups.ToList();
    //     var itemLines = _realDbContext.ItemLines.ToList();
    //     var itemTypes = _realDbContext.ItemTypes.ToList();
    //     var locations = _realDbContext.Locations.ToList();
    //     var transfers = _realDbContext.Transfers.ToList();
    //     var warehouses = _realDbContext.Warehouses.ToList();

    //     // Add data to the in-memory database
    //     _dbContext.Clients.AddRange(clients);
    //     _dbContext.Inventories.AddRange(inventories);
    //     _dbContext.Items.AddRange(items);
    //     _dbContext.ItemGroups.AddRange(itemGroups);
    //     _dbContext.ItemLines.AddRange(itemLines);
    //     _dbContext.ItemTypes.AddRange(itemTypes);
    //     _dbContext.Locations.AddRange(locations);
    //     _dbContext.Transfers.AddRange(transfers);
    //     _dbContext.Warehouses.AddRange(warehouses);

    //     // Save the changes to the in-memory database
    //     _dbContext.SaveChanges();
    // }


    [TestMethod]
    public void TestMethod1()
    {
        InventoryService inventoryService= new InventoryService(_dbContext);
        List<Inventory> inventoryList = inventoryService.GetInventoriesAsync().Result;
        Assert.IsTrue(inventoryList.Count == 1);
    }
}