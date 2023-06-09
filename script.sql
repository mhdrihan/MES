USE [MESAPP]
GO
/****** Object:  UserDefinedFunction [dbo].[Fcn_Check_Result_Station]    Script Date: 5/23/2023 8:48:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[Fcn_Check_Result_Station]
(
	@serialNumber varchar(200),
		@stationNumber varchar(200)
)
RETURNS @ret TABLE(
  [Serial Number] varchar(50) ,
  [Batch ID] varchar(50) ,
  [Refference Name] varchar(50) ,
  [Work Order] varchar(50),
  [Station ID] int ,
  [Station Suffix] int  ,
  [Station Name] varchar(50) ,
  [User ID] varchar(50)  ,
  [Cavity Number] varchar(50) ,
  [Time In] datetime,
  [Time Out] datetime ,
  [Status Result] varchar(50),
  [Status Running] varchar(50),
  [Transact By] varchar(50),
  [FullRefference] varchar(50)
  
)
AS
BEGIN
	INSERT into @ret
		select TOP 1 * from Table_Run_Traceability where [Serial Number] = @serialNumber and [Station ID] = @stationNumber order by [Time In] desc
	 RETURN
END
GO
/****** Object:  UserDefinedFunction [dbo].[FCN_Read_Master_Counter]    Script Date: 5/23/2023 8:48:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[FCN_Read_Master_Counter]
(
	@stationID  Integer,
@stationSuffix  Integer	,
@yearCode  Integer,
@weekCode  Integer	
)
RETURNS @ret TABLE(
  [Station ID] int ,
  [Station Suffix] int ,
  [Year Code] int ,
  [Week Code] int ,
  [Counter Code] int  
)
AS
BEGIN
	INSERT into @ret
		select * from Table_Master_Serial_Counter where [Station ID] = @stationID and [Station Suffix] = @stationSuffix and [Year Code] = @yearCode and [Week Code] = @weekCode
	 RETURN
END
GO
/****** Object:  UserDefinedFunction [dbo].[Fcn_Read_Master_Order]    Script Date: 5/23/2023 8:48:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[Fcn_Read_Master_Order]
(
	@statusOrder varchar(100),
	@statusOrder2 varchar(100),
	@workOrder varchar(100)
)
RETURNS @Tabel TABLE(
   [Work Order] varchar(50),
  [Refference Name] varchar(50),
  [Work Plan] varchar(50) ,
  [Qty Order] int ,
  [Qty Launching] int,
  [Status Order] varchar(50) ,
  [Date Order] datetime ,
  [Date Complete] datetime ,
  [Transact By] varchar(50) ,
  [User Name] varchar(50),
  [WO Comment] varchar(300),
  [Priority WO] int ,
  [Station ID] int,
  [Station Suffix] int 
)
AS
BEGIN
	IF( @workOrder = '') BEGIN
			INSERT INTO @Tabel select * from Table_Master_Order where [Status Order] = @statusOrder or [Status Order] = @statusOrder2
		END ELSE BEGIN
			INSERT INTO @Tabel select * from Table_Master_Order where [Work Order] = @workOrder
	END RETURN 
END
GO
/****** Object:  UserDefinedFunction [dbo].[Fcn_Read_Master_Order1]    Script Date: 5/23/2023 8:48:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[Fcn_Read_Master_Order1]
(
	@statusOrder varchar(100),
	@statusOrder2 varchar(100),
	@workOrder varchar(100)
)
RETURNS @Tabel TABLE(
   [Work Order] varchar(50),
  [Refference Name] varchar(50),
  [Work Plan] varchar(50) ,
  [Qty Order] int ,
  [Qty Launching] int,
  [Status Order] varchar(50) ,
  [Date Order] datetime ,
  [Date Complete] datetime ,
  [Transact By] varchar(50) ,
  [User Name] varchar(50),
  [WO Comment] varchar(300),
  [Priority WO] int ,
  [Station ID] int,
  [Station Suffix] int 
)
AS
BEGIN
	IF( @workOrder = '') BEGIN
			INSERT INTO @Tabel select * from Table_Master_Order where [Status Order] <>'COMPLETE'
		END ELSE BEGIN
			INSERT INTO @Tabel select * from Table_Master_Order where [Work Order] = @workOrder
	END RETURN 
END
GO
/****** Object:  UserDefinedFunction [dbo].[FCN_Read_Master_Refference]    Script Date: 5/23/2023 8:48:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[FCN_Read_Master_Refference]
(
	@reffName  VARCHAR(200) 
)
RETURNS @ret TABLE(
    [Refference ID] int,
  [Refference Name] varchar(50),
  [Refference Description] varchar(50) ,
  [Last Modify] datetime ,
  [Transact By] varchar(50)
)
AS
BEGIN
	INSERT into @ret
		select * from Table_Master_Refference where [Refference Name] = @reffName
	 RETURN
END
GO
/****** Object:  UserDefinedFunction [dbo].[FCN_Read_Master_Station]    Script Date: 5/23/2023 8:48:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[FCN_Read_Master_Station]
(
	@stationID  Integer,
@stationSuffix  Integer	
)
RETURNS @ret TABLE(
   [Station ID] int ,
  [Station Suffix] int ,
  [Station Name] varchar(50) ,
  [Process ID] int ,
  [Line ID] int ,
  [Target Output] int ,
  [Target Yield] int ,
  [Last Modify] datetime ,
  [Transact By] varchar(50) ,
  [Tester Name] varchar(50) 
)
AS
BEGIN
	INSERT into @ret
		select * from Table_Master_Station where [Station ID] = @stationID and [Station Suffix] = @stationSuffix
	 RETURN
END
GO
/****** Object:  UserDefinedFunction [dbo].[FCN_Read_Master_User]    Script Date: 5/23/2023 8:48:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[FCN_Read_Master_User]
(
	@stationID  Integer,
	@idUser nvarchar(max)
)
RETURNS @ret TABLE(
  [ID User] varchar(50),
  [User Name] varchar(50) ,
  [User Password] varchar(50),
  [Station ID] int ,
  [User Level] int 
)
AS
BEGIN
	INSERT into @ret
			select * from Table_Master_User where [Station ID]=@stationID and [ID User]=@idUser
	 RETURN
END
GO
/****** Object:  UserDefinedFunction [dbo].[FCN_Read_Master_Workplan]    Script Date: 5/23/2023 8:48:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[FCN_Read_Master_Workplan]
(
	@flowID  Integer 
)
RETURNS @ret TABLE(
   [Flow ID] int ,
  [Flow Name] varchar(50)  ,
  [Refference Name] varchar(50)  ,
  [Line ID] int ,
  [Valid Status] varchar(50)  ,
  [Revision Number] int  ,
  [Last Modify] datetime ,
  [Transact By] varchar(50)  ,
  [Flow Description] varchar(300)  ,
  [Process Qty] int ,
  [Process 1] int  ,
  [Process 2] int  ,
  [Process 3] int  ,
  [Process 4] int  ,
  [Process 5] int  ,
  [Process 6] int  ,
  [Process 7] int  ,
  [Process 8] int  ,
  [Process 9] int  ,
  [Process 10] int  ,
  [Process 11] int  ,
  [Process 12] int  ,
  [Process 13] int  ,
  [Process 14] int  ,
  [Process 15] int  ,
  [Process 16] int  ,
  [Process 17] int  ,
  [Process 18] int  ,
  [Process 19] int  ,
  [Process 20] int  ,
  [Process 21] int  ,
  [Process 22] int  ,
  [Process 23] int  ,
  [Process 24] int  ,
  [Process 25] int  ,
  [Process 26] int  ,
  [Process 27] int  ,
  [Process 28] int  ,
  [Process 29] int  ,
  [Process 30] int  ,
  [Process 31] int  ,
  [Process 32] int  ,
  [Process 33] int  ,
  [Process 34] int  ,
  [Process 35] int  ,
  [Process 36] int  ,
  [Process 37] int  ,
  [Process 38] int  ,
  [Process 39] int  ,
  [Process 40] int  ,
  [Process 41] int  ,
  [Process 42] int  ,
  [Process 43] int  ,
  [Process 44] int  ,
  [Process 45] int  ,
  [Process 46] int  ,
  [Process 47] int  ,
  [Process 48] int  ,
  [Process 49] int  ,
  [Process 50] int  
)
AS
BEGIN
	INSERT into @ret
		select top 1 * from Table_Master_Workplan where [Flow ID] = @flowID and [Valid Status] = 'VALID' order by [Revision Number] desc
	 RETURN
END
GO
/****** Object:  UserDefinedFunction [dbo].[FcnRead_Run_Traceability]    Script Date: 5/23/2023 8:48:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[FcnRead_Run_Traceability]
(
	@serialNumber nvarchar(max)
)
RETURNS @ret TABLE(
  [Serial Number] varchar(50) ,
  [Batch ID] varchar(50) ,
  [Refference Name] varchar(50) ,
  [Work Order] varchar(50),
  [Station ID] int ,
  [Station Suffix] int  ,
  [Station Name] varchar(50) ,
  [User ID] varchar(50)  ,
  [Cavity Number] varchar(50) ,
  [Time In] datetime,
  [Time Out] datetime ,
  [Status Result] varchar(50),
  [Status Running] varchar(50),
  [Transact By] varchar(50) ,
  [FullRefference]varchar(50)
)
AS
BEGIN
	INSERT into @ret
			select * from Table_Run_Traceability where [Serial Number] = @serialNumber
	 RETURN
END
GO
/****** Object:  Table [dbo].[Table_Master_Batch]    Script Date: 5/23/2023 8:48:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Table_Master_Batch](
	[Batch_ID] [varchar](50) NOT NULL,
	[Work_Order] [varchar](50) NULL,
	[Refference_Name] [varchar](50) NULL,
 CONSTRAINT [PK_Table_Master_Batch] PRIMARY KEY CLUSTERED 
(
	[Batch_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Table_Master_Line]    Script Date: 5/23/2023 8:48:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Table_Master_Line](
	[Line_ID] [int] NOT NULL,
	[Line_Name] [varchar](50) NOT NULL,
	[Line_Location] [varchar](50) NOT NULL,
	[Line_Description] [varchar](50) NOT NULL,
	[Last_Modify] [datetime] NULL,
	[Transact_By] [varchar](50) NULL,
 CONSTRAINT [PK_Table_Master_Line] PRIMARY KEY CLUSTERED 
(
	[Line_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Table_Master_Material]    Script Date: 5/23/2023 8:48:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Table_Master_Material](
	[Material_ID] [int] NOT NULL,
	[Material_Name] [varchar](50) NOT NULL,
	[Refference_Name] [varchar](50) NOT NULL,
	[Station_ID] [int] NOT NULL,
	[Revision_Number] [int] NULL,
	[Last_Modify] [datetime] NULL,
	[Transact_By] [varchar](50) NULL,
	[Part_Qty] [int] NULL,
	[Part_1] [varchar](50) NULL,
	[Part_2] [varchar](50) NULL,
	[Part_3] [varchar](50) NULL,
	[Part_4] [varchar](50) NULL,
	[Part_5] [varchar](50) NULL,
	[Part_6] [varchar](50) NULL,
	[Part_7] [varchar](50) NULL,
	[Part_8] [varchar](50) NULL,
	[Part_9] [varchar](50) NULL,
	[Part_10] [varchar](50) NULL,
	[Part_11] [varchar](50) NULL,
	[Part_12] [varchar](50) NULL,
	[Part_13] [varchar](50) NULL,
	[Part_14] [varchar](50) NULL,
	[Part_15] [varchar](50) NULL,
	[Part_16] [varchar](50) NULL,
	[Part_17] [varchar](50) NULL,
	[Part_18] [varchar](50) NULL,
	[Part_19] [varchar](50) NULL,
	[Part_20] [varchar](50) NULL,
	[Part_21] [varchar](50) NULL,
	[Part_22] [varchar](50) NULL,
	[Part_23] [varchar](50) NULL,
	[Part_24] [varchar](50) NULL,
	[Part_25] [varchar](50) NULL,
	[Part_26] [varchar](50) NULL,
	[Part_27] [varchar](50) NULL,
	[Part_28] [varchar](50) NULL,
	[Part_29] [varchar](50) NULL,
	[Part_30] [varchar](50) NULL,
	[Part_31] [varchar](50) NULL,
	[Part_32] [varchar](50) NULL,
	[Part_33] [varchar](50) NULL,
	[Part_34] [varchar](50) NULL,
	[Part_35] [varchar](50) NULL,
	[Part_36] [varchar](50) NULL,
	[Part_37] [varchar](50) NULL,
	[Part_38] [varchar](50) NULL,
	[Part_39] [varchar](50) NULL,
	[Part_40] [varchar](50) NULL,
	[Part_41] [varchar](50) NULL,
	[Part_42] [varchar](50) NULL,
	[Part_43] [varchar](50) NULL,
	[Part_44] [varchar](50) NULL,
	[Part_45] [varchar](50) NULL,
	[Part_46] [varchar](50) NULL,
	[Part_47] [varchar](50) NULL,
	[Part_48] [varchar](50) NULL,
	[Part_49] [varchar](50) NULL,
	[Part_50] [varchar](50) NULL,
 CONSTRAINT [PK_Table_Master_Material] PRIMARY KEY CLUSTERED 
(
	[Material_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Table_Master_Order]    Script Date: 5/23/2023 8:48:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Table_Master_Order](
	[Work_Order] [varchar](50) NOT NULL,
	[Refference_Name] [varchar](50) NOT NULL,
	[Work_Plan] [varchar](50) NOT NULL,
	[Qty_Order] [int] NOT NULL,
	[Qty_Launching] [int] NULL,
	[Status_Order] [varchar](50) NULL,
	[Date_Order] [datetime] NULL,
	[Date_Complete] [datetime] NULL,
	[Transact_By] [varchar](50) NULL,
	[Username] [varchar](50) NULL,
	[WO_Comment] [varchar](300) NULL,
	[Priority_WO] [int] NULL,
	[Station_ID] [int] NULL,
	[Station_Suffix] [int] NULL,
 CONSTRAINT [PK_Table_Master_Launching] PRIMARY KEY CLUSTERED 
(
	[Work_Order] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Table_Master_Process]    Script Date: 5/23/2023 8:48:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Table_Master_Process](
	[Process_ID] [int] NOT NULL,
	[Process_Name] [varchar](50) NOT NULL,
	[Process_By] [varchar](50) NOT NULL,
	[Process_Description] [varchar](50) NOT NULL,
	[Last_Modify] [datetime] NULL,
	[Transact_By] [varchar](50) NULL,
 CONSTRAINT [PK_Table_Master_Process] PRIMARY KEY CLUSTERED 
(
	[Process_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Table_Master_Process_By]    Script Date: 5/23/2023 8:48:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Table_Master_Process_By](
	[ID] [int] NOT NULL,
	[Process_By] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Table_Master_Process_By] PRIMARY KEY CLUSTERED 
(
	[ID] ASC,
	[Process_By] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Table_Master_Refference]    Script Date: 5/23/2023 8:48:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Table_Master_Refference](
	[Refference_ID] [int] NOT NULL,
	[Refference_Name] [varchar](50) NOT NULL,
	[Refference_Description] [varchar](50) NOT NULL,
	[Last_Modify] [datetime] NULL,
	[Transact_By] [varchar](50) NULL,
 CONSTRAINT [PK_Table_Master_Refference] PRIMARY KEY CLUSTERED 
(
	[Refference_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Table_Master_Serial_Counter]    Script Date: 5/23/2023 8:48:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Table_Master_Serial_Counter](
	[Station_ID] [int] NULL,
	[Station_Suffix] [int] NULL,
	[Year_Code] [int] NULL,
	[Week_Code] [int] NULL,
	[Counter_Code] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Table_Master_Station]    Script Date: 5/23/2023 8:48:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Table_Master_Station](
	[Station_ID] [int] NOT NULL,
	[Station_Suffix] [int] NOT NULL,
	[Station_Name] [varchar](50) NOT NULL,
	[Process_ID] [int] NOT NULL,
	[Line_ID] [int] NOT NULL,
	[Target_Output] [int] NOT NULL,
	[Target_Yield] [int] NOT NULL,
	[Last_Modify] [datetime] NULL,
	[Transact_By] [varchar](50) NULL,
	[Tester_Name] [varchar](50) NULL,
 CONSTRAINT [PK_Table_Master_Station] PRIMARY KEY CLUSTERED 
(
	[Station_ID] ASC,
	[Station_Suffix] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Table_Master_Status_Order]    Script Date: 5/23/2023 8:48:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Table_Master_Status_Order](
	[ID] [int] NOT NULL,
	[Status_Order] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Table_Master_Status_Order] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Table_Master_Status_Result]    Script Date: 5/23/2023 8:48:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Table_Master_Status_Result](
	[ID] [int] NOT NULL,
	[Result_Status] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Table_Master_Result_Status] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Table_Master_Status_Running]    Script Date: 5/23/2023 8:48:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Table_Master_Status_Running](
	[ID] [int] NOT NULL,
	[Status_Running] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Table_Master_Status_Running] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Table_Master_User]    Script Date: 5/23/2023 8:48:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Table_Master_User](
	[ID_User] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[Station_ID] [int] NULL,
	[User_Level] [int] NULL,
 CONSTRAINT [PK_Table_Master_User] PRIMARY KEY CLUSTERED 
(
	[ID_User] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Table_Master_User_Level]    Script Date: 5/23/2023 8:48:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Table_Master_User_Level](
	[User_Level] [int] NOT NULL,
	[ID_Roles] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Table_Master_Workplan]    Script Date: 5/23/2023 8:48:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Table_Master_Workplan](
	[Flow_ID] [int] NOT NULL,
	[Flow_Name] [varchar](50) NOT NULL,
	[Refference_Name] [varchar](50) NOT NULL,
	[Line_ID] [int] NOT NULL,
	[Valid_Status] [varchar](50) NOT NULL,
	[Revision_Number] [int] NOT NULL,
	[Last_Modify] [datetime] NULL,
	[Transact_By] [varchar](50) NULL,
	[Flow_Description] [varchar](300) NOT NULL,
	[Process_Qty] [int] NULL,
	[Process_1] [int] NOT NULL,
	[Process_2] [int] NOT NULL,
	[Process_3] [int] NOT NULL,
	[Process_4] [int] NOT NULL,
	[Process_5] [int] NOT NULL,
	[Process_6] [int] NOT NULL,
	[Process_7] [int] NOT NULL,
	[Process_8] [int] NOT NULL,
	[Process_9] [int] NOT NULL,
	[Process_10] [int] NOT NULL,
	[Process_11] [int] NOT NULL,
	[Process_12] [int] NOT NULL,
	[Process_13] [int] NOT NULL,
	[Process_14] [int] NOT NULL,
	[Process_15] [int] NOT NULL,
	[Process_16] [int] NOT NULL,
	[Process_17] [int] NOT NULL,
	[Process_18] [int] NOT NULL,
	[Process_19] [int] NOT NULL,
	[Process_20] [int] NOT NULL,
	[Process_21] [int] NOT NULL,
	[Process_22] [int] NOT NULL,
	[Process_23] [int] NOT NULL,
	[Process_24] [int] NOT NULL,
	[Process_25] [int] NOT NULL,
	[Process_26] [int] NOT NULL,
	[Process_27] [int] NOT NULL,
	[Process_28] [int] NOT NULL,
	[Process_29] [int] NOT NULL,
	[Process_30] [int] NOT NULL,
	[Process_31] [int] NOT NULL,
	[Process_32] [int] NOT NULL,
	[Process_33] [int] NOT NULL,
	[Process_34] [int] NOT NULL,
	[Process_35] [int] NOT NULL,
	[Process_36] [int] NOT NULL,
	[Process_37] [int] NOT NULL,
	[Process_38] [int] NOT NULL,
	[Process_39] [int] NOT NULL,
	[Process_40] [int] NOT NULL,
	[Process_41] [int] NOT NULL,
	[Process_42] [int] NOT NULL,
	[Process_43] [int] NOT NULL,
	[Process_44] [int] NOT NULL,
	[Process_45] [int] NOT NULL,
	[Process_46] [int] NOT NULL,
	[Process_47] [int] NOT NULL,
	[Process_48] [int] NOT NULL,
	[Process_49] [int] NOT NULL,
	[Process_50] [int] NOT NULL,
 CONSTRAINT [PK_Table_Master_Workplan] PRIMARY KEY CLUSTERED 
(
	[Flow_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Table_Run_Traceability]    Script Date: 5/23/2023 8:48:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Table_Run_Traceability](
	[Serial_Number] [varchar](50) NOT NULL,
	[Batch_ID] [varchar](50) NOT NULL,
	[Refference_Name] [varchar](50) NOT NULL,
	[Work_Order] [varchar](50) NOT NULL,
	[Station_ID] [int] NOT NULL,
	[Station_Suffix] [int] NOT NULL,
	[Station_Name] [varchar](50) NOT NULL,
	[User_ID] [varchar](50) NOT NULL,
	[Cavity_Number] [varchar](50) NOT NULL,
	[Time_In] [datetime] NULL,
	[Time_Out] [datetime] NULL,
	[Status_Result] [varchar](50) NOT NULL,
	[Status_Running] [varchar](50) NOT NULL,
	[Transact_By] [varchar](50) NULL,
	[FullRefference] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 5/23/2023 8:48:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](50) NULL,
	[password] [varchar](50) NULL,
	[role] [varchar](50) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Table_Master_Order] ADD  CONSTRAINT [DF_Table_Master_Launching_Qty Launching]  DEFAULT ((0)) FOR [Qty_Launching]
GO
ALTER TABLE [dbo].[Table_Master_Order] ADD  CONSTRAINT [DF_Table_Master_Order_Station ID]  DEFAULT ((0)) FOR [Station_ID]
GO
ALTER TABLE [dbo].[Table_Master_Order] ADD  CONSTRAINT [DF_Table_Master_Order_Station Suffix]  DEFAULT ((0)) FOR [Station_Suffix]
GO
ALTER TABLE [dbo].[Table_Master_Workplan] ADD  CONSTRAINT [DF_Table_Master_Workplan_Revision Number]  DEFAULT ((1)) FOR [Revision_Number]
GO
ALTER TABLE [dbo].[Table_Master_Workplan] ADD  CONSTRAINT [DF_Table_Master_Workplan_Process 1]  DEFAULT ((0)) FOR [Process_1]
GO
ALTER TABLE [dbo].[Table_Master_Workplan] ADD  CONSTRAINT [DF_Table_Master_Workplan_Process 2]  DEFAULT ((0)) FOR [Process_2]
GO
ALTER TABLE [dbo].[Table_Master_Workplan] ADD  CONSTRAINT [DF_Table_Master_Workplan_Process 3]  DEFAULT ((0)) FOR [Process_3]
GO
ALTER TABLE [dbo].[Table_Master_Workplan] ADD  CONSTRAINT [DF_Table_Master_Workplan_Process 4]  DEFAULT ((0)) FOR [Process_4]
GO
ALTER TABLE [dbo].[Table_Master_Workplan] ADD  CONSTRAINT [DF_Table_Master_Workplan_Process 5]  DEFAULT ((0)) FOR [Process_5]
GO
ALTER TABLE [dbo].[Table_Master_Workplan] ADD  CONSTRAINT [DF_Table_Master_Workplan_Process 6]  DEFAULT ((0)) FOR [Process_6]
GO
ALTER TABLE [dbo].[Table_Master_Workplan] ADD  CONSTRAINT [DF_Table_Master_Workplan_Process 7]  DEFAULT ((0)) FOR [Process_7]
GO
ALTER TABLE [dbo].[Table_Master_Workplan] ADD  CONSTRAINT [DF_Table_Master_Workplan_Process 8]  DEFAULT ((0)) FOR [Process_8]
GO
ALTER TABLE [dbo].[Table_Master_Workplan] ADD  CONSTRAINT [DF_Table_Master_Workplan_Process 9]  DEFAULT ((0)) FOR [Process_9]
GO
ALTER TABLE [dbo].[Table_Master_Workplan] ADD  CONSTRAINT [DF_Table_Master_Workplan_Process 10]  DEFAULT ((0)) FOR [Process_10]
GO
ALTER TABLE [dbo].[Table_Master_Workplan] ADD  CONSTRAINT [DF_Table_Master_Workplan_Process 11]  DEFAULT ((0)) FOR [Process_11]
GO
ALTER TABLE [dbo].[Table_Master_Workplan] ADD  CONSTRAINT [DF_Table_Master_Workplan_Process 12]  DEFAULT ((0)) FOR [Process_12]
GO
ALTER TABLE [dbo].[Table_Master_Workplan] ADD  CONSTRAINT [DF_Table_Master_Workplan_Process 13]  DEFAULT ((0)) FOR [Process_13]
GO
ALTER TABLE [dbo].[Table_Master_Workplan] ADD  CONSTRAINT [DF_Table_Master_Workplan_Process 14]  DEFAULT ((0)) FOR [Process_14]
GO
ALTER TABLE [dbo].[Table_Master_Workplan] ADD  CONSTRAINT [DF_Table_Master_Workplan_Process 15]  DEFAULT ((0)) FOR [Process_15]
GO
ALTER TABLE [dbo].[Table_Master_Workplan] ADD  CONSTRAINT [DF_Table_Master_Workplan_Process 16]  DEFAULT ((0)) FOR [Process_16]
GO
ALTER TABLE [dbo].[Table_Master_Workplan] ADD  CONSTRAINT [DF_Table_Master_Workplan_Process 17]  DEFAULT ((0)) FOR [Process_17]
GO
ALTER TABLE [dbo].[Table_Master_Workplan] ADD  CONSTRAINT [DF_Table_Master_Workplan_Process 18]  DEFAULT ((0)) FOR [Process_18]
GO
ALTER TABLE [dbo].[Table_Master_Workplan] ADD  CONSTRAINT [DF_Table_Master_Workplan_Process 19]  DEFAULT ((0)) FOR [Process_19]
GO
ALTER TABLE [dbo].[Table_Master_Workplan] ADD  CONSTRAINT [DF_Table_Master_Workplan_Process 20]  DEFAULT ((0)) FOR [Process_20]
GO
ALTER TABLE [dbo].[Table_Master_Workplan] ADD  CONSTRAINT [DF_Table_Master_Workplan_Process 21]  DEFAULT ((0)) FOR [Process_21]
GO
ALTER TABLE [dbo].[Table_Master_Workplan] ADD  CONSTRAINT [DF_Table_Master_Workplan_Process 22]  DEFAULT ((0)) FOR [Process_22]
GO
ALTER TABLE [dbo].[Table_Master_Workplan] ADD  CONSTRAINT [DF_Table_Master_Workplan_Process 23]  DEFAULT ((0)) FOR [Process_23]
GO
ALTER TABLE [dbo].[Table_Master_Workplan] ADD  CONSTRAINT [DF_Table_Master_Workplan_Process 24]  DEFAULT ((0)) FOR [Process_24]
GO
ALTER TABLE [dbo].[Table_Master_Workplan] ADD  CONSTRAINT [DF_Table_Master_Workplan_Process 25]  DEFAULT ((0)) FOR [Process_25]
GO
ALTER TABLE [dbo].[Table_Master_Workplan] ADD  CONSTRAINT [DF_Table_Master_Workplan_Process 26]  DEFAULT ((0)) FOR [Process_26]
GO
ALTER TABLE [dbo].[Table_Master_Workplan] ADD  CONSTRAINT [DF_Table_Master_Workplan_Process 27]  DEFAULT ((0)) FOR [Process_27]
GO
ALTER TABLE [dbo].[Table_Master_Workplan] ADD  CONSTRAINT [DF_Table_Master_Workplan_Process 28]  DEFAULT ((0)) FOR [Process_28]
GO
ALTER TABLE [dbo].[Table_Master_Workplan] ADD  CONSTRAINT [DF_Table_Master_Workplan_Process 29]  DEFAULT ((0)) FOR [Process_29]
GO
ALTER TABLE [dbo].[Table_Master_Workplan] ADD  CONSTRAINT [DF_Table_Master_Workplan_Process 30]  DEFAULT ((0)) FOR [Process_30]
GO
ALTER TABLE [dbo].[Table_Master_Workplan] ADD  CONSTRAINT [DF_Table_Master_Workplan_Process 31]  DEFAULT ((0)) FOR [Process_31]
GO
ALTER TABLE [dbo].[Table_Master_Workplan] ADD  CONSTRAINT [DF_Table_Master_Workplan_Process 32]  DEFAULT ((0)) FOR [Process_32]
GO
ALTER TABLE [dbo].[Table_Master_Workplan] ADD  CONSTRAINT [DF_Table_Master_Workplan_Process 33]  DEFAULT ((0)) FOR [Process_33]
GO
ALTER TABLE [dbo].[Table_Master_Workplan] ADD  CONSTRAINT [DF_Table_Master_Workplan_Process 34]  DEFAULT ((0)) FOR [Process_34]
GO
ALTER TABLE [dbo].[Table_Master_Workplan] ADD  CONSTRAINT [DF_Table_Master_Workplan_Process 35]  DEFAULT ((0)) FOR [Process_35]
GO
ALTER TABLE [dbo].[Table_Master_Workplan] ADD  CONSTRAINT [DF_Table_Master_Workplan_Process 36]  DEFAULT ((0)) FOR [Process_36]
GO
ALTER TABLE [dbo].[Table_Master_Workplan] ADD  CONSTRAINT [DF_Table_Master_Workplan_Process 37]  DEFAULT ((0)) FOR [Process_37]
GO
ALTER TABLE [dbo].[Table_Master_Workplan] ADD  CONSTRAINT [DF_Table_Master_Workplan_Process 38]  DEFAULT ((0)) FOR [Process_38]
GO
ALTER TABLE [dbo].[Table_Master_Workplan] ADD  CONSTRAINT [DF_Table_Master_Workplan_Process 39]  DEFAULT ((0)) FOR [Process_39]
GO
ALTER TABLE [dbo].[Table_Master_Workplan] ADD  CONSTRAINT [DF_Table_Master_Workplan_Process 40]  DEFAULT ((0)) FOR [Process_40]
GO
ALTER TABLE [dbo].[Table_Master_Workplan] ADD  CONSTRAINT [DF_Table_Master_Workplan_Process 41]  DEFAULT ((0)) FOR [Process_41]
GO
ALTER TABLE [dbo].[Table_Master_Workplan] ADD  CONSTRAINT [DF_Table_Master_Workplan_Process 42]  DEFAULT ((0)) FOR [Process_42]
GO
ALTER TABLE [dbo].[Table_Master_Workplan] ADD  CONSTRAINT [DF_Table_Master_Workplan_Process 43]  DEFAULT ((0)) FOR [Process_43]
GO
ALTER TABLE [dbo].[Table_Master_Workplan] ADD  CONSTRAINT [DF_Table_Master_Workplan_Process 44]  DEFAULT ((0)) FOR [Process_44]
GO
ALTER TABLE [dbo].[Table_Master_Workplan] ADD  CONSTRAINT [DF_Table_Master_Workplan_Process 45]  DEFAULT ((0)) FOR [Process_45]
GO
ALTER TABLE [dbo].[Table_Master_Workplan] ADD  CONSTRAINT [DF_Table_Master_Workplan_Process 46]  DEFAULT ((0)) FOR [Process_46]
GO
ALTER TABLE [dbo].[Table_Master_Workplan] ADD  CONSTRAINT [DF_Table_Master_Workplan_Process 47]  DEFAULT ((0)) FOR [Process_47]
GO
ALTER TABLE [dbo].[Table_Master_Workplan] ADD  CONSTRAINT [DF_Table_Master_Workplan_Process 48]  DEFAULT ((0)) FOR [Process_48]
GO
ALTER TABLE [dbo].[Table_Master_Workplan] ADD  CONSTRAINT [DF_Table_Master_Workplan_Process 49]  DEFAULT ((0)) FOR [Process_49]
GO
ALTER TABLE [dbo].[Table_Master_Workplan] ADD  CONSTRAINT [DF_Table_Master_Workplan_Process 50]  DEFAULT ((0)) FOR [Process_50]
GO
ALTER TABLE [dbo].[Table_Run_Traceability] ADD  CONSTRAINT [DF_Table_Run_Traceability_Time In]  DEFAULT (getdate()) FOR [Time_In]
GO
ALTER TABLE [dbo].[Table_Run_Traceability] ADD  CONSTRAINT [DF_Table_Run_Traceability_Time Out]  DEFAULT (getdate()) FOR [Time_Out]
GO
ALTER TABLE [dbo].[Table_Run_Traceability] ADD  CONSTRAINT [DF_Table_Run_Traceability_FullRefference]  DEFAULT ('') FOR [FullRefference]
GO
