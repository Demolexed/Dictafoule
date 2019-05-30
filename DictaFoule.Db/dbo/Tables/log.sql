CREATE TABLE [dbo].[log]
(
	[id_log]       INT      IDENTITY (1, 1) NOT NULL,
	[id_log_level]       INT NOT NULL,
    [type] NVARCHAR(250) NOT NULL,
	[id_project] INT NOT NULL, 
    [message] TEXT NULL, 
    [creation_date] DATETIME NOT NULL,
    PRIMARY KEY CLUSTERED ([id_log] ASC)
)
