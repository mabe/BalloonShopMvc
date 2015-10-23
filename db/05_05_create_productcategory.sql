USE BalloonShop

GO

CREATE TABLE ProductCategory(
	ProductID INT NOT NULL,
	CategoryID INT NOT NULL,
 CONSTRAINT PK_ProductCategory PRIMARY KEY (ProductID, CategoryID)
);

ALTER TABLE ProductCategory ADD CONSTRAINT FK_ProductCategory_Category FOREIGN KEY(CategoryID)
REFERENCES Category (CategoryID);

ALTER TABLE ProductCategory ADD  CONSTRAINT FK_ProductCategory_Product FOREIGN KEY(ProductID)
REFERENCES Product (ProductID);
