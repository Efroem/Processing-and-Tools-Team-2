namespace UnitTests;
using CargoHubRefactor;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/*
WHEN MAKING A NEW UNIT TEST FILE FOR AN ENDPOINT. COPY OVER Setup AND SeedDatabase
*/

[TestClass]
public class UnitTest_ItemTypes
{
    // ================ VV Copy this box when making a new Unit Test file VV ====================
    private CargoHubDbContext _dbContext;
    private ItemTypeService ItemTypeService;
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

        // Seed ItemTypes with unique IDs
        context.ItemGroups.Add(new ItemGroup {
            GroupId = 1,  // Ensure unique TypeId
            Name = "dummy",
            Description = "Dummy",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });

        context.ItemGroups.Add(new ItemGroup {
            GroupId = 2,  // Ensure unique TypeId
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
            ItemLine = 1,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });

        context.ItemTypes.Add(new ItemType {
            TypeId = 2,  // Ensure unique TypeId
            Name = "dummy2",
            Description = "Dummy2",
            ItemLine = 2,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });

        // Add an Item type with a big ID to test with Deleting
        context.ItemTypes.Add(new ItemType {
            TypeId = 100,  // Ensure unique TypeId
            Name = "dummy",
            Description = "Dummy",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });

        // Seed ItemLines with unique IDs
        context.ItemLines.Add(new ItemLine {
            LineId = 1,  // Ensure unique LineId
            Name = "dummy",
            Description = "Dummy",
            ItemGroup = 1,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });

        context.ItemLines.Add(new ItemLine {
            LineId = 2,  // Ensure unique LineId
            Name = "dummy2",
            Description = "Dummy2",
            ItemGroup = 1,
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

        // Seed ItemType with a unique TypeId
        context.Inventories.Add(new Inventory { 
            InventoryId = 1,  // Ensure unique TypeId
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
        ItemTypeService ItemTypeService = new ItemTypeService(_dbContext);
        List<ItemType> ItemTypeList = ItemTypeService.GetItemTypesAsync().Result.ToList();
        Assert.IsTrue(ItemTypeList.Count >= 1);
    }

    [TestMethod]
    [DataRow(1, true)]  
    [DataRow(999, false)] 
    public void TestGetById(int TypeId, Boolean expectedresult) {
        ItemTypeService ItemTypeService = new ItemTypeService(_dbContext);
        ItemType? ItemType = ItemTypeService.GetItemTypeByIdAsync(TypeId).Result;
        Assert.AreEqual((ItemType != null), expectedresult);
    }

    [TestMethod]
    [DataRow(25, "dummy", true)]  
    [DataRow(200, null, false)]  
    public void TestPost(int TypeId, string description, Boolean expectedresult) {
        ItemType ItemType = new ItemType { 
            TypeId = TypeId,  // Ensure unique TypeId
            Name = "dummy",
            Description = description,
            ItemLine = 1,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        ItemTypeService ItemTypeService = new ItemTypeService(_dbContext);
        ItemType? returnedItemType = ItemTypeService.AddItemTypeAsync(ItemType).Result.returnedItemType;
        Assert.AreEqual(returnedItemType != null, expectedresult);
    }


    [TestMethod]
    [DataRow(1, "updatedName", true)]  
    [DataRow(1, null, false)]  
    public void TestPut(int TypeId, string name, Boolean expectedresult) {
        ItemType ItemType = new ItemType { 
            TypeId = TypeId,  // Ensure unique TypeId
            Name = name,
            Description = "dummyUpdated",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        ItemTypeService ItemTypeService = new ItemTypeService(_dbContext);
        ItemType? returnedItemType = ItemTypeService.UpdateItemTypeAsync(TypeId, ItemType).Result.returnedItemType;
        Assert.AreEqual(returnedItemType != null, expectedresult);
    }


    [TestMethod]
    [DataRow(100, true)]
    [DataRow(999999, false)]
    public void TestDelete (int TypeId, bool expectedresult) {
        ItemTypeService ItemTypeService = new ItemTypeService(_dbContext);
        bool succesfullyDeleted = ItemTypeService.DeleteItemTypeAsync(TypeId).Result;
        Assert.AreEqual(succesfullyDeleted, expectedresult);
    }
}