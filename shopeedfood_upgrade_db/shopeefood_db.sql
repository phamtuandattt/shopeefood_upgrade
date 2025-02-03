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

CREATE TABLE Employees (
    EmployeeID INT PRIMARY KEY IDENTITY(1,1), -- Unique identifier
    FirstName NVARCHAR(100) NOT NULL, 
    LastName NVARCHAR(100) NOT NULL,
    Gender NVARCHAR(10) CHECK (Gender IN (N'Nam', N'Nữ', N'Khác')), -- Gender selection
    DateOfBirth DATE NOT NULL, -- Birth date
    PhoneNumber VARCHAR(20) UNIQUE, -- Contact number
    Email VARCHAR(255) UNIQUE, -- Email address
    Address TEXT, -- Home address
);




-- CREATE MODELS FROM TABLE EXISTED
--Scaffold-DbContext "server=DESKTOP-KD2BPDJ;database=shopeefood_db;Integrated Security = true;uid=sa;pwd=Aa123456@;TrustServerCertificate=True;MultipleActiveResultSets=true" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
-- UPDATE MODEL 
--Scaffold-DbContext "server=DESKTOP-KD2BPDJ;database=shopeefood_db;Integrated Security = True;uid=sa;pwd=Aa123456@;TrustServerCertificate=True;MultipleActiveResultSets=true" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -force

--CREATE TABLE Employees (
--    EmployeeID INT PRIMARY KEY IDENTITY(1,1), -- Unique identifier
--    FirstName VARCHAR(100) NOT NULL, 
--    LastName VARCHAR(100) NOT NULL,
--    Gender VARCHAR(10) CHECK (Gender IN ('Male', 'Female', 'Other')), -- Gender selection
--    DateOfBirth DATE NOT NULL, -- Birth date
--    PhoneNumber VARCHAR(20) UNIQUE, -- Contact number
--    Email VARCHAR(255) UNIQUE, -- Email address
--    Address TEXT, -- Home address
--    City VARCHAR(100), -- City of residence
--    Country VARCHAR(100), -- Country of residence
    
--    JobTitle VARCHAR(100) NOT NULL, -- Job position
--    DepartmentID INT, -- Foreign key reference to Departments table
--    ManagerID INT NULL, -- Reports to another Employee
--    HireDate DATE NOT NULL, -- Hiring date
--    EmploymentType VARCHAR(50) CHECK (EmploymentType IN ('Full-Time', 'Part-Time', 'Contract')), -- Employment type
    
--    Salary DECIMAL(10,2) CHECK (Salary >= 0), -- Salary details
--    Bonus DECIMAL(10,2) DEFAULT 0, -- Additional bonus
--    BankAccountNumber VARCHAR(50), -- Payment information
    
--    Status VARCHAR(20) CHECK (Status IN ('Active', 'Inactive', 'On Leave', 'Terminated')), -- Employee status
--    CreatedAt DATETIME DEFAULT GETDATE(), -- Record creation timestamp
--    UpdatedAt DATETIME DEFAULT GETDATE() -- Record update timestamp
--);
