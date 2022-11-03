/****** Object:  Table [dbo].[MealRating]    Script Date: 7/8/2020 8:20:39 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MealRating](
	[MealRatingId] [int] IDENTITY(1,1) NOT NULL,
	[MealId] [int] NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[StarRating] [int] NOT NULL,
	[RatingComments] [nvarchar](4000) NULL,
	[IsActive] [bit] NOT NULL,
	[CreateDt] [datetime] NOT NULL,
 CONSTRAINT [PK_MealRating] PRIMARY KEY CLUSTERED 
(
	[MealRatingId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MealRating] ADD  CONSTRAINT [DF_MealRating_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[MealRating] ADD  CONSTRAINT [DF_MealRating_CreateDt]  DEFAULT (getdate()) FOR [CreateDt]
GO

ALTER TABLE [dbo].[MealRating]  WITH CHECK ADD  CONSTRAINT [FK_MealRating_Meal] FOREIGN KEY([MealId])
REFERENCES [dbo].[Meal] ([MealId])
GO

ALTER TABLE [dbo].[MealRating] CHECK CONSTRAINT [FK_MealRating_Meal]
GO