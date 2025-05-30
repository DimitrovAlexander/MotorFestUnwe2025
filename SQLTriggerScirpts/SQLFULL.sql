USE [master]
GO
/****** Object:  Database [MotorFest]    Script Date: 30.5.2025 г. 10:16:57 ******/
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'MotorFest')
BEGIN
CREATE DATABASE [MotorFest]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MotorFest', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\MotorFest.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MotorFest_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\MotorFest_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
END
GO
ALTER DATABASE [MotorFest] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MotorFest].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MotorFest] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MotorFest] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MotorFest] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MotorFest] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MotorFest] SET ARITHABORT OFF 
GO
ALTER DATABASE [MotorFest] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [MotorFest] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MotorFest] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MotorFest] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MotorFest] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MotorFest] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MotorFest] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MotorFest] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MotorFest] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MotorFest] SET  ENABLE_BROKER 
GO
ALTER DATABASE [MotorFest] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MotorFest] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MotorFest] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MotorFest] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MotorFest] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MotorFest] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [MotorFest] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MotorFest] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [MotorFest] SET  MULTI_USER 
GO
ALTER DATABASE [MotorFest] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MotorFest] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MotorFest] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MotorFest] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MotorFest] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MotorFest] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [MotorFest] SET QUERY_STORE = ON
GO
ALTER DATABASE [MotorFest] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [MotorFest]
GO
/****** Object:  Schema [21180022]    Script Date: 30.5.2025 г. 10:16:57 ******/
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'21180022')
EXEC sys.sp_executesql N'CREATE SCHEMA [21180022]'
GO
/****** Object:  Table [21180022].[AspNetUsers]    Script Date: 30.5.2025 г. 10:16:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[21180022].[AspNetUsers]') AND type in (N'U'))
BEGIN
CREATE TABLE [21180022].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[Firstname] [nvarchar](max) NOT NULL,
	[Lastname] [nvarchar](max) NOT NULL,
	[Identifier] [nvarchar](max) NOT NULL,
	[21180022_LastUpdate] [datetime2](7) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [21180022].[Locations]    Script Date: 30.5.2025 г. 10:16:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[21180022].[Locations]') AND type in (N'U'))
BEGIN
CREATE TABLE [21180022].[Locations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[City] [nvarchar](max) NOT NULL,
	[FullAddress] [nvarchar](max) NOT NULL,
	[Country] [nvarchar](max) NOT NULL,
	[21180022_LastUpdate] [datetime2](7) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Locations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [21180022].[Events]    Script Date: 30.5.2025 г. 10:16:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[21180022].[Events]') AND type in (N'U'))
BEGIN
CREATE TABLE [21180022].[Events](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[OrganizerId] [nvarchar](450) NOT NULL,
	[LocationId] [int] NOT NULL,
	[EventDate] [datetime2](7) NOT NULL,
	[EntranceFee] [decimal](18, 2) NOT NULL,
	[21180022_LastUpdate] [datetime2](7) NOT NULL,
	[MinYearOfManufacture] [int] NULL,
	[MaxYearOfManufacture] [int] NULL,
	[IsCanceled] [bit] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[EventLogo] [nvarchar](max) NOT NULL,
	[EventPhotos] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [21180022].[EventEngineTypes]    Script Date: 30.5.2025 г. 10:16:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[21180022].[EventEngineTypes]') AND type in (N'U'))
BEGIN
CREATE TABLE [21180022].[EventEngineTypes](
	[EventId] [int] NOT NULL,
	[EngineTypeId] [int] NOT NULL,
	[21180022_LastUpdate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_EventEngineTypes] PRIMARY KEY CLUSTERED 
(
	[EventId] ASC,
	[EngineTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [21180022].[EventVehicleCategories]    Script Date: 30.5.2025 г. 10:16:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[21180022].[EventVehicleCategories]') AND type in (N'U'))
BEGIN
CREATE TABLE [21180022].[EventVehicleCategories](
	[EventId] [int] NOT NULL,
	[VehicleCategoryId] [int] NOT NULL,
	[21180022_LastUpdate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_EventVehicleCategories] PRIMARY KEY CLUSTERED 
(
	[EventId] ASC,
	[VehicleCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [21180022].[EventRegistrations]    Script Date: 30.5.2025 г. 10:16:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[21180022].[EventRegistrations]') AND type in (N'U'))
BEGIN
CREATE TABLE [21180022].[EventRegistrations](
	[EventId] [int] NOT NULL,
	[VehicleId] [int] NOT NULL,
	[Id] [int] NOT NULL,
	[RegistrationDate] [datetime2](7) NOT NULL,
	[21180022_LastUpdate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_EventRegistrations] PRIMARY KEY CLUSTERED 
(
	[EventId] ASC,
	[VehicleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  View [21180022].[EventsView]    Script Date: 30.5.2025 г. 10:16:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[21180022].[EventsView]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [21180022].[EventsView]
AS
SELECT 
    e.Id AS EventId,
    e.OrganizerId,
    u.Firstname + '' '' + u.Lastname AS OrganizerFullName, 
    e.Name AS EventName,
    l.Name AS LocationName,
    e.EventDate,
    e.EntranceFee,

    (SELECT COUNT(*) 
     FROM [21180022].[EventRegistrations] er 
     WHERE er.EventId = e.Id) AS RegisteredParticipantsCount,
    
    (SELECT COUNT(*) 
     FROM [21180022].[EventRegistrations] er 
     WHERE er.EventId = e.Id) * e.EntranceFee AS ExpectedRevenue,

    (SELECT COUNT(*) 
     FROM [21180022].[EventEngineTypes] eet 
     WHERE eet.EventId = e.Id) AS AllowedEngineTypesCount,

    (SELECT COUNT(*) 
     FROM [21180022].[EventVehicleCategories] evc 
     WHERE evc.EventId = e.Id) AS AllowedVehicleCategoriesCount
FROM 
    [21180022].[Events] e
JOIN 
    [21180022].[Locations] l ON e.LocationId = l.Id
JOIN 
    [21180022].[AspNetUsers] u ON e.OrganizerId = u.Id; ' 
GO
/****** Object:  Table [21180022].[AspNetRoles]    Script Date: 30.5.2025 г. 10:16:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[21180022].[AspNetRoles]') AND type in (N'U'))
BEGIN
CREATE TABLE [21180022].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [21180022].[AspNetUserRoles]    Script Date: 30.5.2025 г. 10:16:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[21180022].[AspNetUserRoles]') AND type in (N'U'))
BEGIN
CREATE TABLE [21180022].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [21180022].[Vehicles]    Script Date: 30.5.2025 г. 10:16:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[21180022].[Vehicles]') AND type in (N'U'))
BEGIN
CREATE TABLE [21180022].[Vehicles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OwnerId] [nvarchar](450) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[EngineTypeId] [int] NOT NULL,
	[Manufacturer] [nvarchar](max) NOT NULL,
	[Model] [nvarchar](max) NOT NULL,
	[YearOfManufacture] [int] NOT NULL,
	[Photo] [nvarchar](max) NULL,
	[21180022_LastUpdate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Vehicles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  View [21180022].[UsersView]    Script Date: 30.5.2025 г. 10:16:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[21180022].[UsersView]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [21180022].[UsersView]
AS
SELECT u.Id AS UserId, u.Firstname + '' '' + u.Lastname AS FullName, r.Name AS RoleName, u.Email, CASE WHEN r.Name = ''Participant'' THEN
                      (SELECT COUNT(*)
                       FROM      [21180022].[Vehicles] v
                       WHERE   v.OwnerId = u.Id) ELSE 0 END AS VehicleCount, CASE WHEN r.Name = ''Participant'' OR
                  r.Name = ''Administrator'' THEN
                      (SELECT COUNT(DISTINCT er.EventId)
                       FROM      [21180022].[EventRegistrations] er JOIN
                                         [21180022].[Vehicles] v ON er.VehicleId = v.Id
                       WHERE   v.OwnerId = u.Id) ELSE 0 END AS ParticipatedEventsCount, CASE WHEN r.Name = ''Organizer'' OR
                  r.Name = ''Administrator'' THEN
                      (SELECT COUNT(*)
                       FROM      [21180022].[Events] e
                       WHERE   e.OrganizerId = u.Id) ELSE 0 END AS OrganizedEventsCount
FROM     [21180022].AspNetUsers AS u LEFT OUTER JOIN
                  [21180022].AspNetUserRoles AS ur ON u.Id = ur.UserId LEFT OUTER JOIN
                  [21180022].AspNetRoles AS r ON ur.RoleId = r.Id
' 
GO
/****** Object:  Table [21180022].[AspNetRoleClaims]    Script Date: 30.5.2025 г. 10:16:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[21180022].[AspNetRoleClaims]') AND type in (N'U'))
BEGIN
CREATE TABLE [21180022].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [21180022].[AspNetUserClaims]    Script Date: 30.5.2025 г. 10:16:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[21180022].[AspNetUserClaims]') AND type in (N'U'))
BEGIN
CREATE TABLE [21180022].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [21180022].[AspNetUserLogins]    Script Date: 30.5.2025 г. 10:16:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[21180022].[AspNetUserLogins]') AND type in (N'U'))
BEGIN
CREATE TABLE [21180022].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [21180022].[AspNetUserTokens]    Script Date: 30.5.2025 г. 10:16:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[21180022].[AspNetUserTokens]') AND type in (N'U'))
BEGIN
CREATE TABLE [21180022].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [21180022].[EngineTypes]    Script Date: 30.5.2025 г. 10:16:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[21180022].[EngineTypes]') AND type in (N'U'))
BEGIN
CREATE TABLE [21180022].[EngineTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[21180022_LastUpdate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_EngineTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [21180022].[log_21180022]    Script Date: 30.5.2025 г. 10:16:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[21180022].[log_21180022]') AND type in (N'U'))
BEGIN
CREATE TABLE [21180022].[log_21180022](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TableName] [nvarchar](max) NOT NULL,
	[OperationType] [nvarchar](max) NOT NULL,
	[OperationDateTime] [datetime2](7) NOT NULL,
	[21180022_LastUpdate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_log_21180022] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [21180022].[VehicleCategories]    Script Date: 30.5.2025 г. 10:16:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[21180022].[VehicleCategories]') AND type in (N'U'))
BEGIN
CREATE TABLE [21180022].[VehicleCategories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[21180022_LastUpdate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_VehicleCategories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 30.5.2025 г. 10:16:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[__EFMigrationsHistory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 30.5.2025 г. 10:16:57 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[21180022].[AspNetRoleClaims]') AND name = N'IX_AspNetRoleClaims_RoleId')
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [21180022].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 30.5.2025 г. 10:16:57 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[21180022].[AspNetRoles]') AND name = N'RoleNameIndex')
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [21180022].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 30.5.2025 г. 10:16:57 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[21180022].[AspNetUserClaims]') AND name = N'IX_AspNetUserClaims_UserId')
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [21180022].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 30.5.2025 г. 10:16:57 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[21180022].[AspNetUserLogins]') AND name = N'IX_AspNetUserLogins_UserId')
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [21180022].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 30.5.2025 г. 10:16:57 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[21180022].[AspNetUserRoles]') AND name = N'IX_AspNetUserRoles_RoleId')
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [21180022].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 30.5.2025 г. 10:16:57 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[21180022].[AspNetUsers]') AND name = N'EmailIndex')
CREATE NONCLUSTERED INDEX [EmailIndex] ON [21180022].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 30.5.2025 г. 10:16:57 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[21180022].[AspNetUsers]') AND name = N'UserNameIndex')
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [21180022].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_EventEngineTypes_EngineTypeId]    Script Date: 30.5.2025 г. 10:16:57 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[21180022].[EventEngineTypes]') AND name = N'IX_EventEngineTypes_EngineTypeId')
CREATE NONCLUSTERED INDEX [IX_EventEngineTypes_EngineTypeId] ON [21180022].[EventEngineTypes]
(
	[EngineTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_EventRegistrations_VehicleId]    Script Date: 30.5.2025 г. 10:16:57 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[21180022].[EventRegistrations]') AND name = N'IX_EventRegistrations_VehicleId')
CREATE NONCLUSTERED INDEX [IX_EventRegistrations_VehicleId] ON [21180022].[EventRegistrations]
(
	[VehicleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Events_LocationId]    Script Date: 30.5.2025 г. 10:16:57 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[21180022].[Events]') AND name = N'IX_Events_LocationId')
CREATE NONCLUSTERED INDEX [IX_Events_LocationId] ON [21180022].[Events]
(
	[LocationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Events_OrganizerId]    Script Date: 30.5.2025 г. 10:16:57 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[21180022].[Events]') AND name = N'IX_Events_OrganizerId')
CREATE NONCLUSTERED INDEX [IX_Events_OrganizerId] ON [21180022].[Events]
(
	[OrganizerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_EventVehicleCategories_VehicleCategoryId]    Script Date: 30.5.2025 г. 10:16:57 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[21180022].[EventVehicleCategories]') AND name = N'IX_EventVehicleCategories_VehicleCategoryId')
CREATE NONCLUSTERED INDEX [IX_EventVehicleCategories_VehicleCategoryId] ON [21180022].[EventVehicleCategories]
(
	[VehicleCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Vehicles_CategoryId]    Script Date: 30.5.2025 г. 10:16:57 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[21180022].[Vehicles]') AND name = N'IX_Vehicles_CategoryId')
CREATE NONCLUSTERED INDEX [IX_Vehicles_CategoryId] ON [21180022].[Vehicles]
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Vehicles_EngineTypeId]    Script Date: 30.5.2025 г. 10:16:57 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[21180022].[Vehicles]') AND name = N'IX_Vehicles_EngineTypeId')
CREATE NONCLUSTERED INDEX [IX_Vehicles_EngineTypeId] ON [21180022].[Vehicles]
(
	[EngineTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Vehicles_OwnerId]    Script Date: 30.5.2025 г. 10:16:57 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[21180022].[Vehicles]') AND name = N'IX_Vehicles_OwnerId')
CREATE NONCLUSTERED INDEX [IX_Vehicles_OwnerId] ON [21180022].[Vehicles]
(
	[OwnerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[21180022].[DF__EventEngi__21180__318258D2]') AND type = 'D')
BEGIN
ALTER TABLE [21180022].[EventEngineTypes] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [21180022_LastUpdate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[21180022].[DF__EventRegi__21180__308E3499]') AND type = 'D')
BEGIN
ALTER TABLE [21180022].[EventRegistrations] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [21180022_LastUpdate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[21180022].[DF__Events__IsCancel__19AACF41]') AND type = 'D')
BEGIN
ALTER TABLE [21180022].[Events] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsCanceled]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[21180022].[DF__Events__Descript__2DB1C7EE]') AND type = 'D')
BEGIN
ALTER TABLE [21180022].[Events] ADD  DEFAULT (N'') FOR [Description]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[21180022].[DF__Events__EventLog__2EA5EC27]') AND type = 'D')
BEGIN
ALTER TABLE [21180022].[Events] ADD  DEFAULT (N'') FOR [EventLogo]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[21180022].[DF__Events__EventPho__2F9A1060]') AND type = 'D')
BEGIN
ALTER TABLE [21180022].[Events] ADD  DEFAULT (N'[]') FOR [EventPhotos]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[21180022].[DF__EventVehi__21180__2CBDA3B5]') AND type = 'D')
BEGIN
ALTER TABLE [21180022].[EventVehicleCategories] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [21180022_LastUpdate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[21180022].[DF__Locations__IsDel__3FD07829]') AND type = 'D')
BEGIN
ALTER TABLE [21180022].[Locations] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDeleted]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[21180022].[FK_AspNetRoleClaims_AspNetRoles_RoleId]') AND parent_object_id = OBJECT_ID(N'[21180022].[AspNetRoleClaims]'))
ALTER TABLE [21180022].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [21180022].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[21180022].[FK_AspNetRoleClaims_AspNetRoles_RoleId]') AND parent_object_id = OBJECT_ID(N'[21180022].[AspNetRoleClaims]'))
ALTER TABLE [21180022].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[21180022].[FK_AspNetUserClaims_AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[21180022].[AspNetUserClaims]'))
ALTER TABLE [21180022].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [21180022].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[21180022].[FK_AspNetUserClaims_AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[21180022].[AspNetUserClaims]'))
ALTER TABLE [21180022].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[21180022].[FK_AspNetUserLogins_AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[21180022].[AspNetUserLogins]'))
ALTER TABLE [21180022].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [21180022].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[21180022].[FK_AspNetUserLogins_AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[21180022].[AspNetUserLogins]'))
ALTER TABLE [21180022].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[21180022].[FK_AspNetUserRoles_AspNetRoles_RoleId]') AND parent_object_id = OBJECT_ID(N'[21180022].[AspNetUserRoles]'))
ALTER TABLE [21180022].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [21180022].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[21180022].[FK_AspNetUserRoles_AspNetRoles_RoleId]') AND parent_object_id = OBJECT_ID(N'[21180022].[AspNetUserRoles]'))
ALTER TABLE [21180022].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[21180022].[FK_AspNetUserRoles_AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[21180022].[AspNetUserRoles]'))
ALTER TABLE [21180022].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [21180022].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[21180022].[FK_AspNetUserRoles_AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[21180022].[AspNetUserRoles]'))
ALTER TABLE [21180022].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[21180022].[FK_AspNetUserTokens_AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[21180022].[AspNetUserTokens]'))
ALTER TABLE [21180022].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [21180022].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[21180022].[FK_AspNetUserTokens_AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[21180022].[AspNetUserTokens]'))
ALTER TABLE [21180022].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[21180022].[FK_EventEngineTypes_EngineTypes_EngineTypeId]') AND parent_object_id = OBJECT_ID(N'[21180022].[EventEngineTypes]'))
ALTER TABLE [21180022].[EventEngineTypes]  WITH CHECK ADD  CONSTRAINT [FK_EventEngineTypes_EngineTypes_EngineTypeId] FOREIGN KEY([EngineTypeId])
REFERENCES [21180022].[EngineTypes] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[21180022].[FK_EventEngineTypes_EngineTypes_EngineTypeId]') AND parent_object_id = OBJECT_ID(N'[21180022].[EventEngineTypes]'))
ALTER TABLE [21180022].[EventEngineTypes] CHECK CONSTRAINT [FK_EventEngineTypes_EngineTypes_EngineTypeId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[21180022].[FK_EventEngineTypes_Events_EventId]') AND parent_object_id = OBJECT_ID(N'[21180022].[EventEngineTypes]'))
ALTER TABLE [21180022].[EventEngineTypes]  WITH CHECK ADD  CONSTRAINT [FK_EventEngineTypes_Events_EventId] FOREIGN KEY([EventId])
REFERENCES [21180022].[Events] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[21180022].[FK_EventEngineTypes_Events_EventId]') AND parent_object_id = OBJECT_ID(N'[21180022].[EventEngineTypes]'))
ALTER TABLE [21180022].[EventEngineTypes] CHECK CONSTRAINT [FK_EventEngineTypes_Events_EventId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[21180022].[FK_EventRegistrations_Events_EventId]') AND parent_object_id = OBJECT_ID(N'[21180022].[EventRegistrations]'))
ALTER TABLE [21180022].[EventRegistrations]  WITH CHECK ADD  CONSTRAINT [FK_EventRegistrations_Events_EventId] FOREIGN KEY([EventId])
REFERENCES [21180022].[Events] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[21180022].[FK_EventRegistrations_Events_EventId]') AND parent_object_id = OBJECT_ID(N'[21180022].[EventRegistrations]'))
ALTER TABLE [21180022].[EventRegistrations] CHECK CONSTRAINT [FK_EventRegistrations_Events_EventId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[21180022].[FK_EventRegistrations_Vehicles_VehicleId]') AND parent_object_id = OBJECT_ID(N'[21180022].[EventRegistrations]'))
ALTER TABLE [21180022].[EventRegistrations]  WITH CHECK ADD  CONSTRAINT [FK_EventRegistrations_Vehicles_VehicleId] FOREIGN KEY([VehicleId])
REFERENCES [21180022].[Vehicles] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[21180022].[FK_EventRegistrations_Vehicles_VehicleId]') AND parent_object_id = OBJECT_ID(N'[21180022].[EventRegistrations]'))
ALTER TABLE [21180022].[EventRegistrations] CHECK CONSTRAINT [FK_EventRegistrations_Vehicles_VehicleId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[21180022].[FK_Events_AspNetUsers_OrganizerId]') AND parent_object_id = OBJECT_ID(N'[21180022].[Events]'))
ALTER TABLE [21180022].[Events]  WITH CHECK ADD  CONSTRAINT [FK_Events_AspNetUsers_OrganizerId] FOREIGN KEY([OrganizerId])
REFERENCES [21180022].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[21180022].[FK_Events_AspNetUsers_OrganizerId]') AND parent_object_id = OBJECT_ID(N'[21180022].[Events]'))
ALTER TABLE [21180022].[Events] CHECK CONSTRAINT [FK_Events_AspNetUsers_OrganizerId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[21180022].[FK_Events_Locations_LocationId]') AND parent_object_id = OBJECT_ID(N'[21180022].[Events]'))
ALTER TABLE [21180022].[Events]  WITH CHECK ADD  CONSTRAINT [FK_Events_Locations_LocationId] FOREIGN KEY([LocationId])
REFERENCES [21180022].[Locations] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[21180022].[FK_Events_Locations_LocationId]') AND parent_object_id = OBJECT_ID(N'[21180022].[Events]'))
ALTER TABLE [21180022].[Events] CHECK CONSTRAINT [FK_Events_Locations_LocationId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[21180022].[FK_EventVehicleCategories_Events_EventId]') AND parent_object_id = OBJECT_ID(N'[21180022].[EventVehicleCategories]'))
ALTER TABLE [21180022].[EventVehicleCategories]  WITH CHECK ADD  CONSTRAINT [FK_EventVehicleCategories_Events_EventId] FOREIGN KEY([EventId])
REFERENCES [21180022].[Events] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[21180022].[FK_EventVehicleCategories_Events_EventId]') AND parent_object_id = OBJECT_ID(N'[21180022].[EventVehicleCategories]'))
ALTER TABLE [21180022].[EventVehicleCategories] CHECK CONSTRAINT [FK_EventVehicleCategories_Events_EventId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[21180022].[FK_EventVehicleCategories_VehicleCategories_VehicleCategoryId]') AND parent_object_id = OBJECT_ID(N'[21180022].[EventVehicleCategories]'))
ALTER TABLE [21180022].[EventVehicleCategories]  WITH CHECK ADD  CONSTRAINT [FK_EventVehicleCategories_VehicleCategories_VehicleCategoryId] FOREIGN KEY([VehicleCategoryId])
REFERENCES [21180022].[VehicleCategories] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[21180022].[FK_EventVehicleCategories_VehicleCategories_VehicleCategoryId]') AND parent_object_id = OBJECT_ID(N'[21180022].[EventVehicleCategories]'))
ALTER TABLE [21180022].[EventVehicleCategories] CHECK CONSTRAINT [FK_EventVehicleCategories_VehicleCategories_VehicleCategoryId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[21180022].[FK_Vehicles_AspNetUsers_OwnerId]') AND parent_object_id = OBJECT_ID(N'[21180022].[Vehicles]'))
ALTER TABLE [21180022].[Vehicles]  WITH CHECK ADD  CONSTRAINT [FK_Vehicles_AspNetUsers_OwnerId] FOREIGN KEY([OwnerId])
REFERENCES [21180022].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[21180022].[FK_Vehicles_AspNetUsers_OwnerId]') AND parent_object_id = OBJECT_ID(N'[21180022].[Vehicles]'))
ALTER TABLE [21180022].[Vehicles] CHECK CONSTRAINT [FK_Vehicles_AspNetUsers_OwnerId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[21180022].[FK_Vehicles_EngineTypes_EngineTypeId]') AND parent_object_id = OBJECT_ID(N'[21180022].[Vehicles]'))
ALTER TABLE [21180022].[Vehicles]  WITH CHECK ADD  CONSTRAINT [FK_Vehicles_EngineTypes_EngineTypeId] FOREIGN KEY([EngineTypeId])
REFERENCES [21180022].[EngineTypes] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[21180022].[FK_Vehicles_EngineTypes_EngineTypeId]') AND parent_object_id = OBJECT_ID(N'[21180022].[Vehicles]'))
ALTER TABLE [21180022].[Vehicles] CHECK CONSTRAINT [FK_Vehicles_EngineTypes_EngineTypeId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[21180022].[FK_Vehicles_VehicleCategories_CategoryId]') AND parent_object_id = OBJECT_ID(N'[21180022].[Vehicles]'))
ALTER TABLE [21180022].[Vehicles]  WITH CHECK ADD  CONSTRAINT [FK_Vehicles_VehicleCategories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [21180022].[VehicleCategories] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[21180022].[FK_Vehicles_VehicleCategories_CategoryId]') AND parent_object_id = OBJECT_ID(N'[21180022].[Vehicles]'))
ALTER TABLE [21180022].[Vehicles] CHECK CONSTRAINT [FK_Vehicles_VehicleCategories_CategoryId]
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_DiagramPane1' , N'SCHEMA',N'21180022', N'VIEW',N'UsersView', NULL,NULL))
	EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "u"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 308
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ur"
            Begin Extent = 
               Top = 175
               Left = 48
               Bottom = 294
               Right = 242
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "r"
            Begin Extent = 
               Top = 294
               Left = 48
               Bottom = 457
               Right = 267
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'21180022', @level1type=N'VIEW',@level1name=N'UsersView'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_DiagramPaneCount' , N'SCHEMA',N'21180022', N'VIEW',N'UsersView', NULL,NULL))
	EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'21180022', @level1type=N'VIEW',@level1name=N'UsersView'
GO
USE [master]
GO
ALTER DATABASE [MotorFest] SET  READ_WRITE 
GO
USE MotorFest;
GO
DECLARE @TableName NVARCHAR(255);
DECLARE @SchemaName NVARCHAR(255) = '21180022';
DECLARE @TriggerSQL NVARCHAR(MAX);
DECLARE @ErrorMsg NVARCHAR(MAX);
DECLARE @TableCount INT = 0;

-- Проверка дали схемата съществува
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = @SchemaName)
BEGIN
    PRINT 'ERROR: Schema [' + @SchemaName + '] does not exist!';
    RETURN;
END


SELECT @TableCount = COUNT(*)
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_SCHEMA = @SchemaName AND TABLE_TYPE = 'BASE TABLE';

PRINT 'Tables found in schema [' + @SchemaName + ']: ' + CAST(@TableCount AS NVARCHAR(10));


IF @TableCount = 0
BEGIN
    PRINT 'ERROR: No tables found in schema [' + @SchemaName + ']';
    RETURN;
END


DECLARE TableCursor CURSOR FOR
SELECT TABLE_NAME
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_SCHEMA = @SchemaName AND TABLE_TYPE = 'BASE TABLE';

OPEN TableCursor;
FETCH NEXT FROM TableCursor INTO @TableName;

PRINT 'Starting table processing...';

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT '----------------------------------------';
    PRINT 'Processing table: [' + @SchemaName + '].[' + @TableName + ']';
    
    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = @SchemaName AND TABLE_NAME = 'log_21180022')
    BEGIN
        PRINT 'Creating logging table [' + @SchemaName + '].[log_21180022]...';
        BEGIN TRY
            EXEC('
            CREATE TABLE [' + @SchemaName + '].[log_21180022] (
                LogID INT IDENTITY(1,1) PRIMARY KEY,
                TableName NVARCHAR(255),
                OperationType NVARCHAR(50),
                OperationDateTime DATETIME,
                [21180022_LastUpdate] DATETIME
            )');
            PRINT 'Logging table created successfully.';
        END TRY
        BEGIN CATCH
            SET @ErrorMsg = ERROR_MESSAGE();
            PRINT 'ERROR creating logging table: ' + @ErrorMsg;
            CLOSE TableCursor;
            DEALLOCATE TableCursor;
            RETURN;
        END CATCH
    END

   
    PRINT 'Creating INSERT trigger for [' + @TableName + ']...';
    BEGIN TRY
       
        IF EXISTS (SELECT * FROM sys.triggers WHERE name = 'trg_' + @TableName + '_Insert' AND parent_id = OBJECT_ID('[' + @SchemaName + '].[' + @TableName + ']'))
        BEGIN
            PRINT 'Dropping existing INSERT trigger...';
            EXEC('DROP TRIGGER [' + @SchemaName + '].[trg_' + @TableName + '_Insert]');
        END
        
       
        SET @TriggerSQL = '
        CREATE TRIGGER [' + @SchemaName + '].[trg_' + @TableName + '_Insert]
        ON [' + @SchemaName + '].[' + @TableName + ']
        AFTER INSERT
        AS
        BEGIN
            SET NOCOUNT ON;
            INSERT INTO [' + @SchemaName + '].[log_21180022] (TableName, OperationType, OperationDateTime, [21180022_LastUpdate])
            SELECT ''' + @TableName + ''', ''INSERT'', GETUTCDATE(), GETUTCDATE();
        END;';
        
        EXEC sp_executesql @TriggerSQL;
        PRINT 'INSERT trigger created successfully.';
    END TRY
    BEGIN CATCH
        SET @ErrorMsg = ERROR_MESSAGE();
        PRINT 'ERROR creating INSERT trigger: ' + @ErrorMsg;
    END CATCH
    
 
    PRINT 'Creating UPDATE trigger for [' + @TableName + ']...';
    BEGIN TRY
        
        IF EXISTS (SELECT * FROM sys.triggers WHERE name = 'trg_' + @TableName + '_Update' AND parent_id = OBJECT_ID('[' + @SchemaName + '].[' + @TableName + ']'))
        BEGIN
            PRINT 'Dropping existing UPDATE trigger...';
            EXEC('DROP TRIGGER [' + @SchemaName + '].[trg_' + @TableName + '_Update]');
        END
        
        
        SET @TriggerSQL = '
        CREATE TRIGGER [' + @SchemaName + '].[trg_' + @TableName + '_Update]
        ON [' + @SchemaName + '].[' + @TableName + ']
        AFTER UPDATE
        AS
        BEGIN
            SET NOCOUNT ON;
            INSERT INTO [' + @SchemaName + '].[log_21180022] (TableName, OperationType, OperationDateTime, [21180022_LastUpdate])
            SELECT ''' + @TableName + ''', ''UPDATE'', GETUTCDATE(), GETUTCDATE();
        END;';
        
        EXEC sp_executesql @TriggerSQL;
        PRINT 'UPDATE trigger created successfully.';
    END TRY
    BEGIN CATCH
        SET @ErrorMsg = ERROR_MESSAGE();
        PRINT 'ERROR creating UPDATE trigger: ' + @ErrorMsg;
    END CATCH
    
    
    PRINT 'Creating DELETE trigger for [' + @TableName + ']...';
    BEGIN TRY
        
        IF EXISTS (SELECT * FROM sys.triggers WHERE name = 'trg_' + @TableName + '_Delete' AND parent_id = OBJECT_ID('[' + @SchemaName + '].[' + @TableName + ']'))
        BEGIN
            PRINT 'Dropping existing DELETE trigger...';
            EXEC('DROP TRIGGER [' + @SchemaName + '].[trg_' + @TableName + '_Delete]');
        END
        
        -- Създаване на нов тригер
        SET @TriggerSQL = '
        CREATE TRIGGER [' + @SchemaName + '].[trg_' + @TableName + '_Delete]
        ON [' + @SchemaName + '].[' + @TableName + ']
        AFTER DELETE
        AS
        BEGIN
            SET NOCOUNT ON;
            INSERT INTO [' + @SchemaName + '].[log_21180022] (TableName, OperationType, OperationDateTime, [21180022_LastUpdate])
            SELECT ''' + @TableName + ''', ''DELETE'', GETUTCDATE(), GETUTCDATE();
        END;';
        
        EXEC sp_executesql @TriggerSQL;
        PRINT 'DELETE trigger created successfully.';
    END TRY
    BEGIN CATCH
        SET @ErrorMsg = ERROR_MESSAGE();
        PRINT 'ERROR creating DELETE trigger: ' + @ErrorMsg;
    END CATCH
    
    
    PRINT 'Verifying triggers for [' + @TableName + ']:';
    DECLARE @TriggerCount INT = 0;
    SELECT @TriggerCount = COUNT(*) 
    FROM sys.triggers 
    WHERE parent_id = OBJECT_ID('[' + @SchemaName + '].[' + @TableName + ']') 
      AND name LIKE 'trg_' + @TableName + '_%';
    
    PRINT 'Found ' + CAST(@TriggerCount AS NVARCHAR(10)) + ' triggers for this table.';
    
    FETCH NEXT FROM TableCursor INTO @TableName;
END;

CLOSE TableCursor;
DEALLOCATE TableCursor;


PRINT '----------------------------------------';
PRINT 'Final verification of all triggers in schema [' + @SchemaName + ']:';
DECLARE @TotalTriggers INT = 0;
SELECT @TotalTriggers = COUNT(*) 
FROM sys.triggers t
JOIN sys.objects o ON t.parent_id = o.object_id
JOIN sys.schemas s ON o.schema_id = s.schema_id
WHERE s.name = @SchemaName;

PRINT 'Total triggers found in schema: ' + CAST(@TotalTriggers AS NVARCHAR(10));

IF @TotalTriggers > 0
BEGIN
    PRINT 'Operation completed successfully.';
END
ELSE
BEGIN
    PRINT 'WARNING: No triggers were created!';
END
