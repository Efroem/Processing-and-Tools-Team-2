using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            context.Locations.Add(new Location
            {
                LocationId = 1,
                Name = "Row: A, Rack: 1, Shelf: 1",
                Code = "LOC001",
                WarehouseId = 1,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });

            context.Locations.Add(new Location
            {
                LocationId = 2,
                Name = "Row: B, Rack: 2, Shelf: 2",
                Code = "LOC002",
                WarehouseId = 2,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });
            context.Locations.Add(new Location
            {
                LocationId = 3,
                Name = "Row: C, Rack: 3, Shelf: 3",
                Code = "LOC002",
                WarehouseId = 2,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });

            context.SaveChanges();
        }

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
