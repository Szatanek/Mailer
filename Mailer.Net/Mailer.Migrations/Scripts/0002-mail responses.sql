IF NOT EXISTS (SELECT [name] FROM sys.columns WHERE object_id = OBJECT_ID(N'[mailer].[Mails]', 'U')  AND [name] = 'Status')
BEGIN
	ALTER TABLE [mailer].[Mails]
	ADD [Status] tinyint CONSTRAINT DF_Mails_Status DEFAULT(0) NOT NULL 
END
GO

IF NOT EXISTS (SELECT [name] FROM sys.columns WHERE object_id = OBJECT_ID(N'[mailer].[Mails]', 'U')  AND [name] = 'IsHtml')
BEGIN
	ALTER TABLE [mailer].[Mails]
	ADD [IsHtml] BIT NOT NULL DEFAULT(0)
END
GO

IF NOT EXISTS (SELECT [name] FROM sys.columns WHERE object_id = OBJECT_ID(N'[mailer].[Mails]', 'U')  AND [name] = 'Timestamp')
BEGIN
	ALTER TABLE [mailer].[Mails]
	ADD [Timestamp] DateTime2(0) NOT NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[mailer].[MailResponses]') AND type in (N'U'))
BEGIN
	CREATE TABLE [mailer].[MailResponses](
		[Id] [int] NOT NULL IDENTITY(1,1),
		[MailId] [int] NOT NULL,
		[ErrorMessage] [nvarchar](1000) NOT NULL,
		[ErrorType] [nvarchar](100) NOT NULL,
		[Timestamp] [datetime2](0) NOT NULL
		CONSTRAINT [PK_MailResponses_Id] PRIMARY KEY CLUSTERED ([Id] ASC) ON [PRIMARY],
		CONSTRAINT [FK_MailResponses_Mails] FOREIGN KEY([MailId]) REFERENCES [mailer].[Mails]([Id])
	)
END
GO
