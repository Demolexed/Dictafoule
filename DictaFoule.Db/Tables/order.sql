CREATE TABLE [order]
(
	[id_payment] INT            IDENTITY (1, 1) NOT NULL, 
    [id_stripe] VARCHAR(MAX) NOT NULL, 
    [amount] INT NOT NULL, 
    [id_project] INT NOT NULL, 
    [email] VARCHAR(MAX) NOT NULL,
	[name_customer] VARCHAR(MAX) NOT NULL, 
    [date] DATETIME NOT NULL,
	CONSTRAINT [FK_project_order] FOREIGN KEY ([id_project]) REFERENCES [dbo].[project]([id]),
    CONSTRAINT [PK_payment] PRIMARY KEY CLUSTERED ([id_payment] ASC)
)
