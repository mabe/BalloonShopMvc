USE BalloonShop

GO

CREATE TABLE ShippingRegion(
	ShippingRegionID INT IDENTITY(1,1) NOT NULL,
	ShippingRegion NVARCHAR(100) NULL,
 CONSTRAINT PK_ShippingRegion_1 PRIMARY KEY CLUSTERED (ShippingRegionID ASC)
)

GO

INSERT INTO ShippingRegion (ShippingRegion)
VALUES ('Please Select'),
	   ('US / Canada'),
	   ('Europe'),
	   ('Rest of World')
	   
GO
