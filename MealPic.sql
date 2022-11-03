/****** Object:  Table [dbo].[MealPic]    Script Date: 7/8/2020 8:18:22 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MealPic](
	[MealPicId] [int] IDENTITY(1,1) NOT NULL,
	[MealId] [int] NOT NULL,
	[ImgSrc] [varchar](256) NOT NULL,
	[BriefDescription] [varchar](50) NOT NULL,
	[UploadDt] [datetime] NOT NULL,
 CONSTRAINT [PK_MealPic] PRIMARY KEY CLUSTERED 
(
	[MealPicId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MealPic] ADD  CONSTRAINT [DF_MealPic_UploadDt]  DEFAULT (getdate()) FOR [UploadDt]
GO

ALTER TABLE [dbo].[MealPic]  WITH CHECK ADD  CONSTRAINT [FK_MealPic_Meal] FOREIGN KEY([MealId])
REFERENCES [dbo].[Meal] ([MealId])
GO

ALTER TABLE [dbo].[MealPic] CHECK CONSTRAINT [FK_MealPic_Meal]
GO