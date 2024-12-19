using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Microsoft.IdentityModel.Tokens;
using System.Data;

namespace UnitTests
{
    [TestClass]
    public class UnitTest_Location
    {
        private CargoHubDbContext _dbContext;
        private LocationService _locationService;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<CargoHubDbContext>()
                .UseInMemoryDatabase(databaseName: "TestLocationDatabase")
                .Options;

            _dbContext = new CargoHubDbContext(options);
            SeedDatabase(_dbContext);
            _locationService = new LocationService(_dbContext);
        }

        private void SeedDatabase(CargoHubDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

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

            context.Warehouses.Add(new Warehouse
            {
                WarehouseId = 1, // Ensure unique WarehouseId
                Code = "WH001",  // Unique code for the warehouse
                Name = "Main Warehouse",
                Address = "123 Warehouse St.",
                Zip = "12345",
                City = "Sample City",
                Province = "Sample Province",
                Country = "Sample Country",
                ContactName = "John Doe",
                ContactPhone = "555-1234",
                ContactEmail = "johndoe@example.com",
                RestrictedClassificationsList = new List<string>{"DummyRestricted"},
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });
            
            context.Locations.Add(new Location
            {
                LocationId = 1,
                Name = "Row: A, Rack: 1, Shelf: 1",
                Code = "LOC001",
                WarehouseId = 1,
                ItemAmounts = new Dictionary<string, int>{},
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });

            context.Locations.Add(new Location
            {
                LocationId = 2,
                Name = "Row: B, Rack: 2, Shelf: 2",
                Code = "LOC002",
                ItemAmounts = new Dictionary<string, int>{},
                WarehouseId = 1,
                MaxHeight = 100,
                MaxWidth = 20,
                MaxDepth = 20,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });
            context.Locations.Add(new Location
            {
                LocationId = 3,
                Name = "Row: C, Rack: 3, Shelf: 3",
                Code = "LOC002",
                WarehouseId = 2,
                ItemAmounts = new Dictionary<string, int>{},
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });
            context.Inventories.Add(new Inventory { 
                InventoryId = 1,  // Ensure unique InventoryId
                ItemId = "P000001",  // Reference the unique ItemId
                Description = "dummy",
                ItemReference = "dummy",
                TotalOnHand = 100,
                TotalExpected = 1,
                TotalOrdered = 1,
                TotalAllocated = 1,
                TotalAvailable = 1,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });
            context.Inventories.Add(new Inventory { 
                InventoryId = 2,  // Ensure unique InventoryId
                ItemId = "P000002",  // Reference the unique ItemId
                Description = "dummy2",
                ItemReference = "dummy2",
                TotalOnHand = 100,
                TotalExpected = 1,
                TotalOrdered = 1,
                TotalAllocated = 1,
                TotalAvailable = 1,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });

            context.SaveChanges();
        }

        // public static IEnumerable<object[]> GetLocationItemsValidData()
        // {
        //     yield return new object[]
        //     {
        //         1, 
        //         new List<LocationItem>
        //         {
        //             new LocationItem { ItemId = "P000001", Amount = 15, Classification = "None", Height = 10, Depth = 10, Width = 10 },
        //             new LocationItem { ItemId = "P00002", Amount = 15, Classification = "None", Height = 10, Depth = 10, Width = 10 }
        //         },
        //         true
        //     };
        // }
        // public static IEnumerable<object[]> GetLocationItemsInvalidItemId()
        // {
        //     yield return new object[]
        //     {
        //         1, 
        //         new List<LocationItem>
        //         {
        //             new LocationItem { ItemId = "P000001", Amount = 15, Classification = "None", Height = 10, Depth = 10, Width = 10 },
        //             new LocationItem { ItemId = "P999999", Amount = 15, Classification = "None", Height = 10, Depth = 10, Width = 10 }
        //         },
        //         false
        //     };
        // }

        [TestMethod]
        public async Task TestGetLocationById()
        {
            var location = await _locationService.GetLocationAsync(1);
            Assert.IsNotNull(location);
            Assert.AreEqual("Row: A, Rack: 1, Shelf: 1", location.Name);
        }

        [TestMethod]
        public async Task TestGetLocationById_NotFound()
        {
            var location = await _locationService.GetLocationAsync(999);
            Assert.IsNull(location);
        }

        [TestMethod]
        public async Task TestGetAllLocations()
        {
            var locations = await _locationService.GetLocationsAsync();
            Assert.AreEqual(3, locations.Count());
        }


        [TestMethod]
        [DataRow("Row: Z, Rack: 150, Shelf: 5", false)]  // Ongeldig rack (150 > 100)
        [DataRow("Row: A, Rack: 5, Shelf: 11", false)]    // Ongeldig shelf (11 > 10)
        [DataRow("Row: AA, Rack: 5, Shelf: 5", false)]    // Ongeldige naam (Row is geen enkele letter)
        [DataRow("Row: A, Rack: 5, Shelf: 5", true)]      // Geldige invoer
        public async Task TestLocationNameValidation(string name, bool expectedResult)
        {
            var isValid = await _locationService.IsValidLocationNameAsync(name);
            Assert.AreEqual(expectedResult, isValid);
        }

        [TestMethod]
        public async Task TestUpdateLocation_ValidData()
        {
            var updatedLocation = await _locationService.UpdateLocationAsync(1, "Row: D, Rack: 4, Shelf: 4", "LOC001-Updated", 1);
            Assert.IsNotNull(updatedLocation);
            Assert.AreEqual("Row: D, Rack: 4, Shelf: 4", updatedLocation.Name);
        }

        [TestMethod]
        public async Task TestUpdateLocationItems_ValidData()
        {
            int id = 1;
            List<LocationItem> locationItems = new List<LocationItem>
            {
                new LocationItem { ItemId = "P000001", Amount = 15, Classification = "None", Height = 10, Depth = 10, Width = 10 },
                new LocationItem { ItemId = "P000002", Amount = 15, Classification = "None", Height = 10, Depth = 10, Width = 10 }
            };
            var updatedLocation = await _locationService.UpdateLocationItemsAsync(id, locationItems);
            Assert.IsTrue(updatedLocation !=  null);
            if (updatedLocation != null) {
                foreach (LocationItem item in locationItems) {
                    Assert.IsTrue(updatedLocation.ItemAmounts.ContainsKey(item.ItemId) && updatedLocation.ItemAmounts[item.ItemId] == item.Amount);
                }
            }

        }

        [TestMethod]
        public async Task TestUpdateLocationItems_InValidItemId()
        {
            int id = 1;
            List<LocationItem> locationItems = new List<LocationItem>
            {
                new LocationItem { ItemId = "P000001", Amount = 15, Classification = "None", Height = 10, Depth = 10, Width = 10 },
                new LocationItem { ItemId = "P999999", Amount = 15, Classification = "None", Height = 10, Depth = 10, Width = 10 }
            };
            var updatedLocation = await _locationService.UpdateLocationItemsAsync(id, locationItems);
            Assert.IsTrue(updatedLocation !=  null);
            Assert.IsTrue(updatedLocation.ItemAmounts.Count == 1);
            

        }

        [TestMethod]
        public async Task TestUpdateLocationItems_TooHighSize()
        {
            int id = 2;
            List<LocationItem> locationItems = new List<LocationItem>
            {
                new LocationItem { ItemId = "P000002", Amount = 15, Classification = "None", Height = 10, Depth = 10, Width = 10 },
                new LocationItem { ItemId = "P000002", Amount = 15, Classification = "None", Height = 100, Depth = 10, Width = 10 }
            };
            var updatedLocation = await _locationService.UpdateLocationItemsAsync(id, locationItems);
            Assert.IsTrue(updatedLocation !=  null);
            Console.WriteLine($"TooHighSize MaxHeight: {updatedLocation.MaxHeight}");
            Console.WriteLine($"TooHighSize ItemCount: {updatedLocation.ItemAmounts.Count}");
            Assert.IsTrue(updatedLocation.ItemAmounts.Count == 1);
        
        }

        [TestMethod]
        [DataRow(10000, 10, 10)]
        [DataRow(10, 10000, 10)]
        [DataRow(10, 10, 10000)]
        public async Task TestUpdateLocationItems_RestrictedCategory(int height, int depth, int width)
        {
            int id = 1;
            List<LocationItem> locationItems = new List<LocationItem>
            {
                new LocationItem { ItemId = "P000001", Amount = 15, Classification = "DummyRestricted", Height = height, Depth = depth, Width = width }
            };
            var updatedLocation = await _locationService.UpdateLocationItemsAsync(id, locationItems);
            Assert.IsTrue(updatedLocation !=  null);
            Assert.IsTrue(updatedLocation.ItemAmounts.Count == 0);
        
        }

        [TestMethod]
        public async Task TestUpdateLocation_NotFound()
        {
            var updatedLocation = await _locationService.UpdateLocationAsync(999, "Row: E, Rack: 5, Shelf: 5", "LOC999", 5);
            Assert.IsNull(updatedLocation);
        }

        [TestMethod]
        public async Task TestDeleteLocation_ValidId()
        {
            var result = await _locationService.DeleteLocationAsync(1);
            Assert.IsTrue(result);

            var location = await _locationService.GetLocationAsync(1);
            Assert.IsNull(location);
        }

        [TestMethod]
        public async Task TestDeleteLocation_InvalidId()
        {
            var result = await _locationService.DeleteLocationAsync(999);
            Assert.IsFalse(result);
        }
    }
}
