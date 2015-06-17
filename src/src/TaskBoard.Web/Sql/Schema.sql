USE [TaskBoard_db]
GO
CREATE TABLE [dbo].[Boards](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](1024) NULL,
 CONSTRAINT [PK_Boards] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)
)
GO
/****** Object:  Table [dbo].[IssueStates]    Script Date: 06/07/2015 21:39:49 ******/
CREATE TABLE [dbo].[IssueStates](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[BoardId] [int] NOT NULL,
	[Order] [int] NOT NULL
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)
)
GO
/****** Object:  Table [dbo].[Issues]    Script Date: 06/07/2015 21:39:49 ******/
CREATE TABLE [dbo].[Issues](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](1024) NOT NULL,
	[BoardId] [int] NOT NULL,
	[StateId] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Issues] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)
)
GO
/****** Object:  ForeignKey [FK_Issue_Board]    Script Date: 06/07/2015 21:39:49 ******/
ALTER TABLE [dbo].[Issues]  WITH CHECK ADD  CONSTRAINT [FK_Issue_Board] FOREIGN KEY([BoardId])
REFERENCES [dbo].[Boards] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Issues] CHECK CONSTRAINT [FK_Issue_Board]
GO
/****** Object:  ForeignKey [FK_Issue_State]    Script Date: 06/07/2015 21:39:49 ******/
ALTER TABLE [dbo].[Issues]  WITH CHECK ADD  CONSTRAINT [FK_Issue_State] FOREIGN KEY([StateId])
REFERENCES [dbo].[IssueStates] ([Id])
GO
ALTER TABLE [dbo].[Issues] CHECK CONSTRAINT [FK_Issue_State]
GO
/****** Object:  ForeignKey [FK_State_Board]    Script Date: 06/07/2015 21:39:49 ******/
ALTER TABLE [dbo].[IssueStates]  WITH CHECK ADD  CONSTRAINT [FK_State_Board] FOREIGN KEY([BoardId])
REFERENCES [dbo].[Boards] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[IssueStates] CHECK CONSTRAINT [FK_State_Board]
GO
