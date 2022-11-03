/****** Object:  Table [dbo].[Cuisine]    Script Date: 7/8/2020 8:11:22 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Cuisine](
	[CuisineId] [int] IDENTITY(1,1) NOT NULL,
	[CuisineName] [varchar](50) NOT NULL,
	[ImgSrc] [varchar](256) NULL,
	[IsActive] [bit] NOT NULL,
	[CreateDt] [datetime] NOT NULL,
 CONSTRAINT [PK_Cuisine] PRIMARY KEY CLUSTERED 
(
	[CuisineId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Cuisine] ADD  CONSTRAINT [DF_Cuisine_CreateDt]  DEFAULT (getdate()) FOR [CreateDt]
GO