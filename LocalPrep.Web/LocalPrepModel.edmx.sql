
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/18/2021 11:10:53
-- Generated from EDMX file: D:\Working Copy\LocalPrepProd\LocalPrep.Web\LocalPrepModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [localprepdb];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserRoles_dbo_AspNetRoles_RoleId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo_AspNetUserRoles_dbo_AspNetRoles_RoleId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserRoles_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo_AspNetUserRoles_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_Meal_Cuisine]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Meals] DROP CONSTRAINT [FK_Meal_Cuisine];
GO
IF OBJECT_ID(N'[dbo].[FK_Meal_Diet]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Meals] DROP CONSTRAINT [FK_Meal_Diet];
GO
IF OBJECT_ID(N'[dbo].[FK_Meal_Vendor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Meals] DROP CONSTRAINT [FK_Meal_Vendor];
GO
IF OBJECT_ID(N'[dbo].[FK_MealAddOn_Meal]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MealAddOns] DROP CONSTRAINT [FK_MealAddOn_Meal];
GO
IF OBJECT_ID(N'[dbo].[FK_MealIngredient_Meal]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MealIngredients] DROP CONSTRAINT [FK_MealIngredient_Meal];
GO
IF OBJECT_ID(N'[dbo].[FK_MealPic_Meal]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MealPics] DROP CONSTRAINT [FK_MealPic_Meal];
GO
IF OBJECT_ID(N'[dbo].[FK_MealRating_Meal]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MealRatings] DROP CONSTRAINT [FK_MealRating_Meal];
GO
IF OBJECT_ID(N'[dbo].[FK_MealTransaction_Id]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MealTransactionDetails] DROP CONSTRAINT [FK_MealTransaction_Id];
GO
IF OBJECT_ID(N'[dbo].[FK_MealTransactionDetail_MealId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MealTransactionDetails] DROP CONSTRAINT [FK_MealTransactionDetail_MealId];
GO
IF OBJECT_ID(N'[dbo].[FK_MealTransactionDetailAddOn_MealAddOn]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MealTransactionDetailAddOns] DROP CONSTRAINT [FK_MealTransactionDetailAddOn_MealAddOn];
GO
IF OBJECT_ID(N'[dbo].[FK_MealTransactionDetailAddOn_MealTransactionDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MealTransactionDetailAddOns] DROP CONSTRAINT [FK_MealTransactionDetailAddOn_MealTransactionDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_ShoppingCart_Meal]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ShoppingCarts] DROP CONSTRAINT [FK_ShoppingCart_Meal];
GO
IF OBJECT_ID(N'[dbo].[FK_ShoppingCartAddOn_MealAddOn]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ShoppingCartAddOns] DROP CONSTRAINT [FK_ShoppingCartAddOn_MealAddOn];
GO
IF OBJECT_ID(N'[dbo].[FK_ShoppingCartAddOn_ShoppingCart]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ShoppingCartAddOns] DROP CONSTRAINT [FK_ShoppingCartAddOn_ShoppingCart];
GO
IF OBJECT_ID(N'[dbo].[FK_State_Country]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[States] DROP CONSTRAINT [FK_State_Country];
GO
IF OBJECT_ID(N'[dbo].[FK_Vendor_State]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Vendors] DROP CONSTRAINT [FK_Vendor_State];
GO
IF OBJECT_ID(N'[dbo].[FK_VendorPayment_Vendor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VendorPayments] DROP CONSTRAINT [FK_VendorPayment_Vendor];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[__MigrationHistory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[__MigrationHistory];
GO
IF OBJECT_ID(N'[dbo].[AspNetRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetRoles];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserClaims]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserClaims];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserLogins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserLogins];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserRoles];
GO
IF OBJECT_ID(N'[dbo].[AspNetUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[Contacts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Contacts];
GO
IF OBJECT_ID(N'[dbo].[Countries]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Countries];
GO
IF OBJECT_ID(N'[dbo].[Cuisines]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Cuisines];
GO
IF OBJECT_ID(N'[dbo].[Diets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Diets];
GO
IF OBJECT_ID(N'[dbo].[MealAddOns]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MealAddOns];
GO
IF OBJECT_ID(N'[dbo].[MealIngredients]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MealIngredients];
GO
IF OBJECT_ID(N'[dbo].[MealPics]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MealPics];
GO
IF OBJECT_ID(N'[dbo].[MealRatings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MealRatings];
GO
IF OBJECT_ID(N'[dbo].[Meals]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Meals];
GO
IF OBJECT_ID(N'[dbo].[MealTransactionDetailAddOns]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MealTransactionDetailAddOns];
GO
IF OBJECT_ID(N'[dbo].[MealTransactionDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MealTransactionDetails];
GO
IF OBJECT_ID(N'[dbo].[MealTransactions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MealTransactions];
GO
IF OBJECT_ID(N'[dbo].[ShoppingCartAddOns]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ShoppingCartAddOns];
GO
IF OBJECT_ID(N'[dbo].[ShoppingCarts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ShoppingCarts];
GO
IF OBJECT_ID(N'[dbo].[States]', 'U') IS NOT NULL
    DROP TABLE [dbo].[States];
GO
IF OBJECT_ID(N'[dbo].[VendorPayments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VendorPayments];
GO
IF OBJECT_ID(N'[dbo].[Vendors]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Vendors];
GO
IF OBJECT_ID(N'[localprepdbModelStoreContainer].[database_firewall_rules]', 'U') IS NOT NULL
    DROP TABLE [localprepdbModelStoreContainer].[database_firewall_rules];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'C__MigrationHistory'
CREATE TABLE [dbo].[C__MigrationHistory] (
    [MigrationId] nvarchar(150)  NOT NULL,
    [ContextKey] nvarchar(300)  NOT NULL,
    [Model] varbinary(max)  NOT NULL,
    [ProductVersion] nvarchar(32)  NOT NULL
);
GO

-- Creating table 'AspNetRoles'
CREATE TABLE [dbo].[AspNetRoles] (
    [Id] nvarchar(128)  NOT NULL,
    [Name] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'AspNetUserClaims'
CREATE TABLE [dbo].[AspNetUserClaims] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(128)  NOT NULL,
    [ClaimType] nvarchar(max)  NULL,
    [ClaimValue] nvarchar(max)  NULL
);
GO

-- Creating table 'AspNetUserLogins'
CREATE TABLE [dbo].[AspNetUserLogins] (
    [LoginProvider] nvarchar(128)  NOT NULL,
    [ProviderKey] nvarchar(128)  NOT NULL,
    [UserId] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'AspNetUsers'
CREATE TABLE [dbo].[AspNetUsers] (
    [Id] nvarchar(128)  NOT NULL,
    [FirstName] nvarchar(max)  NULL,
    [LastName] nvarchar(max)  NULL,
    [Address1] nvarchar(max)  NULL,
    [Address2] nvarchar(max)  NULL,
    [City] nvarchar(max)  NULL,
    [StateId] int  NOT NULL,
    [Zip] nvarchar(max)  NULL,
    [Website] nvarchar(max)  NULL,
    [CuisinesSelected] nvarchar(max)  NULL,
    [DietsSelected] nvarchar(max)  NULL,
    [CookingStyle] nvarchar(max)  NULL,
    [ProfilePic] varbinary(max)  NULL,
    [Email] nvarchar(256)  NULL,
    [EmailConfirmed] bit  NOT NULL,
    [PasswordHash] nvarchar(max)  NULL,
    [SecurityStamp] nvarchar(max)  NULL,
    [PhoneNumber] nvarchar(max)  NULL,
    [PhoneNumberConfirmed] bit  NOT NULL,
    [TwoFactorEnabled] bit  NOT NULL,
    [LockoutEndDateUtc] datetime  NULL,
    [LockoutEnabled] bit  NOT NULL,
    [AccessFailedCount] int  NOT NULL,
    [UserName] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'Contacts'
CREATE TABLE [dbo].[Contacts] (
    [ContactId] int IDENTITY(1,1) NOT NULL,
    [ContactName] varchar(50)  NOT NULL,
    [EmailAddress] varchar(50)  NOT NULL,
    [PhoneNumber] varchar(50)  NULL,
    [ContactText] nvarchar(1000)  NULL,
    [SubmitDt] datetime  NOT NULL
);
GO

-- Creating table 'Countries'
CREATE TABLE [dbo].[Countries] (
    [CountryId] int IDENTITY(1,1) NOT NULL,
    [CountryName] varchar(50)  NOT NULL,
    [CountryNameLong] varchar(50)  NOT NULL
);
GO

-- Creating table 'Cuisines'
CREATE TABLE [dbo].[Cuisines] (
    [CuisineId] int IDENTITY(1,1) NOT NULL,
    [CuisineName] varchar(50)  NOT NULL,
    [ImgSrc] varchar(256)  NULL,
    [IsActive] bit  NOT NULL,
    [CreateDt] datetime  NOT NULL
);
GO

-- Creating table 'Diets'
CREATE TABLE [dbo].[Diets] (
    [DietId] int IDENTITY(1,1) NOT NULL,
    [DietShortName] varchar(20)  NOT NULL,
    [DietLongName] varchar(120)  NULL,
    [DietDescription] varchar(4000)  NULL,
    [ImgSrc] varchar(256)  NULL,
    [IsActive] bit  NOT NULL,
    [CreateDt] datetime  NOT NULL
);
GO

-- Creating table 'MealAddOns'
CREATE TABLE [dbo].[MealAddOns] (
    [MealAddOnId] int IDENTITY(1,1) NOT NULL,
    [MealId] int  NOT NULL,
    [MealAddOnName] varchar(50)  NOT NULL,
    [MealAddOnPrice] decimal(9,2)  NOT NULL
);
GO

-- Creating table 'MealIngredients'
CREATE TABLE [dbo].[MealIngredients] (
    [MealIngredientId] int IDENTITY(1,1) NOT NULL,
    [MealId] int  NOT NULL,
    [IngredientName] varchar(100)  NOT NULL
);
GO

-- Creating table 'MealPics'
CREATE TABLE [dbo].[MealPics] (
    [MealPicId] int IDENTITY(1,1) NOT NULL,
    [MealId] int  NOT NULL,
    [ImgSrc] varchar(256)  NOT NULL,
    [BriefDescription] varchar(50)  NOT NULL,
    [UploadDt] datetime  NOT NULL
);
GO

-- Creating table 'MealRatings'
CREATE TABLE [dbo].[MealRatings] (
    [MealRatingId] int IDENTITY(1,1) NOT NULL,
    [MealId] int  NOT NULL,
    [UserId] nvarchar(128)  NOT NULL,
    [StarRating] int  NOT NULL,
    [RatingComments] nvarchar(4000)  NULL,
    [IsActive] bit  NOT NULL,
    [CreateDt] datetime  NOT NULL
);
GO

-- Creating table 'Meals'
CREATE TABLE [dbo].[Meals] (
    [MealId] int IDENTITY(1,1) NOT NULL,
    [VendorId] int  NOT NULL,
    [MealName] varchar(100)  NOT NULL,
    [HeatingInstructions] varchar(4000)  NULL,
    [MealPrice] decimal(9,2)  NOT NULL,
    [CuisineId] int  NOT NULL,
    [DietId] int  NULL,
    [SubmitDt] datetime  NOT NULL,
    [UpdateDt] datetime  NULL,
    [IsActive] bit  NOT NULL,
    [MealDescription] varchar(4000)  NULL,
    [Servings] varchar(50)  NULL,
    [CaloriesServing] varchar(50)  NULL,
    [Calories] varchar(50)  NULL,
    [Fat] varchar(50)  NULL,
    [Protein] varchar(50)  NULL,
    [Sugar] varchar(50)  NULL,
    [Sodium] varchar(50)  NULL,
    [Cholesterol] varchar(50)  NULL,
    [TotalCarb] varchar(50)  NULL
);
GO

-- Creating table 'MealTransactionDetailAddOns'
CREATE TABLE [dbo].[MealTransactionDetailAddOns] (
    [MealTransactionDetailAddOnId] int IDENTITY(1,1) NOT NULL,
    [MealTransactionDetailId] int  NOT NULL,
    [MealAddOnId] int  NOT NULL,
    [Price] decimal(9,2)  NOT NULL
);
GO

-- Creating table 'MealTransactionDetails'
CREATE TABLE [dbo].[MealTransactionDetails] (
    [MealTransactionDetailId] int IDENTITY(1,1) NOT NULL,
    [MealTransactionId] int  NOT NULL,
    [MealId] int  NOT NULL,
    [MealQty] int  NOT NULL,
    [Price] decimal(9,2)  NOT NULL,
    [PickupDelivery] varchar(20)  NOT NULL,
    [PickupDeliveryDt] datetime  NOT NULL,
    [RemoveIngredients] varchar(2000)  NULL
);
GO

-- Creating table 'MealTransactions'
CREATE TABLE [dbo].[MealTransactions] (
    [MealTransactionId] int IDENTITY(1,1) NOT NULL,
    [Price] decimal(9,2)  NOT NULL,
    [UserId] nvarchar(128)  NOT NULL,
    [CreateDt] datetime  NOT NULL,
    [TransactionId] varchar(50)  NULL,
    [ResponseCode] varchar(20)  NULL,
    [ResponseDescription] varchar(250)  NULL,
    [AuthCode] varchar(100)  NULL
);
GO

-- Creating table 'ShoppingCartAddOns'
CREATE TABLE [dbo].[ShoppingCartAddOns] (
    [CartAddOnId] int IDENTITY(1,1) NOT NULL,
    [CartId] int  NOT NULL,
    [MealAddOnId] int  NOT NULL,
    [Price] decimal(9,2)  NOT NULL
);
GO

-- Creating table 'ShoppingCarts'
CREATE TABLE [dbo].[ShoppingCarts] (
    [CartId] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(256)  NOT NULL,
    [MealId] int  NOT NULL,
    [Qty] int  NOT NULL,
    [Price] decimal(9,2)  NOT NULL,
    [PickupDelivery] varchar(20)  NOT NULL,
    [PickupDeliveryDt] datetime  NULL,
    [DateAdded] datetime  NOT NULL,
    [RemoveIngredients] varchar(2000)  NULL
);
GO

-- Creating table 'States'
CREATE TABLE [dbo].[States] (
    [StateId] int IDENTITY(1,1) NOT NULL,
    [StateShortName] varchar(50)  NOT NULL,
    [StateLongName] varchar(50)  NOT NULL,
    [CountryId] int  NOT NULL
);
GO

-- Creating table 'VendorPayments'
CREATE TABLE [dbo].[VendorPayments] (
    [VendorPaymentId] int IDENTITY(1,1) NOT NULL,
    [VendorId] int  NOT NULL,
    [PlanName] varchar(20)  NOT NULL,
    [Price] decimal(9,2)  NOT NULL,
    [ExpirationDate] datetime  NOT NULL,
    [TransactionId] varchar(50)  NULL,
    [ResponseCode] varchar(20)  NULL,
    [ResponseDescription] varchar(250)  NULL,
    [AuthCode] varchar(100)  NULL
);
GO

-- Creating table 'Vendors'
CREATE TABLE [dbo].[Vendors] (
    [VendorId] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(128)  NOT NULL,
    [VendorName] varchar(50)  NOT NULL,
    [CompanyName] varchar(50)  NULL,
    [Address1] varchar(80)  NOT NULL,
    [Address2] varchar(50)  NULL,
    [City] varchar(50)  NOT NULL,
    [StateId] int  NOT NULL,
    [Zip] varchar(20)  NOT NULL,
    [PhoneNumber] varchar(16)  NOT NULL,
    [EmailAddress] varchar(100)  NOT NULL,
    [VendorDescription] varchar(2500)  NULL,
    [DeliveryAvailable] bit  NOT NULL,
    [PickupAvailable] bit  NOT NULL,
    [Latitude] decimal(9,6)  NOT NULL,
    [Longitude] decimal(9,6)  NOT NULL,
    [PlaceId] nvarchar(500)  NOT NULL,
    [FormattedAddress] varchar(500)  NOT NULL,
    [ImgSrc] varchar(256)  NULL,
    [IsApproved] bit  NOT NULL,
    [ApprovedById] nvarchar(128)  NULL,
    [SubmitDt] datetime  NOT NULL,
    [LicensedInState] bit  NOT NULL,
    [LicenseNo] varchar(50)  NULL,
    [AboutYourself] varchar(2500)  NULL,
    [FavoriteThingsToCook] varchar(2500)  NULL
);
GO

-- Creating table 'database_firewall_rules'
CREATE TABLE [dbo].[database_firewall_rules] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(128)  NOT NULL,
    [start_ip_address] varchar(45)  NOT NULL,
    [end_ip_address] varchar(45)  NOT NULL,
    [create_date] datetime  NOT NULL,
    [modify_date] datetime  NOT NULL
);
GO

-- Creating table 'AspNetUserRoles'
CREATE TABLE [dbo].[AspNetUserRoles] (
    [AspNetRoles_Id] nvarchar(128)  NOT NULL,
    [AspNetUsers_Id] nvarchar(128)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [MigrationId], [ContextKey] in table 'C__MigrationHistory'
ALTER TABLE [dbo].[C__MigrationHistory]
ADD CONSTRAINT [PK_C__MigrationHistory]
    PRIMARY KEY CLUSTERED ([MigrationId], [ContextKey] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetRoles'
ALTER TABLE [dbo].[AspNetRoles]
ADD CONSTRAINT [PK_AspNetRoles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [PK_AspNetUserClaims]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [LoginProvider], [ProviderKey], [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [PK_AspNetUserLogins]
    PRIMARY KEY CLUSTERED ([LoginProvider], [ProviderKey], [UserId] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUsers'
ALTER TABLE [dbo].[AspNetUsers]
ADD CONSTRAINT [PK_AspNetUsers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [ContactId] in table 'Contacts'
ALTER TABLE [dbo].[Contacts]
ADD CONSTRAINT [PK_Contacts]
    PRIMARY KEY CLUSTERED ([ContactId] ASC);
GO

-- Creating primary key on [CountryId] in table 'Countries'
ALTER TABLE [dbo].[Countries]
ADD CONSTRAINT [PK_Countries]
    PRIMARY KEY CLUSTERED ([CountryId] ASC);
GO

-- Creating primary key on [CuisineId] in table 'Cuisines'
ALTER TABLE [dbo].[Cuisines]
ADD CONSTRAINT [PK_Cuisines]
    PRIMARY KEY CLUSTERED ([CuisineId] ASC);
GO

-- Creating primary key on [DietId] in table 'Diets'
ALTER TABLE [dbo].[Diets]
ADD CONSTRAINT [PK_Diets]
    PRIMARY KEY CLUSTERED ([DietId] ASC);
GO

-- Creating primary key on [MealAddOnId] in table 'MealAddOns'
ALTER TABLE [dbo].[MealAddOns]
ADD CONSTRAINT [PK_MealAddOns]
    PRIMARY KEY CLUSTERED ([MealAddOnId] ASC);
GO

-- Creating primary key on [MealIngredientId] in table 'MealIngredients'
ALTER TABLE [dbo].[MealIngredients]
ADD CONSTRAINT [PK_MealIngredients]
    PRIMARY KEY CLUSTERED ([MealIngredientId] ASC);
GO

-- Creating primary key on [MealPicId] in table 'MealPics'
ALTER TABLE [dbo].[MealPics]
ADD CONSTRAINT [PK_MealPics]
    PRIMARY KEY CLUSTERED ([MealPicId] ASC);
GO

-- Creating primary key on [MealRatingId] in table 'MealRatings'
ALTER TABLE [dbo].[MealRatings]
ADD CONSTRAINT [PK_MealRatings]
    PRIMARY KEY CLUSTERED ([MealRatingId] ASC);
GO

-- Creating primary key on [MealId] in table 'Meals'
ALTER TABLE [dbo].[Meals]
ADD CONSTRAINT [PK_Meals]
    PRIMARY KEY CLUSTERED ([MealId] ASC);
GO

-- Creating primary key on [MealTransactionDetailAddOnId] in table 'MealTransactionDetailAddOns'
ALTER TABLE [dbo].[MealTransactionDetailAddOns]
ADD CONSTRAINT [PK_MealTransactionDetailAddOns]
    PRIMARY KEY CLUSTERED ([MealTransactionDetailAddOnId] ASC);
GO

-- Creating primary key on [MealTransactionDetailId] in table 'MealTransactionDetails'
ALTER TABLE [dbo].[MealTransactionDetails]
ADD CONSTRAINT [PK_MealTransactionDetails]
    PRIMARY KEY CLUSTERED ([MealTransactionDetailId] ASC);
GO

-- Creating primary key on [MealTransactionId] in table 'MealTransactions'
ALTER TABLE [dbo].[MealTransactions]
ADD CONSTRAINT [PK_MealTransactions]
    PRIMARY KEY CLUSTERED ([MealTransactionId] ASC);
GO

-- Creating primary key on [CartAddOnId] in table 'ShoppingCartAddOns'
ALTER TABLE [dbo].[ShoppingCartAddOns]
ADD CONSTRAINT [PK_ShoppingCartAddOns]
    PRIMARY KEY CLUSTERED ([CartAddOnId] ASC);
GO

-- Creating primary key on [CartId] in table 'ShoppingCarts'
ALTER TABLE [dbo].[ShoppingCarts]
ADD CONSTRAINT [PK_ShoppingCarts]
    PRIMARY KEY CLUSTERED ([CartId] ASC);
GO

-- Creating primary key on [StateId] in table 'States'
ALTER TABLE [dbo].[States]
ADD CONSTRAINT [PK_States]
    PRIMARY KEY CLUSTERED ([StateId] ASC);
GO

-- Creating primary key on [VendorPaymentId] in table 'VendorPayments'
ALTER TABLE [dbo].[VendorPayments]
ADD CONSTRAINT [PK_VendorPayments]
    PRIMARY KEY CLUSTERED ([VendorPaymentId] ASC);
GO

-- Creating primary key on [VendorId] in table 'Vendors'
ALTER TABLE [dbo].[Vendors]
ADD CONSTRAINT [PK_Vendors]
    PRIMARY KEY CLUSTERED ([VendorId] ASC);
GO

-- Creating primary key on [id], [name], [start_ip_address], [end_ip_address], [create_date], [modify_date] in table 'database_firewall_rules'
ALTER TABLE [dbo].[database_firewall_rules]
ADD CONSTRAINT [PK_database_firewall_rules]
    PRIMARY KEY CLUSTERED ([id], [name], [start_ip_address], [end_ip_address], [create_date], [modify_date] ASC);
GO

-- Creating primary key on [AspNetRoles_Id], [AspNetUsers_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [PK_AspNetUserRoles]
    PRIMARY KEY CLUSTERED ([AspNetRoles_Id], [AspNetUsers_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserId] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserClaims]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserLogins]
    ([UserId]);
GO

-- Creating foreign key on [CountryId] in table 'States'
ALTER TABLE [dbo].[States]
ADD CONSTRAINT [FK_State_Country]
    FOREIGN KEY ([CountryId])
    REFERENCES [dbo].[Countries]
        ([CountryId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_State_Country'
CREATE INDEX [IX_FK_State_Country]
ON [dbo].[States]
    ([CountryId]);
GO

-- Creating foreign key on [CuisineId] in table 'Meals'
ALTER TABLE [dbo].[Meals]
ADD CONSTRAINT [FK_Meal_Cuisine]
    FOREIGN KEY ([CuisineId])
    REFERENCES [dbo].[Cuisines]
        ([CuisineId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Meal_Cuisine'
CREATE INDEX [IX_FK_Meal_Cuisine]
ON [dbo].[Meals]
    ([CuisineId]);
GO

-- Creating foreign key on [DietId] in table 'Meals'
ALTER TABLE [dbo].[Meals]
ADD CONSTRAINT [FK_Meal_Diet]
    FOREIGN KEY ([DietId])
    REFERENCES [dbo].[Diets]
        ([DietId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Meal_Diet'
CREATE INDEX [IX_FK_Meal_Diet]
ON [dbo].[Meals]
    ([DietId]);
GO

-- Creating foreign key on [MealId] in table 'MealAddOns'
ALTER TABLE [dbo].[MealAddOns]
ADD CONSTRAINT [FK_MealAddOn_Meal]
    FOREIGN KEY ([MealId])
    REFERENCES [dbo].[Meals]
        ([MealId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MealAddOn_Meal'
CREATE INDEX [IX_FK_MealAddOn_Meal]
ON [dbo].[MealAddOns]
    ([MealId]);
GO

-- Creating foreign key on [MealAddOnId] in table 'MealTransactionDetailAddOns'
ALTER TABLE [dbo].[MealTransactionDetailAddOns]
ADD CONSTRAINT [FK_MealTransactionDetailAddOn_MealAddOn]
    FOREIGN KEY ([MealAddOnId])
    REFERENCES [dbo].[MealAddOns]
        ([MealAddOnId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MealTransactionDetailAddOn_MealAddOn'
CREATE INDEX [IX_FK_MealTransactionDetailAddOn_MealAddOn]
ON [dbo].[MealTransactionDetailAddOns]
    ([MealAddOnId]);
GO

-- Creating foreign key on [MealAddOnId] in table 'ShoppingCartAddOns'
ALTER TABLE [dbo].[ShoppingCartAddOns]
ADD CONSTRAINT [FK_ShoppingCartAddOn_MealAddOn]
    FOREIGN KEY ([MealAddOnId])
    REFERENCES [dbo].[MealAddOns]
        ([MealAddOnId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ShoppingCartAddOn_MealAddOn'
CREATE INDEX [IX_FK_ShoppingCartAddOn_MealAddOn]
ON [dbo].[ShoppingCartAddOns]
    ([MealAddOnId]);
GO

-- Creating foreign key on [MealId] in table 'MealIngredients'
ALTER TABLE [dbo].[MealIngredients]
ADD CONSTRAINT [FK_MealIngredient_Meal]
    FOREIGN KEY ([MealId])
    REFERENCES [dbo].[Meals]
        ([MealId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MealIngredient_Meal'
CREATE INDEX [IX_FK_MealIngredient_Meal]
ON [dbo].[MealIngredients]
    ([MealId]);
GO

-- Creating foreign key on [MealId] in table 'MealPics'
ALTER TABLE [dbo].[MealPics]
ADD CONSTRAINT [FK_MealPic_Meal]
    FOREIGN KEY ([MealId])
    REFERENCES [dbo].[Meals]
        ([MealId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MealPic_Meal'
CREATE INDEX [IX_FK_MealPic_Meal]
ON [dbo].[MealPics]
    ([MealId]);
GO

-- Creating foreign key on [MealId] in table 'MealRatings'
ALTER TABLE [dbo].[MealRatings]
ADD CONSTRAINT [FK_MealRating_Meal]
    FOREIGN KEY ([MealId])
    REFERENCES [dbo].[Meals]
        ([MealId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MealRating_Meal'
CREATE INDEX [IX_FK_MealRating_Meal]
ON [dbo].[MealRatings]
    ([MealId]);
GO

-- Creating foreign key on [VendorId] in table 'Meals'
ALTER TABLE [dbo].[Meals]
ADD CONSTRAINT [FK_Meal_Vendor]
    FOREIGN KEY ([VendorId])
    REFERENCES [dbo].[Vendors]
        ([VendorId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Meal_Vendor'
CREATE INDEX [IX_FK_Meal_Vendor]
ON [dbo].[Meals]
    ([VendorId]);
GO

-- Creating foreign key on [MealId] in table 'MealTransactionDetails'
ALTER TABLE [dbo].[MealTransactionDetails]
ADD CONSTRAINT [FK_MealTransactionDetail_MealId]
    FOREIGN KEY ([MealId])
    REFERENCES [dbo].[Meals]
        ([MealId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MealTransactionDetail_MealId'
CREATE INDEX [IX_FK_MealTransactionDetail_MealId]
ON [dbo].[MealTransactionDetails]
    ([MealId]);
GO

-- Creating foreign key on [MealId] in table 'ShoppingCarts'
ALTER TABLE [dbo].[ShoppingCarts]
ADD CONSTRAINT [FK_ShoppingCart_Meal]
    FOREIGN KEY ([MealId])
    REFERENCES [dbo].[Meals]
        ([MealId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ShoppingCart_Meal'
CREATE INDEX [IX_FK_ShoppingCart_Meal]
ON [dbo].[ShoppingCarts]
    ([MealId]);
GO

-- Creating foreign key on [MealTransactionDetailId] in table 'MealTransactionDetailAddOns'
ALTER TABLE [dbo].[MealTransactionDetailAddOns]
ADD CONSTRAINT [FK_MealTransactionDetailAddOn_MealTransactionDetail]
    FOREIGN KEY ([MealTransactionDetailId])
    REFERENCES [dbo].[MealTransactionDetails]
        ([MealTransactionDetailId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MealTransactionDetailAddOn_MealTransactionDetail'
CREATE INDEX [IX_FK_MealTransactionDetailAddOn_MealTransactionDetail]
ON [dbo].[MealTransactionDetailAddOns]
    ([MealTransactionDetailId]);
GO

-- Creating foreign key on [MealTransactionId] in table 'MealTransactionDetails'
ALTER TABLE [dbo].[MealTransactionDetails]
ADD CONSTRAINT [FK_MealTransaction_Id]
    FOREIGN KEY ([MealTransactionId])
    REFERENCES [dbo].[MealTransactions]
        ([MealTransactionId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MealTransaction_Id'
CREATE INDEX [IX_FK_MealTransaction_Id]
ON [dbo].[MealTransactionDetails]
    ([MealTransactionId]);
GO

-- Creating foreign key on [CartId] in table 'ShoppingCartAddOns'
ALTER TABLE [dbo].[ShoppingCartAddOns]
ADD CONSTRAINT [FK_ShoppingCartAddOn_ShoppingCart]
    FOREIGN KEY ([CartId])
    REFERENCES [dbo].[ShoppingCarts]
        ([CartId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ShoppingCartAddOn_ShoppingCart'
CREATE INDEX [IX_FK_ShoppingCartAddOn_ShoppingCart]
ON [dbo].[ShoppingCartAddOns]
    ([CartId]);
GO

-- Creating foreign key on [StateId] in table 'Vendors'
ALTER TABLE [dbo].[Vendors]
ADD CONSTRAINT [FK_Vendor_State]
    FOREIGN KEY ([StateId])
    REFERENCES [dbo].[States]
        ([StateId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Vendor_State'
CREATE INDEX [IX_FK_Vendor_State]
ON [dbo].[Vendors]
    ([StateId]);
GO

-- Creating foreign key on [VendorId] in table 'VendorPayments'
ALTER TABLE [dbo].[VendorPayments]
ADD CONSTRAINT [FK_VendorPayment_Vendor]
    FOREIGN KEY ([VendorId])
    REFERENCES [dbo].[Vendors]
        ([VendorId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VendorPayment_Vendor'
CREATE INDEX [IX_FK_VendorPayment_Vendor]
ON [dbo].[VendorPayments]
    ([VendorId]);
GO

-- Creating foreign key on [AspNetRoles_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [FK_AspNetUserRoles_AspNetRoles]
    FOREIGN KEY ([AspNetRoles_Id])
    REFERENCES [dbo].[AspNetRoles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [AspNetUsers_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [FK_AspNetUserRoles_AspNetUsers]
    FOREIGN KEY ([AspNetUsers_Id])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AspNetUserRoles_AspNetUsers'
CREATE INDEX [IX_FK_AspNetUserRoles_AspNetUsers]
ON [dbo].[AspNetUserRoles]
    ([AspNetUsers_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------