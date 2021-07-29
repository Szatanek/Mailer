IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[mailer].[Mails]') AND type in (N'U'))
BEGIN
CREATE TABLE mailer.[Mails](
	[Id] [int] NOT NULL IDENTITY(1,1),
	[MailGuid] [uniqueidentifier] NOT NULL,
	[Topic] [nvarchar](250) NOT NULL,
	[Sender] [nvarchar](50) NOT NULL,
	[Recipient] [nvarchar](1000) NOT NULL,
	[Content] [nvarchar](max) NULL,
 CONSTRAINT [PK_Mails_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
) ON [PRIMARY],
CONSTRAINT [U_Mails_MailGuid] UNIQUE
(
	[MailGuid]
))
END
GO
