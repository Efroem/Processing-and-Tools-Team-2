namespace UnitTests;
using CargoHubRefactor;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Services;
using System.Collections.Generic;
using System.Threading.Tasks;

/*
WHEN MAKING A NEW UNIT TEST FILE FOR AN ENDPOINT. COPY OVER Setup AND SeedDatabase
*/

[TestClass]
public class UnitTest_Supplier
{
    // ================ VV Copy this box when making a new Unit Test file VV ====================
    private CargoHubDbContext _dbContext;
    private SupplierService supplierService;
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

        // Seed Suppliers with unique IDs
        context.Suppliers.Add(new Supplier
        {
            SupplierId = 1,
            Name = "Supplier A",
            Email = "suppliera@example.com",
            Phone = "123-456-7890",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });

        context.Suppliers.Add(new Supplier
        {
            SupplierId = 2,
            Name = "Supplier B",
            Email = "supplierb@example.com",
            Phone = "098-765-4321",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });

        context.SaveChanges();
    }

    // ======================== ^^ Copy this when making a new Unit Test file ^^ =====================

    [TestMethod]
    public void TestGetAllSuppliers()
    {
        SupplierService supplierService = new SupplierService(_dbContext);
        List<Supplier> supplierList = supplierService.GetAllSuppliersAsync().Result;
        Assert.IsTrue(supplierList.Count >= 2);
    }

    [TestMethod]
    [DataRow(1, true)]
    [DataRow(999, false)]
    public void TestGetSupplierById(int supplierId, bool expectedresult)
    {
        SupplierService supplierService = new SupplierService(_dbContext);
        Supplier? supplier = supplierService.GetSupplierByIdAsync(supplierId).Result;
        Assert.AreEqual(supplier != null, expectedresult);
    }

    [TestMethod]
    [DataRow(3, "New Supplier", "new@example.com", true)]
    [DataRow(4, null, "invalid@example.com", false)]
    public void TestPostSupplier(int supplierId, string name, string email, bool expectedresult)
    {
        Supplier supplier = new Supplier
        {
            SupplierId = supplierId,
            Name = name,
            Email = email,
            Phone = "123-123-1234",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        SupplierService supplierService = new SupplierService(_dbContext);
        Supplier? returnedSupplier = supplierService.CreateSupplierAsync(supplier).Result;
        Assert.AreEqual(returnedSupplier != null, expectedresult);
    }

    [TestMethod]
    [DataRow(1, "Updated Supplier", true)]
    [DataRow(999, null, false)]
    public void TestPutSupplier(int supplierId, string name, bool expectedresult)
    {
        Supplier supplier = new Supplier
        {
            SupplierId = supplierId,
            Name = name,
            Email = "updated@example.com",
            Phone = "456-456-4567",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        SupplierService supplierService = new SupplierService(_dbContext);
        bool updated = supplierService.UpdateSupplierAsync(supplierId, supplier).Result;
        Assert.AreEqual(updated, expectedresult);
    }

    [TestMethod]
    [DataRow(1, true)]
    [DataRow(999, false)]
    public void TestDeleteSupplier(int supplierId, bool expectedresult)
    {
        SupplierService supplierService = new SupplierService(_dbContext);
        bool deleted = supplierService.DeleteSupplierAsync(supplierId).Result;
        Assert.AreEqual(deleted, expectedresult);
    }

    [TestMethod]
    public void TestDeleteAllSuppliers()
    {
        SupplierService supplierService = new SupplierService(_dbContext);
        bool deleted = supplierService.DeleteAllSuppliersAsync().Result;
        Assert.IsTrue(deleted);
    }
}
