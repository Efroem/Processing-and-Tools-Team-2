CREATE DATABASE CargoHub;
GO
USE CargoHub;


-- Table: Warehouses
CREATE TABLE Warehouses (
    warehouse_id INTEGER PRIMARY KEY AUTOINCREMENT,
    code TEXT NOT NULL,
    name TEXT NOT NULL,
    address TEXT,
    zip TEXT,
    city TEXT,
    province TEXT,
    country TEXT,
    contact_name TEXT,
    contact_phone TEXT,
    contact_email TEXT,
    created_at TEXT,
    updated_at TEXT
);

-- Table: Locations
CREATE TABLE Locations (
    location_id INTEGER PRIMARY KEY AUTOINCREMENT,
    warehouse_id INTEGER,
    code TEXT,
    name TEXT,
    created_at TEXT,
    updated_at TEXT,
    FOREIGN KEY (warehouse_id) REFERENCES Warehouses(warehouse_id)
);

-- Table: Suppliers
CREATE TABLE Suppliers (
    supplier_id INTEGER PRIMARY KEY AUTOINCREMENT,
    code TEXT NOT NULL,
    name TEXT NOT NULL,
    address TEXT,
    address_extra TEXT,
    city TEXT,
    zip_code TEXT,
    province TEXT,
    country TEXT,
    contact_name TEXT,
    phonenumber TEXT,
    reference TEXT,
    created_at TEXT,
    updated_at TEXT
);

-- Table: Clients
CREATE TABLE Clients (
    client_id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT NOT NULL,
    address TEXT,
    city TEXT,
    zip_code TEXT,
    province TEXT,
    country TEXT,
    contact_name TEXT,
    contact_phone TEXT,
    contact_email TEXT,
    created_at TEXT,
    updated_at TEXT
);

-- Table: Inventories
CREATE TABLE Inventories (
    inventory_id INTEGER PRIMARY KEY AUTOINCREMENT,
    item_id INTEGER,
    description TEXT,
    item_reference TEXT,
    total_on_hand INTEGER,
    total_expected INTEGER,
    total_ordered INTEGER,
    total_allocated INTEGER,
    total_available INTEGER,
    created_at TEXT,
    updated_at TEXT,
    FOREIGN KEY (item_id) REFERENCES Items(item_id)
);

-- Table: ItemGroups
CREATE TABLE ItemGroups (
    group_id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT NOT NULL,
    description TEXT,
    created_at TEXT,
    updated_at TEXT
);

-- Table: ItemLines
CREATE TABLE ItemLines (
    line_id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT NOT NULL,
    description TEXT,
    created_at TEXT,
    updated_at TEXT
);

-- Table: ItemTypes
CREATE TABLE ItemTypes (
    type_id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT NOT NULL,
    description TEXT,
    created_at TEXT,
    updated_at TEXT
);

-- Table: Items
CREATE TABLE Items (
    item_id INTEGER PRIMARY KEY AUTOINCREMENT,
    code TEXT NOT NULL,
    description TEXT,
    short_description TEXT,
    upc_code TEXT,
    model_number TEXT,
    commodity_code TEXT,
    item_line INTEGER,
    item_group INTEGER,
    item_type INTEGER,
    unit_purchase_quantity INTEGER,
    unit_order_quantity INTEGER,
    pack_order_quantity INTEGER,
    supplier_id INTEGER,
    supplier_code TEXT,
    supplier_part_number TEXT,
    created_at TEXT,
    updated_at TEXT,
    FOREIGN KEY (item_line) REFERENCES ItemLines(line_id),
    FOREIGN KEY (item_group) REFERENCES ItemGroups(group_id),
    FOREIGN KEY (item_type) REFERENCES ItemTypes(type_id),
    FOREIGN KEY (supplier_id) REFERENCES Suppliers(supplier_id)
);

-- Table: Transfers
CREATE TABLE Transfers (
    transfer_id INTEGER PRIMARY KEY AUTOINCREMENT,
    reference TEXT,
    transfer_from INTEGER,
    transfer_to INTEGER,
    transfer_status TEXT,
    created_at TEXT,
    updated_at TEXT,
    FOREIGN KEY (transfer_from) REFERENCES Warehouses(warehouse_id),
    FOREIGN KEY (transfer_to) REFERENCES Warehouses(warehouse_id)
);

-- Table: TransferItems
CREATE TABLE TransferItems (
    transfer_item_id INTEGER PRIMARY KEY AUTOINCREMENT,
    transfer_id INTEGER,
    item_id INTEGER,
    amount INTEGER,
    FOREIGN KEY (transfer_id) REFERENCES Transfers(transfer_id),
    FOREIGN KEY (item_id) REFERENCES Items(item_id)
);

-- Table: Shipments
CREATE TABLE Shipments (
    shipment_id INTEGER PRIMARY KEY AUTOINCREMENT,
    order_id INTEGER,
    source_id INTEGER,
    order_date TEXT,
    request_date TEXT,
    shipment_date TEXT,
    shipment_type TEXT,
    shipment_status TEXT,
    notes TEXT,
    carrier_code TEXT,
    carrier_description TEXT,
    service_code TEXT,
    payment_type TEXT,
    transfer_mode TEXT,
    total_package_count INTEGER,
    total_package_weight REAL,
    created_at TEXT,
    updated_at TEXT,
    FOREIGN KEY (source_id) REFERENCES Warehouses(warehouse_id)
);

-- Table: ShipmentItems
CREATE TABLE ShipmentItems (
    shipment_item_id INTEGER PRIMARY KEY AUTOINCREMENT,
    shipment_id INTEGER,
    item_id INTEGER,
    amount INTEGER,
    FOREIGN KEY (shipment_id) REFERENCES Shipments(shipment_id),
    FOREIGN KEY (item_id) REFERENCES Items(item_id)
);
