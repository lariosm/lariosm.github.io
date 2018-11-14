CREATE TABLE [dbo].[Sellers]
(
	[ID]	INT IDENTITY (1,1)	NOT NULL	PRIMARY KEY,
	[Name]	NVARCHAR(30)		NOT NULL
);

CREATE TABLE [dbo].[Buyers]
(
	[ID]	INT IDENTITY (1,1)	NOT NULL	PRIMARY KEY,
	[Name]	NVARCHAR(30)		NOT NULL
);

CREATE TABLE [dbo].[Items]
(
	[ID]			INT IDENTITY (1001,1)						NOT NULL	PRIMARY KEY,
	[Name]			NVARCHAR(100)								NOT NULL,
	[Description]	NVARCHAR(MAX)								NOT NULL,
	[Seller]		INT FOREIGN KEY REFERENCES Sellers(ID)		NOT NULL
);

CREATE TABLE [dbo].[Bids]
(
	[ID]			INT IDENTITY (1,1)							NOT NULL	PRIMARY KEY,
	[ItemID]		INT FOREIGN KEY REFERENCES Items(ID)		NOT NULL,
	[BuyerID]		INT FOREIGN KEY REFERENCES Buyers(ID)		NOT NULL,
	[Price]			NVARCHAR(20)								NOT NULL,
	[TimeStamp]		DATETIME									NOT NULL,
);

INSERT INTO [dbo].[Sellers] (Name) VALUES
	('Gayle Hardy'),
	('Lyle Banks'),
	('Pearl Greene');

INSERT INTO [dbo].[Buyers] (Name) VALUES
	('Jane Stone'),
	('Tom McMasters'),
	('Otto Vanderwall');

INSERT INTO [dbo].[Items] (Name, Description, Seller) VALUES
	('Abraham Lincoln Hammer', 'A bench mallet fashioned from a broken rail-splitting maul in 1829 and owned by Abraham Lincoln', 3),
	('Albert Einsteins Telescope', 'A brass telescope owned by Albert Einstein in Germany, circa 1927', 1),
	('Bob Dylan Love Poems', 'Five versions of an original unpublished, handwritten, love poem by Bob Dylan', 2);

INSERT INTO [dbo].[Bids] (ItemID, BuyerID, Price, TimeStamp) VALUES
	(1001, 1, 250000, '12/04/2017 09:04:22'),
	(1003, 3, 95000 ,'12/04/2017 08:44:03');