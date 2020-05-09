IF OBJECT_ID('Post') IS NOT NULL
	DROP TABLE [Post]
GO

IF OBJECT_ID('Author') IS NOT NULL
	DROP TABLE [Author]
GO

CREATE TABLE [Author](
	[AuthorId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY CLUSTERED,
	[FirstName] VARCHAR(50) NOT NULL,
	[LastName] VARCHAR(50) NOT NULL,
	[NickName] VARCHAR(20) NULL
)
GO

CREATE TABLE [Post](
	[PostId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY NONCLUSTERED,
	[AuthorId] INT NOT NULL FOREIGN KEY REFERENCES Author(AuthorId),
	[Title] VARCHAR(200) NOT NULL,
	[Content] VARCHAR(MAX) NOT NULL,
	[DatePublished] DATETIME NULL
)
GO

DECLARE @AuthorId INT
INSERT INTO AUTHOR([FirstName], [LastName], [NickName])VALUES
('Chris', 'Carter', 'ChrisJohn')
SELECT @AuthorId = SCOPE_IDENTITY()
INSERT INTO [Post]([PostId], [AuthorId], [Title], [Content], [DatePublished]) VALUES
(NEWID(), @AuthorId, 'This is my post', 'This is my content', GETDATE())
GO