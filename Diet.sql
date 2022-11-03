/****** Object:  Table [dbo].[Diet]    Script Date: 7/8/2020 8:12:06 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Diet](
	[DietId] [int] IDENTITY(1,1) NOT NULL,
	[DietShortName] [varchar](20) NOT NULL,
	[DietLongName] [varchar](120) NULL,
	[DietDescription] [varchar](4000) NULL,
	[ImgSrc] [varchar](256) NULL,
	[IsActive] [bit] NOT NULL,
	[CreateDt] [datetime] NOT NULL,
 CONSTRAINT [PK_Diet] PRIMARY KEY CLUSTERED 
(
	[DietId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Diet] ADD  CONSTRAINT [DF_Diet_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO

ALTER TABLE [dbo].[Diet] ADD  CONSTRAINT [DF_Diet_CreateDt]  DEFAULT (getdate()) FOR [CreateDt]
GO