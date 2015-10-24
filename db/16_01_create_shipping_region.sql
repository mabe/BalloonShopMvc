USE BalloonShop

GO

CREATE TABLE ShippingRegion(
	ShippingRegionID SERIAL NOT NULL,
	ShippingRegion VARCHAR(100) NULL,
 CONSTRAINT PK_ShippingRegion_1 PRIMARY KEY (ShippingRegionID)
);

GO

INSERT INTO ShippingRegion (ShippingRegion)
VALUES ('Please Select'),
	   ('US / Canada'),
	   ('Europe'),
	   ('Rest of World');

GO

CREATE TABLE Account (
	AccountId UUID NOT NULL,
	Email varchar(255) NOT NULL,
	Password varchar(255) NOT NULL,
	Address1 varchar(255) NULL,
	Address2 varchar(255) NULL,
	City varchar(255) NULL,
	Country varchar(255) NULL,
	PostalCode varchar(255) NULL,
	Region varchar(255) NULL,
	DaytimePhone varchar(255) NULL,
	EveningPhone varchar(255) NULL,
	MobilePhone varchar(255) NULL,
	CreditCard varchar(255) NULL,
	ShippingRegion int NULL,
 CONSTRAINT PK_Account PRIMARY KEY ( Id ) );
