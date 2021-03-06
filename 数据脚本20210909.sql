USE [Visitor]
GO
/****** Object:  Table [dbo].[Business_CertExtInfo]    Script Date: 2021/9/9 9:53:03 ******/
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
/****** Object:  Table [dbo].[Data_Dictionary]    Script Date: 2021/9/9 9:53:03 ******/
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
/****** Object:  Table [dbo].[Business_CertInfo]    Script Date: 2021/9/9 9:53:03 ******/
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
/****** Object:  View [dbo].[View_Cert]    Script Date: 2021/9/9 9:53:03 ******/
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
/****** Object:  Table [dbo].[Business_VisitorRecord]    Script Date: 2021/9/9 9:53:03 ******/
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
/****** Object:  Table [dbo].[Business_VisitorTimeline]    Script Date: 2021/9/9 9:53:04 ******/
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
/****** Object:  Table [dbo].[Business_VisitRecord]    Script Date: 2021/9/9 9:53:04 ******/
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
	[Areas] [nvarchar](100) NOT NULL,
	[DeviceNTools] [nvarchar](100) NOT NULL,
	[RegisterTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Business_VisitRecord] PRIMARY KEY CLUSTERED 
(
	[VisitRecordID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Data_Area]    Script Date: 2021/9/9 9:53:04 ******/
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
SET IDENTITY_INSERT [dbo].[Data_Area] ON 

INSERT [dbo].[Data_Area] ([ID], [Type], [Name], [Index]) VALUES (1, N'1层', N'1#配电间', 0)
INSERT [dbo].[Data_Area] ([ID], [Type], [Name], [Index]) VALUES (2, N'1层', N'
2#配电间
', 0)
INSERT [dbo].[Data_Area] ([ID], [Type], [Name], [Index]) VALUES (3, N'2层', N'3#配电间
', 0)
INSERT [dbo].[Data_Area] ([ID], [Type], [Name], [Index]) VALUES (4, N'2层', N'4#配电间
', 0)
INSERT [dbo].[Data_Area] ([ID], [Type], [Name], [Index]) VALUES (5, N'3层', N'5#配电间
', 0)
INSERT [dbo].[Data_Area] ([ID], [Type], [Name], [Index]) VALUES (6, N'3层', N'6#配电间
', 0)
INSERT [dbo].[Data_Area] ([ID], [Type], [Name], [Index]) VALUES (7, N'4层', N'7#配电间
', 0)
INSERT [dbo].[Data_Area] ([ID], [Type], [Name], [Index]) VALUES (8, N'4层', N'8#配电间
', 0)
INSERT [dbo].[Data_Area] ([ID], [Type], [Name], [Index]) VALUES (9, N'2层', N'1#电池间
', 0)
INSERT [dbo].[Data_Area] ([ID], [Type], [Name], [Index]) VALUES (10, N'2层', N'2#电池间
', 0)
INSERT [dbo].[Data_Area] ([ID], [Type], [Name], [Index]) VALUES (11, N'3层', N'3#电池间
', 0)
INSERT [dbo].[Data_Area] ([ID], [Type], [Name], [Index]) VALUES (12, N'3层', N'4#电池间
', 0)
INSERT [dbo].[Data_Area] ([ID], [Type], [Name], [Index]) VALUES (13, N'4层', N'5#电池间
', 0)
INSERT [dbo].[Data_Area] ([ID], [Type], [Name], [Index]) VALUES (14, N'4层', N'6#电池间
', 0)
INSERT [dbo].[Data_Area] ([ID], [Type], [Name], [Index]) VALUES (15, N'1层', N'1#风机房
', 0)
INSERT [dbo].[Data_Area] ([ID], [Type], [Name], [Index]) VALUES (16, N'2层', N'2#风机房
', 0)
INSERT [dbo].[Data_Area] ([ID], [Type], [Name], [Index]) VALUES (17, N'2层', N'3#风机房
', 0)
INSERT [dbo].[Data_Area] ([ID], [Type], [Name], [Index]) VALUES (18, N'3层', N'4#风机房
', 0)
INSERT [dbo].[Data_Area] ([ID], [Type], [Name], [Index]) VALUES (19, N'3层', N'5#风机房
', 0)
INSERT [dbo].[Data_Area] ([ID], [Type], [Name], [Index]) VALUES (20, N'4层', N'6#风机房
', 0)
INSERT [dbo].[Data_Area] ([ID], [Type], [Name], [Index]) VALUES (21, N'4层', N'7#风机房
', 0)
INSERT [dbo].[Data_Area] ([ID], [Type], [Name], [Index]) VALUES (22, N'1层', N'ECC', 0)
INSERT [dbo].[Data_Area] ([ID], [Type], [Name], [Index]) VALUES (23, N'1层', N'展厅', 0)
INSERT [dbo].[Data_Area] ([ID], [Type], [Name], [Index]) VALUES (24, N'', N'地下室', 0)
INSERT [dbo].[Data_Area] ([ID], [Type], [Name], [Index]) VALUES (25, N'1层', N'消控室', 0)
INSERT [dbo].[Data_Area] ([ID], [Type], [Name], [Index]) VALUES (26, N'1层', N'冷机房', 0)
INSERT [dbo].[Data_Area] ([ID], [Type], [Name], [Index]) VALUES (27, N'1层', N'1F弱电间', 0)
INSERT [dbo].[Data_Area] ([ID], [Type], [Name], [Index]) VALUES (28, N'2层', N'2F弱电间', 0)
INSERT [dbo].[Data_Area] ([ID], [Type], [Name], [Index]) VALUES (29, N'3层', N'3F弱电间', 0)
INSERT [dbo].[Data_Area] ([ID], [Type], [Name], [Index]) VALUES (30, N'4层', N'4F弱电间', 0)
INSERT [dbo].[Data_Area] ([ID], [Type], [Name], [Index]) VALUES (31, N'1层', N'1F接入间', 0)
INSERT [dbo].[Data_Area] ([ID], [Type], [Name], [Index]) VALUES (32, N'2层', N'2F接入间', 0)
INSERT [dbo].[Data_Area] ([ID], [Type], [Name], [Index]) VALUES (33, N'3层', N'3F接入间', 0)
INSERT [dbo].[Data_Area] ([ID], [Type], [Name], [Index]) VALUES (34, N'4层', N'4F接入间', 0)
INSERT [dbo].[Data_Area] ([ID], [Type], [Name], [Index]) VALUES (35, N'2层', N'2F管道间', 0)
INSERT [dbo].[Data_Area] ([ID], [Type], [Name], [Index]) VALUES (36, N'3层', N'3F管道间', 0)
INSERT [dbo].[Data_Area] ([ID], [Type], [Name], [Index]) VALUES (37, N'4层', N'4F管道间', 0)
INSERT [dbo].[Data_Area] ([ID], [Type], [Name], [Index]) VALUES (38, N'屋顶', N'客梯机房', 0)
INSERT [dbo].[Data_Area] ([ID], [Type], [Name], [Index]) VALUES (40, N'屋顶', N'货梯机房', 0)
INSERT [dbo].[Data_Area] ([ID], [Type], [Name], [Index]) VALUES (41, N'屋顶', N'水箱间', 0)
INSERT [dbo].[Data_Area] ([ID], [Type], [Name], [Index]) VALUES (42, N'屋顶', N'配电间', 0)
INSERT [dbo].[Data_Area] ([ID], [Type], [Name], [Index]) VALUES (43, N'屋顶', N'冷却塔', 0)
INSERT [dbo].[Data_Area] ([ID], [Type], [Name], [Index]) VALUES (44, N'1层', N'会议室', 0)
SET IDENTITY_INSERT [dbo].[Data_Area] OFF
INSERT [dbo].[Data_Dictionary] ([DictID], [DataType], [Code], [Name], [Value], [ExtValue1], [ExtValue2], [ExtValue3], [Remark], [Editable], [Order]) VALUES (N'5976150a-3b7b-42a0-9eed-1d6d21fc4b37', N'来访事由', N'DeviceRepair', N'设备维修', NULL, NULL, NULL, NULL, NULL, 1, 3)
INSERT [dbo].[Data_Dictionary] ([DictID], [DataType], [Code], [Name], [Value], [ExtValue1], [ExtValue2], [ExtValue3], [Remark], [Editable], [Order]) VALUES (N'33f50658-a3da-4181-ba6d-3ef5ae639b50', N'来访事由', N'Meeting', N'会议', NULL, NULL, NULL, NULL, NULL, 1, 1)
INSERT [dbo].[Data_Dictionary] ([DictID], [DataType], [Code], [Name], [Value], [ExtValue1], [ExtValue2], [ExtValue3], [Remark], [Editable], [Order]) VALUES (N'eff4d02c-cc93-4433-9275-70d40a0e6c87', N'来访事由', N'DeviceSetting', N'设备调试', NULL, NULL, NULL, NULL, NULL, 1, 4)
INSERT [dbo].[Data_Dictionary] ([DictID], [DataType], [Code], [Name], [Value], [ExtValue1], [ExtValue2], [ExtValue3], [Remark], [Editable], [Order]) VALUES (N'fc7da6aa-62d9-40f5-b0ca-a2d344022f17', N'来访事由', N'Visit', N'参观', NULL, NULL, NULL, NULL, NULL, 1, 2)
INSERT [dbo].[Data_Dictionary] ([DictID], [DataType], [Code], [Name], [Value], [ExtValue1], [ExtValue2], [ExtValue3], [Remark], [Editable], [Order]) VALUES (N'6da29565-b646-4572-923a-be1710aa0f4f', N'来访事由', N'DeviceSetup', N'设备安装', NULL, NULL, NULL, NULL, NULL, 1, 5)
INSERT [dbo].[Data_Dictionary] ([DictID], [DataType], [Code], [Name], [Value], [ExtValue1], [ExtValue2], [ExtValue3], [Remark], [Editable], [Order]) VALUES (N'84adc006-3864-4457-8e28-c1c63c3df5d3', N'证件类型', N'IdentityCard', N'身份证', NULL, NULL, NULL, NULL, NULL, 0, NULL)
INSERT [dbo].[Data_Dictionary] ([DictID], [DataType], [Code], [Name], [Value], [ExtValue1], [ExtValue2], [ExtValue3], [Remark], [Editable], [Order]) VALUES (N'3fd8a33c-6312-4475-9136-f4638878f6c7', N'来访事由', N'Other', N'其他', NULL, NULL, NULL, NULL, NULL, 1, 6)
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'访问区域' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Business_VisitRecord', @level2type=N'COLUMN',@level2name=N'Areas'
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
