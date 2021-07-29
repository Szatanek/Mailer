IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'mailer')
	EXEC sys.sp_executesql N'CREATE SCHEMA [mailer] AUTHORIZATION dbo'
GO


IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[mailer].[Users]') AND type in (N'U'))
BEGIN
CREATE TABLE [mailer].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Login] [nvarchar](50) NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](150) NULL,
	[Email] [nvarchar](100) NULL
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO