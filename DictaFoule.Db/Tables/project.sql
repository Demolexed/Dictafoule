CREATE TABLE [project]
(
	[id]               INT            IDENTITY (1, 1) NOT NULL,
    [import_sound_file_name] VARCHAR(MAX) NOT NULL, 
    [import_sound_file_uri] VARCHAR(MAX) NULL, 
	[id_user] INT NULL,
    [creation_date]            DATETIME       NOT NULL,
    [state]                    INT            NOT NULL, 
    CONSTRAINT [PK_project] PRIMARY KEY CLUSTERED ([id] ASC),
	CONSTRAINT [FK_user_guid] FOREIGN KEY ([id_user]) REFERENCES [dbo].[user]([id])
)
