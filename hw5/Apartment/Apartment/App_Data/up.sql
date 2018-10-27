-- Tenants table
CREATE TABLE [dbo].[Tenants]
(
	[ID]			INT IDENTITY (1,1)	NOT NULL,
	[FirstName]		NVARCHAR(64)		NOT NULL,
	[LastName]	    NVARCHAR(128)		NOT NULL,
	[PhoneNumber]   NVARCHAR(10)		NOT NULL, 
    [ApartmentName] NVARCHAR(100)		NOT NULL, 
    [UnitNumber]	NVARCHAR(2)			NOT NULL, 
    [Description]	NVARCHAR(500)		NOT NULL, 
    [Checkbox]		TINYINT				NOT NULL, 
    [Received]		TIMESTAMP			NOT NULL DEFAULT CURRENT_TIMESTAMP, 
    CONSTRAINT [PK_dbo.Users] PRIMARY KEY CLUSTERED ([ID] ASC)
);
