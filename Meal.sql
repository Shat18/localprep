/****** Object:  Table [dbo].[Meal]    Script Date: 7/8/2020 8:12:32 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Meal](
	[MealId] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[MealName] [varchar](100) NOT NULL,
	[Ingredients] [varchar](4000) NULL,
	[SpecialNotes] [varchar](4000) NULL,
	[MealPrice] [decimal](9, 2) NOT NULL,
	[CuisineId] [int] NOT NULL,
	[DietId] [int] NULL,
	[SubmitDt] [datetime] NOT NULL,
	[UpdateDt] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
	[MealDescription] [varchar](4000) NULL,
 CONSTRAINT [PK_Meal] PRIMARY KEY CLUSTERED 
(
	[MealId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Meal] ADD  CONSTRAINT [DF_Meal_SubmitDt]  DEFAULT (getdate()) FOR [SubmitDt]
GO

ALTER TABLE [dbo].[Meal]  WITH CHECK ADD  CONSTRAINT [FK_Meal_Cuisine] FOREIGN KEY([CuisineId])
REFERENCES [dbo].[Cuisine] ([CuisineId])
GO

ALTER TABLE [dbo].[Meal] CHECK CONSTRAINT [FK_Meal_Cuisine]
GO

ALTER TABLE [dbo].[Meal]  WITH CHECK ADD  CONSTRAINT [FK_Meal_Diet] FOREIGN KEY([DietId])
REFERENCES [dbo].[Diet] ([DietId])
GO

ALTER TABLE [dbo].[Meal] CHECK CONSTRAINT [FK_Meal_Diet]
GO

ALTER TABLE [dbo].[Meal]  WITH CHECK ADD  CONSTRAINT [FK_Meal_Vendor] FOREIGN KEY([VendorId])
REFERENCES [dbo].[Vendor] ([VendorId])
GO

ALTER TABLE [dbo].[Meal] CHECK CONSTRAINT [FK_Meal_Vendor]
GO
