using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using CargoHubRefactor;

namespace CargoHubRefactorTesting
{
    public class WarehousesEndpointTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _clientFactory;

        public WarehousesEndpointTests(WebApplicationFactory<Program> factory)
        {
            _clientFactory = factory;
            _client = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Remove the existing DbContext configuration
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(DbContextOptions<CargoHubDbContext>));
                    if (descriptor != null) services.Remove(descriptor);

                    // Register the DbContext with a consistent in-memory database instance
                    services.AddDbContext<CargoHubDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("TestDb"); // Shared in-memory database
                    });
                });
            }).CreateClient();
        }

        [Fact]
        public async Task Get_AllWarehouses_ReturnsOkAndSingleWarehouse()
        {
            // Ensure a clean database state
            using (var scope = _clientFactory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<CargoHubDbContext>();
                dbContext.Database.EnsureDeleted();  // Clear the database
                dbContext.Database.EnsureCreated();  // Re-create the database schema
            }

            // Arrange: Seed the database by sending a POST request via the API
            var warehouse = new Warehouse
            {
                Code = "WH001",
                Name = "Single Warehouse",
                Address = "123 Main St",
                Zip = "12345",
                City = "CityOne",
                Province = "ProvinceOne",
                Country = "CountryOne",
                ContactName = "Contact One",
                ContactPhone = "123-456-7890",
                ContactEmail = "contact1@example.com",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            // Seed the warehouse via API call
            var postResponse = await _client.PostAsJsonAsync("/api/v1/warehouses", warehouse);
            Assert.Equal(HttpStatusCode.OK, postResponse.StatusCode);

            // Act: Send a GET request to retrieve all warehouses
            var response = await _client.GetAsync("/api/v1/warehouses");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            // Deserialize and verify the response content
            var warehousesFromResponse = await response.Content.ReadFromJsonAsync<List<Warehouse>>();

            Assert.NotNull(warehousesFromResponse);
            Assert.Equal("Single Warehouse", warehousesFromResponse[0].Name);
            Assert.Equal("123 Main St", warehousesFromResponse[0].Address);
        }
    

        [Fact]
        public async Task Post_Warehouse_CreatesWarehouse_ReturnsOkWithMessage()
        {
            // Arrange
            var newWarehouse = new Warehouse
            {
                Code = "WH002",
                Name = "Test Warehouse",
                Address = "123 Test St",
                Zip = "12345",
                City = "Test City",
                Province = "Test Province",
                Country = "Test Country",
                ContactName = "John Doe",
                ContactPhone = "123-456-7890",
                ContactEmail = "johndoe@example.com",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/v1/warehouses", newWarehouse);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode); // Expecting OK

            // Read the response content as a string
            var message = await response.Content.ReadAsStringAsync();

            // Verify that the message matches the expected response
            Assert.Equal("Warehouse successfully created.", message);
        }

        [Fact]
        public async Task Put_Warehouse_UpdatesWarehouse_ReturnsOkWithMessage()
        {
            // First, create a new warehouse to update
            var newWarehouse = new Warehouse
            {
                Code = "WH003",
                Name = "Warehouse to Update",
                Address = "789 Initial St",
                Zip = "67890",
                City = "Initial City",
                Province = "Initial Province",
                Country = "Initial Country",
                ContactName = "Jane Smith",
                ContactPhone = "987-654-3210",
                ContactEmail = "janesmith@example.com",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            var postResponse = await _client.PostAsJsonAsync("/api/v1/warehouses", newWarehouse);
            var warehouseId = 1; // Use a known ID or obtain it from the created warehouse if returned by the API

            // Arrange updated data
            var updatedWarehouse = new Warehouse
            {
                WarehouseId = warehouseId,
                Code = "WH004",
                Name = "Updated Warehouse",
                Address = "456 Update St",
                Zip = "54321",
                City = "Update City",
                Province = "Update Province",
                Country = "Update Country",
                ContactName = "Updated Contact",
                ContactPhone = "555-555-5555",
                ContactEmail = "updated@example.com",
                CreatedAt = newWarehouse.CreatedAt, // Keep the original CreatedAt date
                UpdatedAt = DateTime.Now // Set a new UpdatedAt date
            };

            // Act
            var response = await _client.PutAsJsonAsync($"/api/v1/warehouses/{warehouseId}", updatedWarehouse);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode); // Expecting OK

            // Read the response content as a string
            var message = await response.Content.ReadAsStringAsync();

            // Verify that the message matches the expected response
            Assert.Equal("Warehouse successfully updated.", message);
        }

        [Fact]
        public async Task Delete_Warehouse_RemovesWarehouse_ReturnsOkWithMessage()
        {
            // First, create a new warehouse to delete
            var newWarehouse = new Warehouse
            {
                Code = "WH005",
                Name = "Warehouse to Delete",
                Address = "789 Delete St",
                Zip = "67890",
                City = "Delete City",
                Province = "Delete Province",
                Country = "Delete Country",
                ContactName = "Delete Contact",
                ContactPhone = "000-000-0000",
                ContactEmail = "delete@example.com",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            var postResponse = await _client.PostAsJsonAsync("/api/v1/warehouses", newWarehouse);
            var warehouseId = 1; // Use a known ID or obtain it from the created warehouse if returned by the API

            // Act
            var response = await _client.DeleteAsync($"/api/v1/warehouses/{warehouseId}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode); // Expecting OK

            // Read the response content as a string
            var message = await response.Content.ReadAsStringAsync();

            // Verify that the message matches the expected response
            Assert.Equal("Warehouse successfully deleted.", message);
        }

    }
}
