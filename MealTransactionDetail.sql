SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MealTransactionDetail](
	[MealTransactionDetailId] [int] IDENTITY(1,1) NOT NULL,
	[MealTransactionId] [int] NOT NULL,
	[MealId] [int] NOT NULL,
	[MealQty] [int] NOT NULL,
	[Price] [decimal](9, 2) NOT NULL,
	[PickupDelivery] [varchar](20) NOT NULL,
	[PickupDeliveryDt] [date] NOT NULL,
 CONSTRAINT [PK_MealTransactionDetail] PRIMARY KEY CLUSTERED 
(
	[MealTransactionDetailId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MealTransactionDetail]  WITH CHECK ADD  CONSTRAINT [FK_MealTransaction_Id] FOREIGN KEY([MealTransactionId])
REFERENCES [dbo].[MealTransaction] ([MealTransactionId])
GO

ALTER TABLE [dbo].[MealTransactionDetail] CHECK CONSTRAINT [FK_MealTransaction_Id]
GO

ALTER TABLE [dbo].[MealTransactionDetail]  WITH CHECK ADD  CONSTRAINT [FK_MealTransactionDetail_MealId] FOREIGN KEY([MealId])
REFERENCES [dbo].[Meal] ([MealId])
GO

ALTER TABLE [dbo].[MealTransactionDetail] CHECK CONSTRAINT [FK_MealTransactionDetail_MealId]
GO