CREATE DATABASE shopeefood_db

CREATE TABLE Cities (
    CityID INT PRIMARY KEY IDENTITY(1, 1),
    CityName NVARCHAR(255) NOT NULL
);

CREATE TABLE Districts (
    DistrictID INT PRIMARY KEY IDENTITY(1, 1),
    DistrictName NVARCHAR(255) NOT NULL,
    CityID INT,
    FOREIGN KEY (CityID) REFERENCES Cities(CityID)
);

CREATE TABLE BusinessFields (
    FieldID INT PRIMARY KEY IDENTITY(1, 1),
    FieldName NVARCHAR(255) NOT NULL
);

CREATE TABLE CityBusinessFields (
    CityID INT,
    FieldID INT,
    PRIMARY KEY (CityID, FieldID),
    FOREIGN KEY (CityID) REFERENCES Cities(CityID),
    FOREIGN KEY (FieldID) REFERENCES BusinessFields(FieldID)
);

CREATE TABLE Shops (
    ShopID INT PRIMARY KEY IDENTITY(1, 1),
    ShopName NVARCHAR(255) NOT NULL,
	ShopImage NVARCHAR(MAX),
	ShopAddress NVARCHAR(MAX),
	ShopUptime VARCHAR(50),
);


CREATE TABLE CityBusinessFieldsShop (
    CityID INT,
    FieldID INT,
	ShopID INT,
    PRIMARY KEY (CityID, FieldID, ShopID),
    FOREIGN KEY (CityID) REFERENCES Cities(CityID),
    FOREIGN KEY (FieldID) REFERENCES BusinessFields(FieldID),
	FOREIGN KEY (ShopID) REFERENCES Shops(ShopID)
);

CREATE TABLE MenuShop(
	CategoryID INT PRIMARY KEY IDENTITY(1, 1),
	ShopID INT,
	CategoryName NVARCHAR(MAX)
);

CREATE TABLE MenuDetailShop(
	CategoryItemID INT IDENTITY(1, 1),
	CategoryID INT,
	CategoryItemName NVARCHAR(MAX),
	CategoryItemDescription NVARCHAR(MAX),
	CategoryItemImage NVARCHAR(MAX),
	CategoryItemPrice INT,

	PRIMARY KEY (CategoryItemID),
    FOREIGN KEY (CategoryID) REFERENCES MenuShop(CategoryID)
);

