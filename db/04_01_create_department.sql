USE BalloonShop

GO

---
--- DEPARTMENT
---

CREATE TABLE Department (
	DepartmentID SERIAL NOT NULL,
	Name varchar(50) NOT NULL,
	Description varchar(1000) NULL,
	CONSTRAINT PK_Department PRIMARY KEY (DepartmentID)
);
