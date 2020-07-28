# Products_Site

1. If you are running the Project Please run it through controller which is Home Controller
2. For the Queries of the DB As please run the below commands

CREATE TABLE [dbo].[RegisterUser](
	[Id] [int] IDENTITY(1,1) NOT NULL primary key,
	[FirstName] [varchar](100) NOT NULL,
	[LastName] [varchar](100) NOT NULL,
	[Email] [varchar](100) NOT NULL unique,
	[Password] [varchar](500) NOT NULL,
)

ALTER DATABASE [test] SET  READ_WRITE 

SET IDENTITY_INSERT [dbo].[RegisterUser] ON 

SET IDENTITY_INSERT [dbo].[RegisterUser] OFF

-----------------------------------------------------------------------------------------
/* Creation of Products Table */
CREATE TABLE [dbo].[ProductDetails](
	[Product_id] [int] IDENTITY(10000,1) NOT NULL primary key,
	[Email] [varchar](100) NOT NULL,
	[Product_type] [varchar](100) NOT NULL,
	[Product_name] [varchar](100) NOT NULL,
	[Product_price] [decimal] NOT NULL,
	[Product_weight] [float] NOT NULL,
	[Product_description] [varchar](500) NOT NULL
)

SET IDENTITY_INSERT [dbo].[ProductDetails] ON 

SET IDENTITY_INSERT [dbo].[ProductDetails] OFF




3. Please check the IP Address in the Config file.
4. Please register if you are new user now and they you can login and when login you can see the list of products button.
5. Click the list of products to see the products as now no data will be present so please add some products before viewing the list of products
