﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CargoHubRefactor.Migrations
{
    [DbContext(typeof(CargoHubDbContext))]
    partial class CargoHubDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("Client", b =>
                {
                    b.Property<int>("ClientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ContactEmail")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ContactName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ContactPhone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ClientId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Inventory", b =>
                {
                    b.Property<int>("InventoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ItemId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ItemReference")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Locations")
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "locationsString");

                    b.Property<int>("TotalAllocated")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TotalAvailable")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TotalExpected")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TotalOnHand")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TotalOrdered")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("InventoryId");

                    b.HasIndex("ItemId");

                    b.ToTable("Inventories");
                });

            modelBuilder.Entity("Item", b =>
                {
                    b.Property<string>("Uid")
                        .HasColumnType("TEXT")
                        .HasColumnName("Uid");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CommodityCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ItemGroup")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ItemLine")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ItemType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ModelNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PackOrderQuantity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SupplierCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("SupplierId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SupplierPartNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("UnitOrderQuantity")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UnitPurchaseQuantity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UpcCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Uid");

                    b.HasIndex("ItemGroup");

                    b.HasIndex("ItemLine");

                    b.HasIndex("ItemType");

                    b.HasIndex("SupplierId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("ItemGroup", b =>
                {
                    b.Property<int>("GroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("GroupId");

                    b.ToTable("ItemGroups");
                });

            modelBuilder.Entity("ItemLine", b =>
                {
                    b.Property<int>("LineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ItemGroup")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("LineId");

                    b.HasIndex("ItemGroup");

                    b.ToTable("ItemLines");
                });

            modelBuilder.Entity("ItemType", b =>
                {
                    b.Property<int>("TypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ItemLine")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("TypeId");

                    b.HasIndex("ItemLine");

                    b.ToTable("ItemTypes");
                });

            modelBuilder.Entity("Location", b =>
                {
                    b.Property<int>("LocationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("ItemAmountsString")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int>("WarehouseId")
                        .HasColumnType("INTEGER");

                    b.HasKey("LocationId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("Models.Supplier", b =>
                {
                    b.Property<int>("SupplierId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("AddressExtra")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("ContactName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("TEXT");

                    b.Property<string>("Province")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Reference")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.HasKey("SupplierId");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BillTo")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("OrderStatus")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PickingNotes")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Reference")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ReferenceExtra")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("RequestDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("ShipTo")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ShipmentId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ShippingNotes")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("SourceId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("TotalAmount")
                        .HasColumnType("REAL");

                    b.Property<double>("TotalDiscount")
                        .HasColumnType("REAL");

                    b.Property<double>("TotalSurcharge")
                        .HasColumnType("REAL");

                    b.Property<double>("TotalTax")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int>("WarehouseId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("BillTo");

                    b.HasIndex("ShipTo");

                    b.HasIndex("WarehouseId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Amount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ItemId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("OrderId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("Shipment", b =>
                {
                    b.Property<int>("ShipmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CarrierCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CarrierDescription")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("OrderId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PaymentType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("RequestDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("ServiceCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ShipmentDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("ShipmentStatus")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ShipmentType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("SourceId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TotalPackageCount")
                        .HasColumnType("INTEGER");

                    b.Property<double>("TotalPackageWeight")
                        .HasColumnType("REAL");

                    b.Property<string>("TransferMode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("ShipmentId");

                    b.HasIndex("SourceId");

                    b.ToTable("Shipments");
                });

            modelBuilder.Entity("ShipmentItem", b =>
                {
                    b.Property<int>("ShipmentItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Amount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ItemId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ShipmentId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ShipmentItemId");

                    b.HasIndex("ItemId");

                    b.HasIndex("ShipmentId");

                    b.ToTable("ShipmentItems");
                });

            modelBuilder.Entity("Transfer", b =>
                {
                    b.Property<int>("TransferId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Reference")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("TransferFrom")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TransferStatus")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("TransferTo")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("TransferId");

                    b.HasIndex("TransferFrom");

                    b.HasIndex("TransferTo");

                    b.ToTable("Transfers");
                });

            modelBuilder.Entity("TransferItem", b =>
                {
                    b.Property<int>("TransferItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Amount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ItemId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("TransferId")
                        .HasColumnType("INTEGER");

                    b.HasKey("TransferItemId");

                    b.HasIndex("ItemId");

                    b.HasIndex("TransferId");

                    b.ToTable("TransferItems");
                });

            modelBuilder.Entity("Warehouse", b =>
                {
                    b.Property<int>("WarehouseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ContactEmail")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ContactName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ContactPhone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Zip")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("WarehouseId");

                    b.ToTable("Warehouses");
                });

            modelBuilder.Entity("Inventory", b =>
                {
                    b.HasOne("Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Item");
                });

            modelBuilder.Entity("Item", b =>
                {
                    b.HasOne("ItemGroup", "Group")
                        .WithMany()
                        .HasForeignKey("ItemGroup")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ItemLine", "Line")
                        .WithMany()
                        .HasForeignKey("ItemLine")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ItemType", "Type")
                        .WithMany()
                        .HasForeignKey("ItemType")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Models.Supplier", "Supplier")
                        .WithMany()
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("Line");

                    b.Navigation("Supplier");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("ItemLine", b =>
                {
                    b.HasOne("ItemGroup", "Group")
                        .WithMany()
                        .HasForeignKey("ItemGroup")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("ItemType", b =>
                {
                    b.HasOne("ItemLine", "Line")
                        .WithMany()
                        .HasForeignKey("ItemLine")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Line");
                });

            modelBuilder.Entity("Location", b =>
                {
                    b.HasOne("Warehouse", "Warehouse")
                        .WithMany()
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("Order", b =>
                {
                    b.HasOne("Client", "BillToClient")
                        .WithMany()
                        .HasForeignKey("BillTo")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Client", "ShipToClient")
                        .WithMany()
                        .HasForeignKey("ShipTo")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Warehouse", "Warehouse")
                        .WithMany()
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("BillToClient");

                    b.Navigation("ShipToClient");

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("OrderItem", b =>
                {
                    b.HasOne("Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Order", "Order")
                        .WithMany("Items")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Shipment", b =>
                {
                    b.HasOne("Warehouse", "SourceWarehouse")
                        .WithMany()
                        .HasForeignKey("SourceId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("SourceWarehouse");
                });

            modelBuilder.Entity("ShipmentItem", b =>
                {
                    b.HasOne("Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Shipment", "Shipment")
                        .WithMany()
                        .HasForeignKey("ShipmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Shipment");
                });

            modelBuilder.Entity("Transfer", b =>
                {
                    b.HasOne("Warehouse", "FromWarehouse")
                        .WithMany()
                        .HasForeignKey("TransferFrom")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Warehouse", "ToWarehouse")
                        .WithMany()
                        .HasForeignKey("TransferTo")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("FromWarehouse");

                    b.Navigation("ToWarehouse");
                });

            modelBuilder.Entity("TransferItem", b =>
                {
                    b.HasOne("Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Transfer", "Transfer")
                        .WithMany()
                        .HasForeignKey("TransferId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Transfer");
                });

            modelBuilder.Entity("Order", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
