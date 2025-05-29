USE master

-- Tạo cơ sở dữ liệu
CREATE DATABASE [CozyCare];
GO

USE [SP25_SWD392_CozyCare]
GO

-- Tạo bảng Roles (vai trò người dùng)
CREATE TABLE [dbo].[Roles] (
    [roleId] [int] IDENTITY(1,1) NOT NULL,
    [roleName] [nvarchar](50) NOT NULL UNIQUE,
    PRIMARY KEY CLUSTERED ([roleId] ASC)
);
GO

-- Tạo bảng AccountStatuses (trạng thái tài khoản)
CREATE TABLE [dbo].[AccountStatuses] (
    [statusId] [int] IDENTITY(1,1) NOT NULL,
    [statusName] [nvarchar](50) NOT NULL UNIQUE,
    PRIMARY KEY CLUSTERED ([statusId] ASC)
);
GO

-- Tạo bảng Accounts (tài khoản người dùng)
CREATE TABLE [dbo].[Accounts] (
    [accountId] [int] IDENTITY(1,1) NOT NULL,
    [email] [nvarchar](255) NOT NULL UNIQUE,
    [password] [nvarchar](255) NOT NULL,
    [address] [nvarchar](255) NULL,
    [phone] [nvarchar](20) NULL,
    [fullName] [nvarchar](255) NULL,
    [avatar] [nvarchar](255) NULL,
    [roleId] [int] NOT NULL,
    [statusId] [int] NOT NULL,
    [createdDate] [datetime] DEFAULT GETDATE(),
    [updatedDate] [datetime] NULL,
    [createdBy] [int] NULL,
    [updatedBy] [int] NULL,
    PRIMARY KEY CLUSTERED ([accountId] ASC),
    CONSTRAINT [FK_Accounts_Roles] FOREIGN KEY ([roleId]) REFERENCES [dbo].[Roles] ([roleId]),
    CONSTRAINT [FK_Accounts_AccountStatuses] FOREIGN KEY ([statusId]) REFERENCES [dbo].[AccountStatuses] ([statusId]),
    CONSTRAINT [FK_Accounts_CreatedBy] FOREIGN KEY ([createdBy]) REFERENCES [dbo].[Accounts] ([accountId]),
    CONSTRAINT [FK_Accounts_UpdatedBy] FOREIGN KEY ([updatedBy]) REFERENCES [dbo].[Accounts] ([accountId])
);
GO

-- Tạo bảng Housekeepers (thông tin người giúp việc)
CREATE TABLE [dbo].[Housekeepers] (
    [housekeeperId] [int] IDENTITY(1,1) NOT NULL,
    [accountId] [int] NOT NULL,
    [rating] [float] NULL,
    [experience] [int] NULL,
    PRIMARY KEY CLUSTERED ([housekeeperId] ASC),
    CONSTRAINT [FK_Housekeepers_Accounts] FOREIGN KEY ([accountId]) REFERENCES [dbo].[Accounts] ([accountId]) ON DELETE CASCADE
);
GO

-- Tạo bảng Categories (danh mục dịch vụ)
CREATE TABLE [dbo].[Categories] (
    [categoryId] [int] IDENTITY(1,1) NOT NULL,
    [categoryName] [nvarchar](255) NOT NULL,
    [description] [nvarchar](500) NULL,
    [image] [nvarchar](255) NULL,
    [isActive] [bit] DEFAULT 1,
    [createdDate] [datetime] DEFAULT GETDATE(),
    [updatedDate] [datetime] NULL,
    PRIMARY KEY CLUSTERED ([categoryId] ASC)
);
GO

-- Tạo bảng Services (dịch vụ)
CREATE TABLE [dbo].[Services] (
    [serviceId] [int] IDENTITY(1,1) NOT NULL,
    [categoryId] [int] NOT NULL,
    [serviceName] [nvarchar](255) NOT NULL,
    [description] [nvarchar](500) NULL,
    [image] [nvarchar](255) NULL,
    [price] [decimal](10, 2) NOT NULL,
    [duration] [int] NOT NULL,
    [isActive] [bit] DEFAULT 1,
    [createdDate] [datetime] DEFAULT GETDATE(),
    [updatedDate] [datetime] NULL,
    PRIMARY KEY CLUSTERED ([serviceId] ASC),
    CONSTRAINT [FK_Services_Categories] FOREIGN KEY ([categoryId]) REFERENCES [dbo].[Categories] ([categoryId]) ON DELETE CASCADE
);
GO

-- Tạo bảng ServiceDetails (dịch vụ)
CREATE TABLE [dbo].[ServiceDetails] (
    [serviceDetailId] [int] IDENTITY(1,1) NOT NULL,
    [serviceId] [int] NOT NULL,
    [OptionName] [nvarchar](255) NOT NULL,
    [OptionType] [nvarchar](50) NOT NULL,
    [BasePrice] [decimal](10, 2) NOT NULL,
    [Unit] [nvarchar](50) NOT NULL,
    [Duration] [int] NOT NULL,
    [Description] [nvarchar](1000) NULL,
    [IsActive] [bit] NOT NULL DEFAULT 1,
    PRIMARY KEY CLUSTERED ([serviceDetailId] ASC),
    CONSTRAINT [FK_ServiceDetail_Services] FOREIGN KEY ([serviceId]) REFERENCES [dbo].[Services] ([serviceId]) ON DELETE CASCADE
);
GO

-- Tạo bảng BookingStatuses (trạng thái đặt chỗ)
CREATE TABLE [dbo].[BookingStatuses] (
    [statusId] [int] IDENTITY(1,1) NOT NULL,
    [statusName] [nvarchar](50) NOT NULL UNIQUE,
    PRIMARY KEY CLUSTERED ([statusId] ASC)
);
GO

-- Tạo bảng PaymentStatuses (trạng thái thanh toán)
CREATE TABLE [dbo].[PaymentStatuses] (
    [statusId] [int] IDENTITY(1,1) NOT NULL,
    [statusName] [nvarchar](50) NOT NULL UNIQUE,
    PRIMARY KEY CLUSTERED ([statusId] ASC)
);
GO

-- Tạo bảng Promotions (khuyến mãi)
CREATE TABLE [dbo].[Promotions] (
    [code] [nvarchar](50) NOT NULL,
    [discountType] [nvarchar](20) NOT NULL CHECK ([discountType] IN ('AMOUNT', 'PERCENT')),
    [discountAmount] [decimal](10, 2) NULL,
    [discountPercent] [float] NULL,
    [startDate] [datetime] NOT NULL,
    [endDate] [datetime] NOT NULL,
    [minOrderAmount] [decimal](10, 2) NULL,
    [maxDiscountAmount] [decimal](10, 2) NULL,
    [createdDate] [datetime] DEFAULT GETDATE(),
    PRIMARY KEY CLUSTERED ([code] ASC)
);
GO

-- Tạo bảng Bookings (đặt chỗ)
CREATE TABLE [dbo].[Bookings] (
    [bookingId] [int] IDENTITY(1,1) NOT NULL,
    [bookingNumber] [nvarchar](50) NOT NULL UNIQUE,
    [customerId] [int] NOT NULL,
    [promotionCode] [nvarchar](50) NULL,
    [bookingDate] [datetime] DEFAULT GETDATE(),
    [totalAmount] [decimal](10, 2) NOT NULL,
    [notes] [nvarchar](500) NULL,
    [bookingStatusId] [int] NOT NULL,
    [paymentStatusId] [int] NOT NULL,
    PRIMARY KEY CLUSTERED ([bookingId] ASC),
    CONSTRAINT [FK_Bookings_Accounts] FOREIGN KEY ([customerId]) REFERENCES [dbo].[Accounts] ([accountId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Bookings_Promotions] FOREIGN KEY ([promotionCode]) REFERENCES [dbo].[Promotions] ([code]) ON DELETE SET NULL,
    CONSTRAINT [FK_Bookings_BookingStatuses] FOREIGN KEY ([bookingStatusId]) REFERENCES [dbo].[BookingStatuses] ([statusId]),
    CONSTRAINT [FK_Bookings_PaymentStatuses] FOREIGN KEY ([paymentStatusId]) REFERENCES [dbo].[PaymentStatuses] ([statusId])
);
GO

-- Tạo bảng BookingDetails (chi tiết đặt chỗ)
CREATE TABLE [dbo].[BookingDetails] (
    [detailId] [int] IDENTITY(1,1) NOT NULL,
    [bookingId] [int] NOT NULL,
    [serviceId] [int] NOT NULL,
    [scheduleDatetime] [datetime] NOT NULL,
    [quantity] [int] NOT NULL CHECK ([quantity] > 0),
    [unitPrice] [decimal](10, 2) NOT NULL,
    PRIMARY KEY CLUSTERED ([detailId] ASC),
    CONSTRAINT [FK_BookingDetails_Bookings] FOREIGN KEY ([bookingId]) REFERENCES [dbo].[Bookings] ([bookingId]) ON DELETE CASCADE,
    CONSTRAINT [FK_BookingDetails_Services] FOREIGN KEY ([serviceId]) REFERENCES [dbo].[Services] ([serviceId]) ON DELETE CASCADE
);
GO

-- Tạo bảng AssignmentStatuses (trạng thái phân công)
CREATE TABLE [dbo].[AssignmentStatuses] (
    [statusId] [int] IDENTITY(1,1) NOT NULL,
    [statusName] [nvarchar](50) NOT NULL UNIQUE,
    PRIMARY KEY CLUSTERED ([statusId] ASC)
);
GO

-- Tạo bảng ScheduleAssignments (phân công lịch)
CREATE TABLE [dbo].[ScheduleAssignments] (
    [assignmentId] [int] IDENTITY(1,1) NOT NULL,
    [housekeeperId] [int] NOT NULL,
    [detailId] [int] NOT NULL,
    [assignDate] [date] NOT NULL,
    [startTime] [time](7) NOT NULL,
    [endTime] [time](7) NOT NULL,
    [statusId] [int] NOT NULL,
    [notes] [nvarchar](500) NULL,
    PRIMARY KEY CLUSTERED ([assignmentId] ASC),
    CONSTRAINT [FK_ScheduleAssignments_Accounts] FOREIGN KEY ([housekeeperId]) REFERENCES [dbo].[Accounts] ([accountId]) ON DELETE CASCADE,
    CONSTRAINT [FK_ScheduleAssignments_BookingDetails] FOREIGN KEY ([detailId]) REFERENCES [dbo].[BookingDetails] ([detailId]),
    CONSTRAINT [FK_ScheduleAssignments_AssignmentStatuses] FOREIGN KEY ([statusId]) REFERENCES [dbo].[AssignmentStatuses] ([statusId])
);
GO

-- Tạo bảng Payments (thanh toán)
CREATE TABLE [dbo].[Payments] (
    [paymentId] [int] IDENTITY(1,1) NOT NULL,
    [bookingId] [int] NOT NULL,
    [amount] [decimal](10, 2) NOT NULL,
    [paymentMethod] [nvarchar](50) NOT NULL,
    [statusId] [int] NOT NULL,
    [paymentDate] [datetime] DEFAULT GETDATE(),
    [createdDate] [datetime] DEFAULT GETDATE(),
    [updatedDate] [datetime] NULL,
    [notes] [nvarchar](500) NULL,
    PRIMARY KEY CLUSTERED ([paymentId] ASC),
    CONSTRAINT [FK_Payments_Bookings] FOREIGN KEY ([bookingId]) REFERENCES [dbo].[Bookings] ([bookingId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Payments_PaymentStatuses] FOREIGN KEY ([statusId]) REFERENCES [dbo].[PaymentStatuses] ([statusId])
);
GO

-- Tạo bảng Reports (báo cáo công việc)
CREATE TABLE [dbo].[Reports] (
    [recordId] [int] IDENTITY(1,1) NOT NULL,
    [housekeeperId] [int] NOT NULL,
    [assignId] [int] NOT NULL,
    [workDate] [date] NOT NULL,
    [startTime] [time](7) NOT NULL,
    [endTime] [time](7) NOT NULL,
    [totalHours] [float] NOT NULL,
    [taskStatus] [nvarchar](50) NOT NULL,
    PRIMARY KEY CLUSTERED ([recordId] ASC),
    CONSTRAINT [FK_Reports_Accounts] FOREIGN KEY ([housekeeperId]) REFERENCES [dbo].[Accounts] ([accountId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Reports_ScheduleAssignments] FOREIGN KEY ([assignId]) REFERENCES [dbo].[ScheduleAssignments] ([assignmentId])
);
GO

-- Tạo bảng Reviews (đánh giá)
CREATE TABLE [dbo].[Reviews] (
    [reviewId] [int] IDENTITY(1,1) NOT NULL,
    [customerId] [int] NOT NULL,
    [detailId] [int] NOT NULL,
    [reviewTarget] [nvarchar](20) NOT NULL CHECK ([reviewTarget] IN ('SERVICE', 'HOUSEKEEPER')),
    [rating] [int] CHECK ([rating] >= 1 AND [rating] <= 5),
    [comment] [nvarchar](1000) NULL,
    [reviewDate] [datetime] DEFAULT GETDATE(),
    PRIMARY KEY CLUSTERED ([reviewId] ASC),
    CONSTRAINT [FK_Reviews_Accounts] FOREIGN KEY ([customerId]) REFERENCES [dbo].[Accounts] ([accountId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Reviews_BookingDetails] FOREIGN KEY ([detailId]) REFERENCES [dbo].[BookingDetails] ([detailId])
);
GO

-- Tạo bảng HousekeeperAvailability (lịch sẵn có của người giúp việc)
CREATE TABLE [dbo].[HousekeeperAvailability] (
    [availabilityId] [int] IDENTITY(1,1) NOT NULL,
    [housekeeperId] [int] NOT NULL,
    [availableDate] [date] NOT NULL,
    [startTime] [time](7) NOT NULL,
    [endTime] [time](7) NOT NULL,
    PRIMARY KEY CLUSTERED ([availabilityId] ASC),
    CONSTRAINT [FK_HousekeeperAvailability_Accounts] FOREIGN KEY ([housekeeperId]) REFERENCES [dbo].[Accounts] ([accountId]) ON DELETE CASCADE
);
GO

-- Thêm các giá trị mặc định cho các bảng trạng thái
INSERT INTO [dbo].[Roles] ([roleName]) VALUES ('ADMIN'), ('CUSTOMER'), ('STAFF'), ('HOUSEKEEPER');
INSERT INTO [dbo].[AccountStatuses] ([statusName]) VALUES ('ACTIVE'), ('INACTIVE');
INSERT INTO [dbo].[BookingStatuses] ([statusName]) VALUES ('PENDING'), ('CONFIRMED'), ('CANCELLED');
INSERT INTO [dbo].[PaymentStatuses] ([statusName]) VALUES ('UNPAID'), ('PAID'), ('REFUNDED');
INSERT INTO [dbo].[AssignmentStatuses] ([statusName]) VALUES ('ASSIGNED'), ('COMPLETED'), ('CANCELLED');
GO