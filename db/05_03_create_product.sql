USE BalloonShop

GO

---
--- PRODUCT
---

GO

CREATE TABLE Product(
	ProductID SERIAL NOT NULL,
	Name VARCHAR(50) NOT NULL,
	Description TEXT NOT NULL,
	Price MONEY NOT NULL,
	Thumbnail VARCHAR(50) NULL,
	Image VARCHAR(50) NULL,
	PromoFront BIT NOT NULL,
	PromoDept BIT NOT NULL,
 CONSTRAINT PK_Product PRIMARY KEY (ProductID)
);


GO

CREATE TABLE ShoppingCart(
	CartID char(36) NOT NULL,
	ProductID INT NOT NULL,
	Quantity INT NOT NULL,
	DateAdded DATE NOT NULL,
 CONSTRAINT PK_ShoppingCart PRIMARY KEY (CartID, ProductID)
);

GO

ALTER TABLE ShoppingCart ADD CONSTRAINT FK_ShoppingCart_Product FOREIGN KEY(ProductID)
REFERENCES Product (ProductID);
