USE BalloonShop

GO

---
--- CATEGORY
---

CREATE TABLE Category(
	CategoryID SERIAL NOT NULL,
	DepartmentID INT NOT NULL,
	Name VARCHAR(50) NOT NULL,
	Description VARCHAR(1000) NULL,
 	CONSTRAINT PK_Category_1 PRIMARY KEY (CategoryID)
);

ALTER TABLE Category ADD CONSTRAINT FK_Category_Department FOREIGN KEY(DepartmentID)
REFERENCES Department (DepartmentID);

GO
