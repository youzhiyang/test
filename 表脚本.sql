USE [master]
GO
/****** Object:  Database [Visitor]    Script Date: 2021/7/29 16:29:22 ******/
CREATE DATABASE [Visitor]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Visitor', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\Visitor.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Visitor_log', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\Visitor_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Visitor] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Visitor].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Visitor] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Visitor] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Visitor] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Visitor] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Visitor] SET ARITHABORT OFF 
GO
ALTER DATABASE [Visitor] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Visitor] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Visitor] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Visitor] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Visitor] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Visitor] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Visitor] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Visitor] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Visitor] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Visitor] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Visitor] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Visitor] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Visitor] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Visitor] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Visitor] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Visitor] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Visitor] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Visitor] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Visitor] SET  MULTI_USER 
GO
ALTER DATABASE [Visitor] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Visitor] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Visitor] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Visitor] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Visitor] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Visitor] SET QUERY_STORE = OFF
GO
USE [Visitor]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [Visitor]
GO
/****** Object:  Table [dbo].[Business_CertExtInfo]    Script Date: 2021/7/29 16:29:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Business_CertExtInfo](
	[CertID] [nchar](18) NOT NULL,
	[IdentityPic] [text] NULL,
	[PicFront] [text] NULL,
	[PicBack] [text] NULL,
 CONSTRAINT [PK_Business_CertExtInfo_1] PRIMARY KEY CLUSTERED 
(
	[CertID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Data_Dictionary]    Script Date: 2021/7/29 16:30:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_Dictionary](
	[DictID] [uniqueidentifier] NOT NULL,
	[DataType] [nvarchar](50) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Value] [nvarchar](100) NULL,
	[ExtValue1] [nvarchar](100) NULL,
	[ExtValue2] [nvarchar](100) NULL,
	[ExtValue3] [nvarchar](100) NULL,
	[Remark] [nvarchar](100) NULL,
	[Editable] [bit] NOT NULL,
	[Order] [int] NULL,
 CONSTRAINT [PK_Data_Dictionary] PRIMARY KEY CLUSTERED 
(
	[DictID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Business_CertInfo]    Script Date: 2021/7/29 16:31:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Business_CertInfo](
	[CertID] [nchar](18) NOT NULL,
	[CertTypeCode] [nvarchar](50) NOT NULL,
	[CertTypeName] [nvarchar](100) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Gender] [nchar](1) NOT NULL,
	[Nation] [nvarchar](10) NOT NULL,
	[BornDay] [date] NOT NULL,
	[CertAddress] [nvarchar](50) NOT NULL,
	[CertOrg] [nvarchar](50) NOT NULL,
	[EffDate] [date] NOT NULL,
	[ExpDate] [date] NOT NULL,
 CONSTRAINT [PK_Business_CertInfo] PRIMARY KEY CLUSTERED 
(
	[CertID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[View_Cert]    Script Date: 2021/7/29 16:32:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View_Cert]
AS
SELECT   bci.CertID, bci.CertTypeCode, dd.Name AS CertTypeName, bci.Name, bci.Gender, bci.Nation, bci.BornDay, 
                bci.CertAddress, bci.CertOrg, bci.EffDate, bci.ExpDate, bcei.IdentityPic, bcei.PicFront, bcei.PicBack
FROM      dbo.Business_CertInfo AS bci LEFT OUTER JOIN
                dbo.Business_CertExtInfo AS bcei ON bcei.CertID = bci.CertID LEFT OUTER JOIN
                dbo.Data_Dictionary AS dd ON dd.DataType = '证件类型' AND dd.Code = bci.CertTypeCode
GO
/****** Object:  Table [dbo].[Business_VisitorRecord]    Script Date: 2021/7/29 16:32:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Business_VisitorRecord](
	[VisitorRecordID] [int] IDENTITY(1,1) NOT NULL,
	[VisitRecordID] [int] NOT NULL,
	[CertID] [nchar](18) NOT NULL,
	[VisitOrgization] [nvarchar](50) NOT NULL,
	[EnterTime] [datetime] NULL,
	[LeaveTime] [datetime] NULL,
 CONSTRAINT [PK_Business_VisitorRecord] PRIMARY KEY CLUSTERED 
(
	[VisitorRecordID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Business_VisitorTimeline]    Script Date: 2021/7/29 16:34:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Business_VisitorTimeline](
	[VisitorTimelineID] [int] IDENTITY(1,1) NOT NULL,
	[RecordTime] [datetime] NOT NULL,
	[IsLeaveTime] [bit] NOT NULL,
 CONSTRAINT [PK_Business_VisitorTimeline] PRIMARY KEY CLUSTERED 
(
	[VisitorTimelineID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Business_VisitRecord]    Script Date: 2021/7/29 16:35:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Business_VisitRecord](
	[VisitRecordID] [int] IDENTITY(1,1) NOT NULL,
	[AuthCode] [nchar](10) NULL,
	[ReasonCode] [nvarchar](50) NOT NULL,
	[ReasonName] [nvarchar](100) NOT NULL,
	[Reason] [nvarchar](100) NOT NULL,
	[AreaID] [int] NOT NULL,
	[DeviceNTools] [nvarchar](100) NOT NULL,
	[RegisterTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Business_VisitRecord] PRIMARY KEY CLUSTERED 
(
	[VisitRecordID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Data_Area]    Script Date: 2021/7/29 16:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_Area](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Index] [int] NOT NULL,
 CONSTRAINT [PK_Data_Area] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Business_VisitorTimeline] ADD  CONSTRAINT [DF_Business_VisitorTimeline_IsLeaveTime]  DEFAULT ((0)) FOR [IsLeaveTime]
GO
ALTER TABLE [dbo].[Business_VisitRecord] ADD  CONSTRAINT [DF_Business_VisitRecord_RegisterTime]  DEFAULT (getdate()) FOR [RegisterTime]
GO
ALTER TABLE [dbo].[Data_Dictionary] ADD  CONSTRAINT [DF_Data_Dictionary_DictID]  DEFAULT (newid()) FOR [DictID]
GO
ALTER TABLE [dbo].[Data_Dictionary] ADD  CONSTRAINT [DF_Data_Dictionary_Editable]  DEFAULT ((1)) FOR [Editable]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'身份证件信息ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Business_CertExtInfo', @level2type=N'COLUMN',@level2name=N'CertID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'证件照片' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Business_CertExtInfo', @level2type=N'COLUMN',@level2name=N'IdentityPic'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'证件正面照片' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Business_CertExtInfo', @level2type=N'COLUMN',@level2name=N'PicFront'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'证件背面照片' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Business_CertExtInfo', @level2type=N'COLUMN',@level2name=N'PicBack'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务数据_身份证件扩展信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Business_CertExtInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'身份证件号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Business_CertInfo', @level2type=N'COLUMN',@level2name=N'CertID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'证件类型代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Business_CertInfo', @level2type=N'COLUMN',@level2name=N'CertTypeCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'证件类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Business_CertInfo', @level2type=N'COLUMN',@level2name=N'CertTypeName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Business_CertInfo', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'性别' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Business_CertInfo', @level2type=N'COLUMN',@level2name=N'Gender'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'民族' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Business_CertInfo', @level2type=N'COLUMN',@level2name=N'Nation'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'出生日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Business_CertInfo', @level2type=N'COLUMN',@level2name=N'BornDay'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'证件地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Business_CertInfo', @level2type=N'COLUMN',@level2name=N'CertAddress'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发证机关' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Business_CertInfo', @level2type=N'COLUMN',@level2name=N'CertOrg'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'起始有效期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Business_CertInfo', @level2type=N'COLUMN',@level2name=N'EffDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'截止有效期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Business_CertInfo', @level2type=N'COLUMN',@level2name=N'ExpDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务数据_身份证件信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Business_CertInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'访客记录ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Business_VisitorRecord', @level2type=N'COLUMN',@level2name=N'VisitorRecordID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'来访记录ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Business_VisitorRecord', @level2type=N'COLUMN',@level2name=N'VisitRecordID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'身份证件号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Business_VisitorRecord', @level2type=N'COLUMN',@level2name=N'CertID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'来访组织' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Business_VisitorRecord', @level2type=N'COLUMN',@level2name=N'VisitOrgization'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'进入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Business_VisitorRecord', @level2type=N'COLUMN',@level2name=N'EnterTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'离开时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Business_VisitorRecord', @level2type=N'COLUMN',@level2name=N'LeaveTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务数据_访客记录' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Business_VisitorRecord'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'记录时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Business_VisitorTimeline', @level2type=N'COLUMN',@level2name=N'RecordTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否离场时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Business_VisitorTimeline', @level2type=N'COLUMN',@level2name=N'IsLeaveTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务数据_访客时间线' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Business_VisitorTimeline'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'来访记录ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Business_VisitRecord', @level2type=N'COLUMN',@level2name=N'VisitRecordID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'来访授权码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Business_VisitRecord', @level2type=N'COLUMN',@level2name=N'AuthCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'来访事由代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Business_VisitRecord', @level2type=N'COLUMN',@level2name=N'ReasonCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'来访事由' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Business_VisitRecord', @level2type=N'COLUMN',@level2name=N'ReasonName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'自定义来访事由' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Business_VisitRecord', @level2type=N'COLUMN',@level2name=N'Reason'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'访问区域ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Business_VisitRecord', @level2type=N'COLUMN',@level2name=N'AreaID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'携带设备与工具' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Business_VisitRecord', @level2type=N'COLUMN',@level2name=N'DeviceNTools'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登记时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Business_VisitRecord', @level2type=N'COLUMN',@level2name=N'RegisterTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务数据_来访记录' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Business_VisitRecord'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'区域ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Data_Area', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'区域类别' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Data_Area', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'区域名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Data_Area', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Data_Area', @level2type=N'COLUMN',@level2name=N'Index'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'基础数据_区域' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Data_Area'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'字典ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Data_Dictionary', @level2type=N'COLUMN',@level2name=N'DictID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'字典类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Data_Dictionary', @level2type=N'COLUMN',@level2name=N'DataType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'字典代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Data_Dictionary', @level2type=N'COLUMN',@level2name=N'Code'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'字典名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Data_Dictionary', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'字典值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Data_Dictionary', @level2type=N'COLUMN',@level2name=N'Value'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'扩展值1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Data_Dictionary', @level2type=N'COLUMN',@level2name=N'ExtValue1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'扩展值2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Data_Dictionary', @level2type=N'COLUMN',@level2name=N'ExtValue2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'扩展值3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Data_Dictionary', @level2type=N'COLUMN',@level2name=N'ExtValue3'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'字典备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Data_Dictionary', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否可以编辑' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Data_Dictionary', @level2type=N'COLUMN',@level2name=N'Editable'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Data_Dictionary', @level2type=N'COLUMN',@level2name=N'Order'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'基础数据_数据字典' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Data_Dictionary'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'视图_身份证件' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_Cert'
GO
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
         Begin Table = "bci"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 146
               Right = 199
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "bcei"
            Begin Extent = 
               Top = 6
               Left = 237
               Bottom = 146
               Right = 385
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "dd"
            Begin Extent = 
               Top = 6
               Left = 423
               Bottom = 146
               Right = 568
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_Cert'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_Cert'
GO
USE [master]
GO
ALTER DATABASE [Visitor] SET  READ_WRITE 
GO
