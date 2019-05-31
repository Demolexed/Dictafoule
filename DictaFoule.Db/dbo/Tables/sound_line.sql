CREATE TABLE [dbo].[sound_line]
(
    [id]               INT            IDENTITY (1, 1) NOT NULL,
    [id_project] INT NOT NULL, 
    [name] VARCHAR(MAX) NOT NULL, 
    [uri] VARCHAR(MAX) NOT NULL,  
    [transcript] TEXT NULL, 
    [creation_date]            DATETIME       NOT NULL,
    [state]                    INT            NOT NULL,
    [id_taskline] INT NULL, 
    [task_answer] VARCHAR(MAX) NULL, 
    CONSTRAINT [PK_sound_line] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_project_sound_line] FOREIGN KEY ([id_project]) REFERENCES [dbo].[project] ([id])
)