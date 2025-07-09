
CREATE TABLE DepartmentTable (
    Id INT PRIMARY KEY IDENTITY,
    Title NVARCHAR(100) NOT NULL
);

CREATE TABLE LocationTable (
    Id INT PRIMARY KEY IDENTITY,
    Title NVARCHAR(100),
    City NVARCHAR(100),
    State NVARCHAR(50),
    Country NVARCHAR(50),
    Zip INT
);

CREATE TABLE JobManagementTable (
    Id INT PRIMARY KEY IDENTITY,
    Code NVARCHAR(20),
    Title NVARCHAR(100),
    Description NVARCHAR(MAX),
    LocationId INT,
    DepartmentId INT,
    PostedDate DATETIME,
    ClosingDate DATETIME,
    FOREIGN KEY (LocationId) REFERENCES LocationTable(Id),
    FOREIGN KEY (DepartmentId) REFERENCES DepartmentTable(Id)
);

