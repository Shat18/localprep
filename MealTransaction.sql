SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MealTransaction](
	[MealTransactionId] [int] IDENTITY(1,1) NOT NULL,
	[MealId] [int] NOT NULL,
	[Price] [decimal](9, 2) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[CreateDt] [datetime] NOT NULL,
	[TransactionId] [varchar](50) NULL,
	[ResponseCode] [varchar](20) NULL,
	[ResponseDescription] [varchar](250) NULL,
	[AuthCode] [varchar](100) NULL,
 CONSTRAINT [PK_MealTransaction] PRIMARY KEY CLUSTERED 
(
	[MealTransactionId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MealTransaction] ADD  CONSTRAINT [DF_MealTransaction_CreateDt]  DEFAULT (getdate()) FOR [CreateDt]
GO

ALTER TABLE [dbo].[MealTransaction]  WITH CHECK ADD  CONSTRAINT [FK_MealTransaction_Meal] FOREIGN KEY([MealId])
REFERENCES [dbo].[Meal] ([MealId])
GO

ALTER TABLE [dbo].[MealTransaction] CHECK CONSTRAINT [FK_MealTransaction_Meal]
GO