namespace UnitTests;
using CargoHubRefactor;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/*
WHEN MAKING A NEW UNIT TEST FILE FOR AN ENDPOINT. COPY OVER Setup AND SeedDatabase
*/

[TestClass]
public class UnitTest_Inventory
{
    // ================ VV Copy this box when making a new Unit Test file VV ====================
    private CargoHubDbContext _dbContext;
    private InventoryService inventoryService;
    public TestContext TestContext { get; set; }

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
        // Clear existing data to avoid conflicts
        context.Database.EnsureDeleted();  // Ensure database is cleared before seeding
        context.Database.EnsureCreated();  // Create the database if not already created

        // Seed ItemGroups with unique IDs
        context.ItemGroups.Add(new ItemGroup {
            GroupId = 1,  // Ensure unique GroupId
            Name = "dummy",
            Description = "Dummy",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });

        context.ItemGroups.Add(new ItemGroup {
            GroupId = 2,  // Ensure unique GroupId
            Name = "dummy2",
            Description = "Dummy2",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });

        // Seed ItemTypes with unique IDs
        context.ItemTypes.Add(new ItemType {
            TypeId = 1,  // Ensure unique TypeId
            Name = "dummy",
            Description = "Dummy",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });

        context.ItemTypes.Add(new ItemType {
            TypeId = 2,  // Ensure unique TypeId
            Name = "dummy2",
            Description = "Dummy2",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });

        // Seed ItemLines with unique IDs
        context.ItemLines.Add(new ItemLine {
            LineId = 1,  // Ensure unique LineId
            Name = "dummy",
            Description = "Dummy",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });

        context.ItemLines.Add(new ItemLine {
            LineId = 2,  // Ensure unique LineId
            Name = "dummy2",
            Description = "Dummy2",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });

        // Seed Items with unique codes and references
        context.Items.Add(new Item {
            Uid = "P000001",  // Unique Item Uid
            Code = "Dummy",
            Description = "dummy",
            ShortDescription = "dummy",
            UpcCode = "null",
            ModelNumber = "null",
            CommodityCode = "null",
            ItemLine = 1,  // Reference the unique ItemLine ID
            ItemGroup = 1,  // Reference the unique ItemGroup ID
            ItemType = 1,  // Reference the unique ItemType ID
            UnitPurchaseQuantity = 1,
            UnitOrderQuantity = 1,
            PackOrderQuantity = 1,
            SupplierId = 1,
            SupplierCode = "null",
            SupplierPartNumber = "null",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });

        context.Items.Add(new Item {
            Uid = "P000002",  // Unique Item Uid
            Code = "Dummy2",
            Description = "dummy2",
            ShortDescription = "dummy2",
            UpcCode = "null",
            ModelNumber = "null",
            CommodityCode = "null",
            ItemLine = 2,  // Reference the unique ItemLine ID
            ItemGroup = 2,  // Reference the unique ItemGroup ID
            ItemType = 2,  // Reference the unique ItemType ID
            UnitPurchaseQuantity = 1,
            UnitOrderQuantity = 1,
            PackOrderQuantity = 1,
            SupplierId = 2,
            SupplierCode = "null",
            SupplierPartNumber = "null",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });

        // Seed Inventory with a unique InventoryId
        context.Inventories.Add(new Inventory { 
            InventoryId = 1,  // Ensure unique InventoryId
            ItemId = "P000001",  // Reference the unique ItemId
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

    // ======================== ^^ Copy this when making a new Unit Test file ^^ =====================

    [TestMethod]
    public void TestGetAll()
    {
        InventoryService inventoryService = new InventoryService(_dbContext);
        List<Inventory> inventoryList = inventoryService.GetInventoriesAsync().Result;
        Assert.IsTrue(inventoryList.Count >= 1);
    }

    [TestMethod]
    [DataRow(1, true)]  
    [DataRow(999, false)] 
    public void TestGetById(int inventoryId, Boolean expectedresult) {
        InventoryService inventoryService = new InventoryService(_dbContext);
        Inventory? inventory = inventoryService.GetInventoryByIdAsync(inventoryId).Result;
        Assert.AreEqual((inventory != null), expectedresult);
    }

    [TestMethod]
    [DataRow(2, "dummy", true)]  
    [DataRow(4, null, false)]  
    public void TestPost(int inventoryId, string description, Boolean expectedresult) {
        Inventory inventory = new Inventory { 
            InventoryId = inventoryId,  // Ensure unique InventoryId
            ItemId = "P000001",  // Reference the unique ItemId
            Description = description,
            ItemReference = "dummy",
            TotalOnHand = 1,
            TotalExpected = 1,
            TotalOrdered = 1,
            TotalAllocated = 1,
            TotalAvailable = 1,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        InventoryService inventoryService = new InventoryService(_dbContext);
        Inventory? returnedInventory = inventoryService.AddInventoryAsync(inventory).Result.returnedInventory;
        Assert.AreEqual(returnedInventory != null, expectedresult);
    }


    [TestMethod]
    [DataRow(1, "UpdatedReference", true)]  
    [DataRow(1, null, false)]  
    public void TestPut(int inventoryId, string itemReference, Boolean expectedresult) {
        Inventory inventory = new Inventory { 
            InventoryId = inventoryId,  // Ensure unique InventoryId
            ItemId = "P000001",  // Reference the unique ItemId
            Description = "dummyUpdated",
            ItemReference = itemReference,
            TotalOnHand = 0,
            TotalExpected = 1,
            TotalOrdered = 1,
            TotalAllocated = 1,
            TotalAvailable = 1,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        InventoryService inventoryService = new InventoryService(_dbContext);
        Inventory? returnedInventory = inventoryService.UpdateInventoryAsync(inventoryId, inventory).Result.returnedInventory;
        Assert.AreEqual(returnedInventory != null, expectedresult);
    }


    [TestMethod]
    [DataRow(1, true)]
    [DataRow(999999, false)]
    public void TestDelete (int inventoryId, bool expectedresult) {
        InventoryService inventoryService = new InventoryService(_dbContext);
        bool succesfullyDeleted = inventoryService.DeleteInventoryAsync(inventoryId).Result;
        Assert.AreEqual(succesfullyDeleted, expectedresult);
    }
}