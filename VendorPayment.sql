/****** Object:  Table [dbo].[VendorPayment]    Script Date: 7/8/2020 8:21:50 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[VendorPayment](
	[VendorPaymentId] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[PlanName] [varchar](20) NOT NULL,
	[Price] [decimal](9, 2) NOT NULL,
	[ExpirationDate] [date] NOT NULL,
	[TransactionId] [varchar](50) NULL,
	[ResponseCode] [varchar](20) NULL,
	[ResponseDescription] [varchar](250) NULL,
	[AuthCode] [varchar](100) NULL,
 CONSTRAINT [PK_VendorPayment] PRIMARY KEY CLUSTERED 
(
	[VendorPaymentId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[VendorPayment]  WITH CHECK ADD  CONSTRAINT [FK_VendorPayment_Vendor] FOREIGN KEY([VendorId])
REFERENCES [dbo].[Vendor] ([VendorId])
GO

ALTER TABLE [dbo].[VendorPayment] CHECK CONSTRAINT [FK_VendorPayment_Vendor]
GO