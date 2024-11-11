using Microsoft.EntityFrameworkCore;

public class CargoHubDbContext : DbContext
{
    public CargoHubDbContext(DbContextOptions<CargoHubDbContext> options) : base(options) { }

    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<ItemGroup> ItemGroups { get; set; }
    public DbSet<ItemLine> ItemLines { get; set; }
    public DbSet<ItemType> ItemTypes { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Transfer> Transfers { get; set; }
    public DbSet<TransferItem> TransferItems { get; set; }
    public DbSet<Shipment> Shipments { get; set; }
    public DbSet<ShipmentItem> ShipmentItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Location - Warehouse (One-to-Many)
        modelBuilder.Entity<Location>()
            .HasOne(l => l.Warehouse)
            .WithMany()
            .HasForeignKey(l => l.WarehouseId)
            .OnDelete(DeleteBehavior.Restrict);

        // Inventory - Item (One-to-One)
        modelBuilder.Entity<Inventory>()
            .HasOne(i => i.Item)
            .WithMany()
            .HasForeignKey(i => i.ItemId)
            .OnDelete(DeleteBehavior.Restrict);

        // Item - ItemLine (Many-to-One)
        modelBuilder.Entity<Item>()
            .HasOne(i => i.Line)
            .WithMany()
            .HasForeignKey(i => i.ItemLine)
            .OnDelete(DeleteBehavior.Restrict);

        // Item - ItemGroup (Many-to-One)
        modelBuilder.Entity<Item>()
            .HasOne(i => i.Group)
            .WithMany()
            .HasForeignKey(i => i.ItemGroup)
            .OnDelete(DeleteBehavior.Restrict);

        // Item - ItemType (Many-to-One)
        modelBuilder.Entity<Item>()
            .HasOne(i => i.Type)
            .WithMany()
            .HasForeignKey(i => i.ItemType)
            .OnDelete(DeleteBehavior.Restrict);

        // Item - Supplier (Many-to-One)
        modelBuilder.Entity<Item>()
            .HasOne(i => i.Supplier)
            .WithMany()
            .HasForeignKey(i => i.SupplierId)
            .OnDelete(DeleteBehavior.Restrict);

        // Transfer - Warehouses (Many-to-One for TransferFrom and TransferTo)
        modelBuilder.Entity<Transfer>()
            .HasOne(t => t.FromWarehouse)
            .WithMany()
            .HasForeignKey(t => t.TransferFrom)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Transfer>()
            .HasOne(t => t.ToWarehouse)
            .WithMany()
            .HasForeignKey(t => t.TransferTo)
            .OnDelete(DeleteBehavior.Restrict);

        // TransferItem - Transfer (Many-to-One)
        modelBuilder.Entity<TransferItem>()
            .HasOne(ti => ti.Transfer)
            .WithMany()
            .HasForeignKey(ti => ti.TransferId)
            .OnDelete(DeleteBehavior.Cascade);

        // TransferItem - Item (Many-to-One)
        modelBuilder.Entity<TransferItem>()
            .HasOne(ti => ti.Item)
            .WithMany()
            .HasForeignKey(ti => ti.ItemId)
            .OnDelete(DeleteBehavior.Restrict);

        // Shipment - Warehouse (Many-to-One)
        modelBuilder.Entity<Shipment>()
            .HasOne(s => s.SourceWarehouse)
            .WithMany()
            .HasForeignKey(s => s.SourceId)
            .OnDelete(DeleteBehavior.Restrict);

        // ShipmentItem - Shipment (Many-to-One)
        modelBuilder.Entity<ShipmentItem>()
            .HasOne(si => si.Shipment)
            .WithMany()
            .HasForeignKey(si => si.ShipmentId)
            .OnDelete(DeleteBehavior.Cascade);

        // ShipmentItem - Item (Many-to-One)
        modelBuilder.Entity<ShipmentItem>()
            .HasOne(si => si.Item)
            .WithMany()
            .HasForeignKey(si => si.ItemId)
            .OnDelete(DeleteBehavior.Restrict);

        // Order - Warehouse (Many-to-One)
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Warehouse)
            .WithMany()
            .HasForeignKey(o => o.WarehouseId)
            .OnDelete(DeleteBehavior.Restrict);

        // Order - ShipToClient (Many-to-One)
        modelBuilder.Entity<Order>()
            .HasOne(o => o.ShipToClient)
            .WithMany()
            .HasForeignKey(o => o.ShipTo)
            .OnDelete(DeleteBehavior.Restrict);

        // Order - BillToClient (Many-to-One)
        modelBuilder.Entity<Order>()
            .HasOne(o => o.BillToClient)
            .WithMany()
            .HasForeignKey(o => o.BillTo)
            .OnDelete(DeleteBehavior.Restrict);

        // OrderItem - Order (Many-to-One)
        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Order)
            .WithMany(o => o.Items)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        // OrderItem - Item (Many-to-One)
        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Item)
            .WithMany()
            .HasForeignKey(oi => oi.ItemId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
