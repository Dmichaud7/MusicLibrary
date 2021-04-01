IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BandMember]') AND type in (N'U'))
ALTER TABLE [dbo].[BandMember] DROP CONSTRAINT IF EXISTS [FK_BandMember_Member]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BandMember]') AND type in (N'U'))
ALTER TABLE [dbo].[BandMember] DROP CONSTRAINT IF EXISTS [FK_BandMember_Band]
GO
/****** Object:  Table [dbo].[Member]    Script Date: 2020-06-01 11:29:50 AM ******/
DROP TABLE IF EXISTS [dbo].[Member]
GO
/****** Object:  Table [dbo].[Login]    Script Date: 2020-06-01 11:29:50 AM ******/
DROP TABLE IF EXISTS [dbo].[Login]
GO
/****** Object:  Table [dbo].[BandMember]    Script Date: 2020-06-01 11:29:50 AM ******/
DROP TABLE IF EXISTS [dbo].[BandMember]
GO
/****** Object:  Table [dbo].[Band]    Script Date: 2020-06-01 11:29:50 AM ******/
DROP TABLE IF EXISTS [dbo].[Band]
GO
/****** Object:  Database [MusicLibrary]    Script Date: 2020-06-01 11:29:50 AM ******/
DROP DATABASE IF EXISTS [MusicLibrary]
GO
/****** Object:  Database [MusicLibrary]    Script Date: 2020-06-01 11:29:50 AM ******/
CREATE DATABASE [MusicLibrary]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MusicLibrary', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\MusicLibrary.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MusicLibrary_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\MusicLibrary_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [MusicLibrary] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MusicLibrary].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MusicLibrary] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MusicLibrary] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MusicLibrary] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MusicLibrary] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MusicLibrary] SET ARITHABORT OFF 
GO
ALTER DATABASE [MusicLibrary] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MusicLibrary] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MusicLibrary] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MusicLibrary] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MusicLibrary] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MusicLibrary] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MusicLibrary] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MusicLibrary] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MusicLibrary] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MusicLibrary] SET  ENABLE_BROKER 
GO
ALTER DATABASE [MusicLibrary] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MusicLibrary] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MusicLibrary] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MusicLibrary] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MusicLibrary] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MusicLibrary] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MusicLibrary] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MusicLibrary] SET RECOVERY FULL 
GO
ALTER DATABASE [MusicLibrary] SET  MULTI_USER 
GO
ALTER DATABASE [MusicLibrary] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MusicLibrary] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MusicLibrary] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MusicLibrary] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MusicLibrary] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'MusicLibrary', N'ON'
GO
ALTER DATABASE [MusicLibrary] SET QUERY_STORE = OFF
GO
/****** Object:  Table [dbo].[Band]    Script Date: 2020-06-01 11:29:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
GO
USE [MusicLibrary]
GO
CREATE TABLE [dbo].[Band](
	[BandID] [int] IDENTITY(1,1) NOT NULL,
	[BandName] [nvarchar](50) NOT NULL,
	[Genre] [nvarchar](50) NOT NULL,
	[FoundedDate] [datetime] NOT NULL,
	[Description] [nvarchar](50) NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Band] PRIMARY KEY CLUSTERED 
(
	[BandID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BandMember]    Script Date: 2020-06-01 11:29:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BandMember](
	[BandID] [int] NOT NULL,
	[MemberID] [int] NOT NULL,
 CONSTRAINT [PK_BandMember] PRIMARY KEY CLUSTERED 
(
	[BandID] ASC,
	[MemberID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Login]    Script Date: 2020-06-01 11:29:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Login](
	[LoginID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Login] PRIMARY KEY CLUSTERED 
(
	[LoginID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Member]    Script Date: 2020-06-01 11:29:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Member](
	[MemberID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[MiddleName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[JoinDate] [datetime] NOT NULL,
	[Instrument] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Member] PRIMARY KEY CLUSTERED 
(
	[MemberID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Band] ON 
GO
INSERT [dbo].[Band] ([BandID], [BandName], [Genre], [FoundedDate], [Description], [Active]) VALUES (1, N'Ghost', N'Metal', CAST(N'2006-01-01T00:00:00.000' AS DateTime), NULL, 1)
GO
INSERT [dbo].[Band] ([BandID], [BandName], [Genre], [FoundedDate], [Description], [Active]) VALUES (2, N'Sabaton', N'Metal', CAST(N'1999-01-01T00:00:00.000' AS DateTime), NULL, 1)
GO
INSERT [dbo].[Band] ([BandID], [BandName], [Genre], [FoundedDate], [Description], [Active]) VALUES (3, N'Journey', N'Rock', CAST(N'1973-01-01T00:00:00.000' AS DateTime), NULL, 1)
GO
INSERT [dbo].[Band] ([BandID], [BandName], [Genre], [FoundedDate], [Description], [Active]) VALUES (4, N'ABBA', N'POP', CAST(N'1972-01-01T00:00:00.000' AS DateTime), NULL, 1)
GO
INSERT [dbo].[Band] ([BandID], [BandName], [Genre], [FoundedDate], [Description], [Active]) VALUES (5, N'Rammstein', N'Metal', CAST(N'1994-01-01T00:00:00.000' AS DateTime), NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[Band] OFF
GO
INSERT [dbo].[BandMember] ([BandID], [MemberID]) VALUES (1, 1)
GO
INSERT [dbo].[BandMember] ([BandID], [MemberID]) VALUES (2, 2)
GO
INSERT [dbo].[BandMember] ([BandID], [MemberID]) VALUES (2, 3)
GO
INSERT [dbo].[BandMember] ([BandID], [MemberID]) VALUES (2, 4)
GO
INSERT [dbo].[BandMember] ([BandID], [MemberID]) VALUES (2, 5)
GO
INSERT [dbo].[BandMember] ([BandID], [MemberID]) VALUES (2, 6)
GO
INSERT [dbo].[BandMember] ([BandID], [MemberID]) VALUES (3, 7)
GO
INSERT [dbo].[BandMember] ([BandID], [MemberID]) VALUES (3, 8)
GO
INSERT [dbo].[BandMember] ([BandID], [MemberID]) VALUES (3, 9)
GO
INSERT [dbo].[BandMember] ([BandID], [MemberID]) VALUES (3, 10)
GO
INSERT [dbo].[BandMember] ([BandID], [MemberID]) VALUES (3, 11)
GO
INSERT [dbo].[BandMember] ([BandID], [MemberID]) VALUES (3, 12)
GO
INSERT [dbo].[BandMember] ([BandID], [MemberID]) VALUES (4, 13)
GO
INSERT [dbo].[BandMember] ([BandID], [MemberID]) VALUES (4, 14)
GO
INSERT [dbo].[BandMember] ([BandID], [MemberID]) VALUES (4, 15)
GO
INSERT [dbo].[BandMember] ([BandID], [MemberID]) VALUES (4, 16)
GO
INSERT [dbo].[BandMember] ([BandID], [MemberID]) VALUES (5, 17)
GO
INSERT [dbo].[BandMember] ([BandID], [MemberID]) VALUES (5, 18)
GO
INSERT [dbo].[BandMember] ([BandID], [MemberID]) VALUES (5, 19)
GO
INSERT [dbo].[BandMember] ([BandID], [MemberID]) VALUES (5, 20)
GO
INSERT [dbo].[BandMember] ([BandID], [MemberID]) VALUES (5, 21)
GO
INSERT [dbo].[BandMember] ([BandID], [MemberID]) VALUES (5, 22)
GO
SET IDENTITY_INSERT [dbo].[Login] ON 
GO
INSERT [dbo].[Login] ([LoginID], [Username], [Password]) VALUES (1, N'Denis', N'12345')
GO
INSERT [dbo].[Login] ([LoginID], [Username], [Password]) VALUES (2, N'Stephen', N'12345')
GO
INSERT [dbo].[Login] ([LoginID], [Username], [Password]) VALUES (3, N'Bob', N'12345')
GO
SET IDENTITY_INSERT [dbo].[Login] OFF
GO
SET IDENTITY_INSERT [dbo].[Member] ON 
GO
INSERT [dbo].[Member] ([MemberID], [FirstName], [MiddleName], [LastName], [JoinDate], [Instrument]) VALUES (1, N'Tobias', NULL, N'Forge', CAST(N'2008-01-01T00:00:00.000' AS DateTime), N'Vocals')
GO
INSERT [dbo].[Member] ([MemberID], [FirstName], [MiddleName], [LastName], [JoinDate], [Instrument]) VALUES (2, N'Joakim', NULL, N'Broden', CAST(N'1999-01-01T00:00:00.000' AS DateTime), N'Vocals')
GO
INSERT [dbo].[Member] ([MemberID], [FirstName], [MiddleName], [LastName], [JoinDate], [Instrument]) VALUES (3, N'Par', NULL, N'Sundstrom', CAST(N'1990-01-01T00:00:00.000' AS DateTime), N'Bass')
GO
INSERT [dbo].[Member] ([MemberID], [FirstName], [MiddleName], [LastName], [JoinDate], [Instrument]) VALUES (4, N'Chris', NULL, N'Roland', CAST(N'2012-01-01T00:00:00.000' AS DateTime), N'Guitar')
GO
INSERT [dbo].[Member] ([MemberID], [FirstName], [MiddleName], [LastName], [JoinDate], [Instrument]) VALUES (5, N'Hannes', N'van', N'Dashl', CAST(N'2014-01-01T00:00:00.000' AS DateTime), N'Drums')
GO
INSERT [dbo].[Member] ([MemberID], [FirstName], [MiddleName], [LastName], [JoinDate], [Instrument]) VALUES (6, N'Tommy', NULL, N'Joshansson', CAST(N'2016-01-01T00:00:00.000' AS DateTime), N'Guitar')
GO
INSERT [dbo].[Member] ([MemberID], [FirstName], [MiddleName], [LastName], [JoinDate], [Instrument]) VALUES (7, N'Neal', NULL, N'Schon', CAST(N'1973-01-01T00:00:00.000' AS DateTime), N'Guitar')
GO
INSERT [dbo].[Member] ([MemberID], [FirstName], [MiddleName], [LastName], [JoinDate], [Instrument]) VALUES (8, N'Jonathan', NULL, N'Cain', CAST(N'1980-01-01T00:00:00.000' AS DateTime), N'Keyboard')
GO
INSERT [dbo].[Member] ([MemberID], [FirstName], [MiddleName], [LastName], [JoinDate], [Instrument]) VALUES (9, N'Randy', NULL, N'Jackson', CAST(N'1985-01-01T00:00:00.000' AS DateTime), N'Bass')
GO
INSERT [dbo].[Member] ([MemberID], [FirstName], [MiddleName], [LastName], [JoinDate], [Instrument]) VALUES (10, N'Arnel', NULL, N'Pineda', CAST(N'2007-01-01T00:00:00.000' AS DateTime), N'Vocals')
GO
INSERT [dbo].[Member] ([MemberID], [FirstName], [MiddleName], [LastName], [JoinDate], [Instrument]) VALUES (11, N'Jason', NULL, N'Derlatka', CAST(N'1973-01-01T00:00:00.000' AS DateTime), N'Keyboard')
GO
INSERT [dbo].[Member] ([MemberID], [FirstName], [MiddleName], [LastName], [JoinDate], [Instrument]) VALUES (12, N'Narada', N'Micheal', N'Walden', CAST(N'2020-01-01T00:00:00.000' AS DateTime), N'Drums')
GO
INSERT [dbo].[Member] ([MemberID], [FirstName], [MiddleName], [LastName], [JoinDate], [Instrument]) VALUES (13, N'Agnethat', NULL, N'Falskog', CAST(N'1972-01-01T00:00:00.000' AS DateTime), N'Vocals')
GO
INSERT [dbo].[Member] ([MemberID], [FirstName], [MiddleName], [LastName], [JoinDate], [Instrument]) VALUES (14, N'Anni', N'Frid', N'Lyngstad', CAST(N'1972-01-01T00:00:00.000' AS DateTime), N'Vocals')
GO
INSERT [dbo].[Member] ([MemberID], [FirstName], [MiddleName], [LastName], [JoinDate], [Instrument]) VALUES (15, N'Bjom', NULL, N'Ulvaeus', CAST(N'1972-01-01T00:00:00.000' AS DateTime), N'Guitar')
GO
INSERT [dbo].[Member] ([MemberID], [FirstName], [MiddleName], [LastName], [JoinDate], [Instrument]) VALUES (16, N'Benny', NULL, N'Andersson', CAST(N'1972-01-01T00:00:00.000' AS DateTime), N'Keyboard')
GO
INSERT [dbo].[Member] ([MemberID], [FirstName], [MiddleName], [LastName], [JoinDate], [Instrument]) VALUES (17, N'Till', NULL, N'Lindemann', CAST(N'1994-01-01T00:00:00.000' AS DateTime), N'Vocals')
GO
INSERT [dbo].[Member] ([MemberID], [FirstName], [MiddleName], [LastName], [JoinDate], [Instrument]) VALUES (18, N'Richard', NULL, N'Kruspe', CAST(N'1994-01-01T00:00:00.000' AS DateTime), N'Guitar')
GO
INSERT [dbo].[Member] ([MemberID], [FirstName], [MiddleName], [LastName], [JoinDate], [Instrument]) VALUES (19, N'Paul', NULL, N'Kruspe', CAST(N'1994-01-01T00:00:00.000' AS DateTime), N'Guitar')
GO
INSERT [dbo].[Member] ([MemberID], [FirstName], [MiddleName], [LastName], [JoinDate], [Instrument]) VALUES (20, N'Oliver', NULL, N'Riedel', CAST(N'1994-01-01T00:00:00.000' AS DateTime), N'Bass')
GO
INSERT [dbo].[Member] ([MemberID], [FirstName], [MiddleName], [LastName], [JoinDate], [Instrument]) VALUES (21, N'Christoph', NULL, N'Schneider', CAST(N'1994-01-01T00:00:00.000' AS DateTime), N'Drums')
GO
INSERT [dbo].[Member] ([MemberID], [FirstName], [MiddleName], [LastName], [JoinDate], [Instrument]) VALUES (22, N'Chritian', N'Flake', N'Lorenz', CAST(N'1972-01-01T00:00:00.000' AS DateTime), N'Keyboard')
GO
SET IDENTITY_INSERT [dbo].[Member] OFF
GO
ALTER TABLE [dbo].[BandMember]  WITH CHECK ADD  CONSTRAINT [FK_BandMember_Band] FOREIGN KEY([BandID])
REFERENCES [dbo].[Band] ([BandID])
GO
ALTER TABLE [dbo].[BandMember] CHECK CONSTRAINT [FK_BandMember_Band]
GO
ALTER TABLE [dbo].[BandMember]  WITH CHECK ADD  CONSTRAINT [FK_BandMember_Member] FOREIGN KEY([MemberID])
REFERENCES [dbo].[Member] ([MemberID])
GO
ALTER TABLE [dbo].[BandMember] CHECK CONSTRAINT [FK_BandMember_Member]
GO
ALTER DATABASE [MusicLibrary] SET  READ_WRITE 
GO

USE master
GO