USE BalloonShop

GO

CREATE TABLE Orders(
	OrderID SERIAL NOT NULL,
	DateCreated DATE NOT NULL CONSTRAINT DF_Orders_DateCreated  DEFAULT CURRENT_DATE,
	DateShipped DATE NULL,
	Verified BIT NOT NULL CONSTRAINT DF_Orders_Verified DEFAULT (('0'::bit)),
	Completed BIT NOT NULL CONSTRAINT DF_Orders_Completed DEFAULT (('0'::bit)),
	Canceled BIT NOT NULL CONSTRAINT DF_Orders_Canceled DEFAULT (('0'::bit)),
	Comments VARCHAR(1000) NULL,
	CustomerName VARCHAR(50) NULL,
	CustomerEmail VARCHAR(50) NULL,
	ShippingAddress VARCHAR(500) NULL,
 CONSTRAINT PK_Orders PRIMARY KEY(OrderID)
);

GO

CREATE TABLE OrderDetail(
	OrderID INT NOT NULL,
	ProductID INT NOT NULL,
	ProductName VARCHAR(50) NOT NULL,
	Quantity INT NOT NULL,
	UnitCost MONEY NOT NULL,
	--Subtotal  AS (Quantity*UnitCost),
 CONSTRAINT PK_OrderDetail PRIMARY KEY (OrderID, ProductID)
);

GO

ALTER TABLE OrderDetail ADD CONSTRAINT FK_OrderDetail_Orders FOREIGN KEY(OrderID)
REFERENCES Orders (OrderID);
