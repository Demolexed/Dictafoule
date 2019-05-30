CREATE TABLE [dbo].[user]
(
	[id_user]    INT            IDENTITY (1, 1) NOT NULL,
    [email] NVARCHAR(MAX) NULL,
    [password] NVARCHAR(MAX) NULL,
	[guid] VARCHAR(MAX) NULL, 
    [right] INT NULL, 
    CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED ([id_user] ASC)
)