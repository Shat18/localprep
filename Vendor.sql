/****** Object:  Table [dbo].[Vendor]    Script Date: 7/8/2020 8:13:50 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Vendor](
	[VendorId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[VendorName] [varchar](50) NOT NULL,
	[CompanyName] [varchar](50) NULL,
	[Address1] [varchar](80) NOT NULL,
	[Address2] [varchar](50) NULL,
	[City] [varchar](50) NOT NULL,
	[StateId] [int] NOT NULL,
	[Zip] [varchar](20) NOT NULL,
	[PhoneNumber] [varchar](16) NOT NULL,
	[EmailAddress] [varchar](100) NOT NULL,
	[VendorDescription] [varchar](2500) NULL,
	[DeliveryAvailable] [bit] NOT NULL,
	[PickupAvailable] [bit] NOT NULL,
	[Latitude] [decimal](9, 6) NOT NULL,
	[Longitude] [decimal](9, 6) NOT NULL,
	[PlaceId] [nvarchar](500) NOT NULL,
	[FormattedAddress] [varchar](500) NOT NULL,
	[ImgSrc] [varchar](256) NULL,
	[IsApproved] [bit] NOT NULL,
	[ApprovedById] [nvarchar](128) NULL,
	[SubmitDt] [datetime] NOT NULL,
 CONSTRAINT [PK_Vendor] PRIMARY KEY CLUSTERED 
(
	[VendorId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Vendor] ADD  CONSTRAINT [DF_Vendor_SubmitDt]  DEFAULT (getdate()) FOR [SubmitDt]
GO

ALTER TABLE [dbo].[Vendor]  WITH CHECK ADD  CONSTRAINT [FK_Vendor_State] FOREIGN KEY([StateId])
REFERENCES [dbo].[State] ([StateId])
GO

ALTER TABLE [dbo].[Vendor] CHECK CONSTRAINT [FK_Vendor_State]
GO