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


CREATE TABLE [dbo].[Game](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Guid] [varchar](255) NOT NULL,
	[AktuelleFrageId] [bigint] NOT NULL,
	[Score] [bigint] NOT NULL,
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
INSERT [dbo].[Auswahl] ([Id], [AuswahlText], [Order], [FrageId], [AssetId]) VALUES (1, N'Gans', 4, 1, N'Swan_goose')
GO
INSERT [dbo].[Auswahl] ([Id], [AuswahlText], [Order], [FrageId], [AssetId]) VALUES (2, N'Bär', 2, 1, N'Brown_bear')
GO
INSERT [dbo].[Auswahl] ([Id], [AuswahlText], [Order], [FrageId], [AssetId]) VALUES (3, N'Hai', 1, 1, N'Great_white_shark')
GO
INSERT [dbo].[Auswahl] ([Id], [AuswahlText], [Order], [FrageId], [AssetId]) VALUES (5, N'Schwein', 3, 1, N'Iron_age_pig')
GO
INSERT [dbo].[Auswahl] ([Id], [AuswahlText], [Order], [FrageId], [AssetId]) VALUES (6, N'10 Liter', 0, 2, N'Cattle_male')
GO
INSERT [dbo].[Auswahl] ([Id], [AuswahlText], [Order], [FrageId], [AssetId]) VALUES (7, N'5 Liter', 0, 2, N'Cattle_male')
GO
INSERT [dbo].[Auswahl] ([Id], [AuswahlText], [Order], [FrageId], [AssetId]) VALUES (8, N'25 Liter', 1, 2, N'Cattle_male')
GO
INSERT [dbo].[Auswahl] ([Id], [AuswahlText], [Order], [FrageId], [AssetId]) VALUES (9, N'40 Liter', 0, 2, N'Cattle_male')
GO
INSERT [dbo].[Auswahl] ([Id], [AuswahlText], [Order], [FrageId], [AssetId]) VALUES (10, N'Wolf', 3, 3, N'Wolf')
GO
INSERT [dbo].[Auswahl] ([Id], [AuswahlText], [Order], [FrageId], [AssetId]) VALUES (11, N'Hirsch', 1, 3, N'Deer_male')
GO
INSERT [dbo].[Auswahl] ([Id], [AuswahlText], [Order], [FrageId], [AssetId]) VALUES (12, N'Wildschschwein', 4, 3, N'Wild_boar')
GO
INSERT [dbo].[Auswahl] ([Id], [AuswahlText], [Order], [FrageId], [AssetId]) VALUES (13, N'Steinbock', 2, 3, N'Ibex')
GO
INSERT [dbo].[Auswahl] ([Id], [AuswahlText], [Order], [FrageId], [AssetId]) VALUES (14, N'50 Eier', 0, 4, N'Chicken')
GO
INSERT [dbo].[Auswahl] ([Id], [AuswahlText], [Order], [FrageId], [AssetId]) VALUES (15, N'180 Eier', 0, 4, N'Chicken')
GO
INSERT [dbo].[Auswahl] ([Id], [AuswahlText], [Order], [FrageId], [AssetId]) VALUES (16, N'500 Eier', 0, 4, N'Chicken')
GO
INSERT [dbo].[Auswahl] ([Id], [AuswahlText], [Order], [FrageId], [AssetId]) VALUES (17, N'280 Eier', 1, 4, N'Chicken')
GO
SET IDENTITY_INSERT [dbo].[Auswahl] OFF
GO
SET IDENTITY_INSERT [dbo].[Frage] ON 
GO
INSERT [dbo].[Frage] ([Id], [FrageText]) VALUES (1, N'Ordne die Tiere nach der Grösse')
GO
INSERT [dbo].[Frage] ([Id], [FrageText]) VALUES (2, N'Wie viel Liter Milch produziert eine Milchkuh pro Tag?')
GO
INSERT [dbo].[Frage] ([Id], [FrageText]) VALUES (3, N'Ordne die Tiere nach ihren Lebenserwartungen')
GO
INSERT [dbo].[Frage] ([Id], [FrageText]) VALUES (4, N'Wie viele Eier legt ein Huhn pro Jahr?')
GO
SET IDENTITY_INSERT [dbo].[Frage] OFF
GO