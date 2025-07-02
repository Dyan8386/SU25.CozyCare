-- ===============================================
-- ✅ 1. CozyCare.IdentityDb - Quản lý tài khoản
-- ===============================================
USE master;
IF DB_ID('CozyCare.IdentityDb') IS NULL
    CREATE DATABASE [CozyCare.IdentityDb];
GO
USE [CozyCare.IdentityDb];
GO

CREATE TABLE Roles (
    roleId INT IDENTITY(1,1) PRIMARY KEY,
    roleName NVARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE AccountStatuses (
    statusId INT IDENTITY(1,1) PRIMARY KEY,
    statusName NVARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE Accounts (
    accountId INT IDENTITY(1,1) PRIMARY KEY,
    email NVARCHAR(255) NOT NULL UNIQUE,
    password NVARCHAR(255) NOT NULL,
    fullName NVARCHAR(255),
    avatar NVARCHAR(255),
    address NVARCHAR(255),
    phone NVARCHAR(20),
    roleId INT NOT NULL,
    statusId INT NOT NULL,
    createdDate DATETIME DEFAULT GETDATE(),
    updatedDate DATETIME,
    createdBy INT,
    updatedBy INT,
    CONSTRAINT FK_Accounts_Roles FOREIGN KEY (roleId) REFERENCES Roles(roleId),
    CONSTRAINT FK_Accounts_Status FOREIGN KEY (statusId) REFERENCES AccountStatuses(statusId),
    CONSTRAINT FK_Accounts_CreatedBy FOREIGN KEY (createdBy) REFERENCES Accounts(accountId),
    CONSTRAINT FK_Accounts_UpdatedBy FOREIGN KEY (updatedBy) REFERENCES Accounts(accountId)
);

INSERT INTO Roles (roleName) VALUES ('ADMIN'), ('CUSTOMER'), ('HOUSEKEEPER'), ('STAFF');
INSERT INTO AccountStatuses (statusName) VALUES ('ACTIVE'), ('INACTIVE');
GO

-- ===============================================
-- ✅ 2. CozyCare.CatalogDb - Dịch vụ & danh mục
-- ===============================================
USE master;
IF DB_ID('CozyCare.CatalogDb') IS NULL
    CREATE DATABASE [CozyCare.CatalogDb];
GO
USE [CozyCare.CatalogDb];
GO

CREATE TABLE Categories (
    categoryId INT IDENTITY(1,1) PRIMARY KEY,
    categoryName NVARCHAR(255) NOT NULL,
    description NVARCHAR(500),
    image NVARCHAR(255),
    isActive BIT DEFAULT 1,
    createdDate DATETIME DEFAULT GETDATE(),
    updatedDate DATETIME
);

CREATE TABLE Services (
    serviceId INT IDENTITY(1,1) PRIMARY KEY,
    categoryId INT NOT NULL,
    serviceName NVARCHAR(255) NOT NULL,
    description NVARCHAR(500),
    image NVARCHAR(255),
    price DECIMAL(10,2) NOT NULL,
    duration INT NOT NULL,
    isActive BIT DEFAULT 1,
    createdDate DATETIME DEFAULT GETDATE(),
    updatedDate DATETIME,
    CONSTRAINT FK_Services_Categories FOREIGN KEY (categoryId) REFERENCES Categories(categoryId)
);

CREATE TABLE ServiceDetails (
    serviceDetailId INT IDENTITY(1,1) PRIMARY KEY,
    serviceId INT NOT NULL,
    optionName NVARCHAR(255) NOT NULL,
    optionType NVARCHAR(50) NOT NULL,
    basePrice DECIMAL(10,2) NOT NULL,
    unit NVARCHAR(50) NOT NULL,
    duration INT NOT NULL,
    description NVARCHAR(1000),
    isActive BIT DEFAULT 1,
    CONSTRAINT FK_ServiceDetails_Services FOREIGN KEY (serviceId) REFERENCES Services(serviceId)
);
GO

-- ===============================================
-- ✅ 3. CozyCare.BookingDb - Đặt dịch vụ
-- ===============================================
USE master;
IF DB_ID('CozyCare.BookingDb') IS NULL
    CREATE DATABASE [CozyCare.BookingDb];
GO
USE [CozyCare.BookingDb];
GO

CREATE TABLE BookingStatuses (
    statusId INT IDENTITY(1,1) PRIMARY KEY,
    statusName NVARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE Bookings (
    bookingId INT IDENTITY(1,1) PRIMARY KEY,
    bookingNumber NVARCHAR(50) NOT NULL UNIQUE,
    customerId INT NOT NULL,
    promotionCode NVARCHAR(50),
    bookingDate DATETIME DEFAULT GETDATE(),
    deadline DATETIME NOT NULL,
    totalAmount DECIMAL(10,2) NOT NULL,
    notes NVARCHAR(500),
    bookingStatusId INT NOT NULL,
    paymentStatusId INT NOT NULL,
    CONSTRAINT FK_Bookings_Status FOREIGN KEY (bookingStatusId) REFERENCES BookingStatuses(statusId)
);

CREATE TABLE BookingDetails (
    detailId INT IDENTITY(1,1) PRIMARY KEY,
    bookingId INT NOT NULL,
    serviceId INT NOT NULL,
    scheduleDatetime DATETIME NOT NULL,
    quantity INT NOT NULL CHECK (quantity > 0),
    unitPrice DECIMAL(10,2) NOT NULL,
    CONSTRAINT FK_Details_Bookings FOREIGN KEY (bookingId) REFERENCES Bookings(bookingId) ON DELETE CASCADE
);

INSERT INTO BookingStatuses (statusName) VALUES ('PENDING'), ('CONFIRMED'), ('CANCELLED');
GO

-- ===============================================
-- ✅ 4. CozyCare.JobDb - Nhận việc & đánh giá
-- ===============================================
USE master;
IF DB_ID('CozyCare.JobDb') IS NULL
    CREATE DATABASE [CozyCare.JobDb];
GO
USE [CozyCare.JobDb];
GO

CREATE TABLE TaskClaimStatuses (
    statusId INT IDENTITY(1,1) PRIMARY KEY,
    statusName NVARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE TaskClaims (
    claimId INT IDENTITY(1,1) PRIMARY KEY,
    detailId INT NOT NULL,
    housekeeperId INT NOT NULL,
    claimDate DATETIME DEFAULT GETDATE(),
    statusId INT NOT NULL,
    note NVARCHAR(500),
    CONSTRAINT FK_Claims_Status FOREIGN KEY (statusId) REFERENCES TaskClaimStatuses(statusId)
);

CREATE TABLE Reviews (
    reviewId INT IDENTITY(1,1) PRIMARY KEY,
    customerId INT NOT NULL,
    detailId INT NOT NULL,
    reviewTarget NVARCHAR(20) NOT NULL CHECK (reviewTarget IN ('SERVICE', 'HOUSEKEEPER')),
    rating INT CHECK (rating BETWEEN 1 AND 5),
    comment NVARCHAR(1000),
    reviewDate DATETIME DEFAULT GETDATE()
);

INSERT INTO TaskClaimStatuses (statusName) VALUES ('CLAIMED'), ('COMPLETED'), ('CANCELLED');
GO

-- ===============================================
-- ✅ 5. CozyCare.PaymentDb - Thanh toán & khuyến mãi
-- ===============================================
USE master;
IF DB_ID('CozyCare.PaymentDb') IS NULL
    CREATE DATABASE [CozyCare.PaymentDb];
GO
USE [CozyCare.PaymentDb];
GO

CREATE TABLE PaymentStatuses (
    statusId INT IDENTITY(1,1) PRIMARY KEY,
    statusName NVARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE Promotions (
    code NVARCHAR(50) PRIMARY KEY,
    discountType NVARCHAR(20) NOT NULL CHECK (discountType IN ('AMOUNT', 'PERCENT')),
    discountAmount DECIMAL(10,2),
    discountPercent FLOAT,
    startDate DATETIME NOT NULL,
    endDate DATETIME NOT NULL,
    minOrderAmount DECIMAL(10,2),
    maxDiscountAmount DECIMAL(10,2),
    createdDate DATETIME DEFAULT GETDATE()
);

CREATE TABLE Payments (
    paymentId INT IDENTITY(1,1) PRIMARY KEY,
    bookingId INT NOT NULL,
    amount DECIMAL(10,2) NOT NULL,
    paymentMethod NVARCHAR(50) NOT NULL,
    statusId INT NOT NULL,
    paymentDate DATETIME DEFAULT GETDATE(),
    createdDate DATETIME DEFAULT GETDATE(),
    updatedDate DATETIME,
    notes NVARCHAR(500),
    CONSTRAINT FK_Payments_Status FOREIGN KEY (statusId) REFERENCES PaymentStatuses(statusId)
);

INSERT INTO PaymentStatuses (statusName) VALUES ('UNPAID'), ('PAID'), ('REFUNDED');
GO
