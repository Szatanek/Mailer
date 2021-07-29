IF NOT EXISTS (SELECT [name] FROM sys.columns WHERE object_id = OBJECT_ID(N'[mailer].[Mails]', 'U')  AND [name] = 'SystemId')
BEGIN
	ALTER TABLE [mailer].[Mails]
	ADD [SystemId] INT NOT NULL
END
GO
