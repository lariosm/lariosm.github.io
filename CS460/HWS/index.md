# Homework 5

Similar to our last assignment, we were tasked with writing a ASP.NET MVC web application, this time with a database. The goal of this assignment was to create an input form (in our case, a tenant request form) that would interact with a database.

## Links

* Assignment page can be found [here](http://www.wou.edu/~morses/classes/cs46x/assignments/HW5_1819.html)
* Code repository referencing this work can be found [here](https://github.com/mlarios1/CS460/tree/master/hw5)
* Clone repo link: [https://github.com/mlarios1/CS460.git](https://github.com/mlarios1/CS460.git)

## Step 1: Preparation
Given the nature of this assignment, there was not much new material to learn. However, I had to re-familiarize myself with SQL syntax (MySQL in particular), given I had not worked with it since June 2018. Soon after, I went on to learn about Transact-SQL (T-SQL) since we're to create a table that Visual Studio's built-in LocalDB can work with.

## Step 2: Writing an SQL table
At this point, I haven't fully mastered SQL, but I know enough to create a single table needed for this assignment. To begin, I started by writing an SQL script (saved as ```up.sql```) in Visual Studio's table designer (mostly to check syntax errors) that constructs a table, containing the necessary keys to store in information from the tenant request form to the database and populated it with a few sample entries.

```SQL
-- Tenants table
CREATE TABLE [dbo].[Tenants]
(
  [ID]            INT IDENTITY (1,1)  NOT NULL,
  [FirstName]     NVARCHAR(64)        NOT NULL,
  [LastName]      NVARCHAR(100)       NOT NULL,
  [PhoneNumber]   NVARCHAR(12)        NOT NULL, 
  [ApartmentName] NVARCHAR(100)       NOT NULL, 
  [UnitNumber]    INT                 NOT NULL, 
  [Description]   NVARCHAR(MAX)       NOT NULL, 
  [Checkbox]      BIT                 NOT NULL, 
  [Received]      DATETIME            NOT NULL DEFAULT CURRENT_TIMESTAMP, 
  CONSTRAINT [PK_dbo.Tenants] PRIMARY KEY CLUSTERED ([ID] ASC)
);

INSERT INTO [dbo].[Tenants] (FirstName, LastName, PhoneNumber, ApartmentName, UnitNumber, Description, Checkbox, Received) VALUES
	('Jim','Johnson','503-999-8888', 'Alpha Apartments', 11, 'Stovetop is in need of repair. One of the burners recently stopped working and we don''t know why that is.', 1, '10-18-2018' ),
	('John','Schwartz','503-777-5555', 'Woodland Apartments', 7, 'Could you send someone to look at the dryer in the laundry room? I think the heating element might be broken.', 1, '10-22-2018' ),
	('Kate','Ocean','503-444-3333', 'Vista Apartments', 4, 'Can you send an exterminator out to the complex right away? The mice infestation is quickly getting out of hand.', 1, '10-25-2018' ),
	('Suzy','Collins','971-444-8888', 'Woodland Apartments', 16, 'Hi, can we get a seesaw and another swing set for the playground. Thanks.', 1, '09-28-2018' ),
	('John','Skeeter','971-222-5555', 'Alpha Apartments', 3, 'Mind talking to the folks upstairs? I''m getting tired of having to ask them to turn down their music after curfew.', 1, '10-21-2018' )
GO
```

In addition, I also wrote another SQL script (saved as ```down.sql```) that would clear the table above, if needed to.
```SQL
-- Take the Tenants table down
DROP TABLE [dbo].[Tenants];
```
