USE BalloonShop

GO

---
--- PRODUCT 
---

GO

CREATE TABLE Product(
	ProductID INT IDENTITY(1,1) NOT NULL,
	Name NVARCHAR(50) NOT NULL,
	Description NVARCHAR(MAX) NOT NULL,
	Price MONEY NOT NULL,
	Thumbnail NVARCHAR(50) NULL,
	Image NVARCHAR(50) NULL,
	PromoFront BIT NOT NULL,
	PromoDept BIT NOT NULL,
 CONSTRAINT PK_Product PRIMARY KEY CLUSTERED (ProductID ASC)
) 
