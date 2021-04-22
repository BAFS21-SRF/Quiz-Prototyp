Create database QuizPrototype
GO
Use QuizPrototype
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Frage](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FrageText] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[Auswahl](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[AuswahlText] [varchar](255) NOT NULL,
	[Order] [int] NOT NULL,
	[FrageId] [bigint] NOT NULL,
	[AssetId] [varchar](255) NOT NULL		
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [QuizPrototype]
GO
SET IDENTITY_INSERT [dbo].[Auswahl] ON 
GO
INSERT [dbo].[Auswahl] ([Id], [AuswahlText], [Order], [FrageId], [AssetId]) VALUES (1, N'Ente', 4, 1, N'DuckWhite')
GO
INSERT [dbo].[Auswahl] ([Id], [AuswahlText], [Order], [FrageId], [AssetId]) VALUES (2, N'Schwein', 2, 1, N'Pig')
GO
INSERT [dbo].[Auswahl] ([Id], [AuswahlText], [Order], [FrageId], [AssetId]) VALUES (3, N'Kuh', 1, 1, N'CowBIW')
GO
INSERT [dbo].[Auswahl] ([Id], [AuswahlText], [Order], [FrageId], [AssetId]) VALUES (5, N'Schaf', 3, 1, N'SheepWhite')
GO
SET IDENTITY_INSERT [dbo].[Auswahl] OFF
GO
SET IDENTITY_INSERT [dbo].[Frage] ON 
GO
INSERT [dbo].[Frage] ([Id], [FrageText]) VALUES (1, N'Ordne die Tiere nach der Grösse')
GO
SET IDENTITY_INSERT [dbo].[Frage] OFF
GO
