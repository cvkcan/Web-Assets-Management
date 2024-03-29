USE [master]
GO
/****** Object:  Database [AssetsManagement]    Script Date: 11.06.2023 14:15:31 ******/
CREATE DATABASE [AssetsManagement]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'AssetsManagement', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\AssetsManagement.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'AssetsManagement_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\AssetsManagement_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [AssetsManagement] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AssetsManagement].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AssetsManagement] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AssetsManagement] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AssetsManagement] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AssetsManagement] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AssetsManagement] SET ARITHABORT OFF 
GO
ALTER DATABASE [AssetsManagement] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [AssetsManagement] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AssetsManagement] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AssetsManagement] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AssetsManagement] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AssetsManagement] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AssetsManagement] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AssetsManagement] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AssetsManagement] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AssetsManagement] SET  DISABLE_BROKER 
GO
ALTER DATABASE [AssetsManagement] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AssetsManagement] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AssetsManagement] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AssetsManagement] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AssetsManagement] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AssetsManagement] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [AssetsManagement] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AssetsManagement] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [AssetsManagement] SET  MULTI_USER 
GO
ALTER DATABASE [AssetsManagement] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AssetsManagement] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AssetsManagement] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AssetsManagement] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [AssetsManagement] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [AssetsManagement] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [AssetsManagement] SET QUERY_STORE = ON
GO
ALTER DATABASE [AssetsManagement] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [AssetsManagement]
GO
/****** Object:  Table [dbo].[Assets]    Script Date: 11.06.2023 14:15:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Assets](
	[AssetsNumber] [int] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Price] [int] NULL,
	[CategorieName] [nvarchar](50) NULL,
	[Vendor] [nvarchar](50) NULL,
 CONSTRAINT [PK__Assets__672653048E51BE3F] PRIMARY KEY CLUSTERED 
(
	[AssetsNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 11.06.2023 14:15:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategorieId] [int] IDENTITY(1,1) NOT NULL,
	[CategorieName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK__Categori__F643ADA67A44632C] PRIMARY KEY CLUSTERED 
(
	[CategorieId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reports]    Script Date: 11.06.2023 14:15:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reports](
	[Date] [datetime] NULL,
	[EId] [int] NULL,
	[Auth] [nvarchar](25) NULL,
	[Information] [nvarchar](max) NULL,
	[Url] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 11.06.2023 14:15:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[EId] [int] NOT NULL,
	[Password] [nvarchar](50) NULL,
	[Auth] [nvarchar](6) NULL,
PRIMARY KEY CLUSTERED 
(
	[EId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vendors]    Script Date: 11.06.2023 14:15:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vendors](
	[VendorId] [int] IDENTITY(1,1) NOT NULL,
	[VendorName] [nvarchar](25) NULL,
 CONSTRAINT [PK_Vendors] PRIMARY KEY CLUSTERED 
(
	[VendorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Assets]  WITH CHECK ADD  CONSTRAINT [CHK_Price] CHECK  (([Price]>(0)))
GO
ALTER TABLE [dbo].[Assets] CHECK CONSTRAINT [CHK_Price]
GO
USE [master]
GO
ALTER DATABASE [AssetsManagement] SET  READ_WRITE 
GO
