namespace UnitTests;
using CargoHubRefactor;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class UnitTest_Client
{
    private CargoHubDbContext _dbContext;
    private ClientService clientService;
    public TestContext TestContext { get; set; }

    [TestInitialize]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<CargoHubDbContext>()
            .UseInMemoryDatabase(databaseName: "TestCargoHubDatabase")
            .Options;

        _dbContext = new CargoHubDbContext(options);
        clientService = new ClientService(_dbContext);
        SeedDatabase(_dbContext);
    }

    private void SeedDatabase(CargoHubDbContext context)
    {
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        context.Clients.Add(new Client
        {
            ClientId = 1,
            Name = "Vincent",
            Address = "123 Street A",
            City = "Oosterland",
            ZipCode = "12345",
            Province = "Zeeland",
            Country = "Netherlands",
            ContactName = "Contact A",
            ContactPhone = "1234567890",
            ContactEmail = "vincent@gmail.com",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });

        context.Clients.Add(new Client
        {
            ClientId = 2,
            Name = "Xander",
            Address = "456 Street B",
            City = "CityB",
            ZipCode = "67890",
            Province = "ProvinceB",
            Country = "Netherlands",
            ContactName = "Contact B",
            ContactPhone = "012312312",
            ContactEmail = "xanderbos@gmail.com",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });

        context.SaveChanges();
    }

    [TestMethod]
    public void TestGetAllClients()
    {
        var clients = clientService.GetClients();
        Assert.AreEqual(2, clients.Count());
    }

    [TestMethod]
    [DataRow(1, true)]
    [DataRow(99999, false)]
    public void TestGetClientById(int clientId, bool exists)
    {
        var client = clientService.GetClient(clientId);
        Assert.AreEqual(exists, client != null);
    }

    [TestMethod]
    public void TestAddClient()
    {
        var newClient = clientService.AddClient(
            "New Client", 
            "789 Street C", 
            "CityC", 
            "33333", 
            "ProvinceC", 
            "CountryC", 
            "Contact C", 
            "1231231234", 
            "contactc@example.com");

        Assert.IsNotNull(newClient);
        Assert.AreEqual(3, _dbContext.Clients.Count());
    }

    [TestMethod]
    [DataRow(1, "Updated Name", true)]
    [DataRow(999, "Nonexistent", false)]
    public void TestUpdateClient(int clientId, string updatedName, bool expectedResult)
    {
        try
        {
            var updatedClient = clientService.UpdateClient(
                clientId, 
                updatedName, 
                "Updated Address", 
                "Updated City", 
                "00000", 
                "Updated Province", 
                "Updated Country", 
                "Updated Contact", 
                "0000000000", 
                "updated@example.com");

            Assert.IsNotNull(updatedClient);
            Assert.AreEqual(updatedName, updatedClient.Name);
        }
        catch (KeyNotFoundException)
        {
            Assert.IsFalse(expectedResult);
        }
    }

    [TestMethod]
    [DataRow(1, true)]
    [DataRow(999, false)]
    public void TestDeleteClient(int clientId, bool expectedResult)
    {
        var result = clientService.DeleteClient(clientId);
        Assert.AreEqual(expectedResult, result);
        if (result)
        {
            var client = _dbContext.Clients.FirstOrDefault(c => c.ClientId == clientId);
            Assert.IsNull(client);
        }
    }
}
