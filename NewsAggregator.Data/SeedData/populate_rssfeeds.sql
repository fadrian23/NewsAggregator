USE [NewsAggregator]
GO
SET IDENTITY_INSERT [dbo].[RssFeeds] ON 
GO
INSERT [dbo].[RssFeeds] ([RssFeedId], [Name], [URL], [ParentFeedId]) VALUES (1, N'Polsat News', N'https://www.polsatnews.pl/rss/wszystkie.xml', NULL)
GO
INSERT [dbo].[RssFeeds] ([RssFeedId], [Name], [URL], [ParentFeedId]) VALUES (2, N'Polska', N'https://www.polsatnews.pl/rss/polska.xml', 1)
GO
INSERT [dbo].[RssFeeds] ([RssFeedId], [Name], [URL], [ParentFeedId]) VALUES (3, N'Swiat', N'https://www.polsatnews.pl/rss/swiat.xml', 1)
GO
INSERT [dbo].[RssFeeds] ([RssFeedId], [Name], [URL], [ParentFeedId]) VALUES (4, N'Wideo', N'https://www.polsatnews.pl/rss/wideo.xml', 1)
GO
INSERT [dbo].[RssFeeds] ([RssFeedId], [Name], [URL], [ParentFeedId]) VALUES (5, N'Biznes', N'https://www.polsatnews.pl/rss/biznes.xml', 1)
GO
INSERT [dbo].[RssFeeds] ([RssFeedId], [Name], [URL], [ParentFeedId]) VALUES (6, N'Technologie', N'https://www.polsatnews.pl/rss/technologie.xml', 1)
GO
INSERT [dbo].[RssFeeds] ([RssFeedId], [Name], [URL], [ParentFeedId]) VALUES (7, N'Moto', N'https://www.polsatnews.pl/rss/moto.xml', 1)
GO
INSERT [dbo].[RssFeeds] ([RssFeedId], [Name], [URL], [ParentFeedId]) VALUES (8, N'Kultura', N'https://www.polsatnews.pl/rss/kultura.xml', 1)
GO
INSERT [dbo].[RssFeeds] ([RssFeedId], [Name], [URL], [ParentFeedId]) VALUES (9, N'Sport', N'https://www.polsatnews.pl/rss/sport.xml', 1)
GO
INSERT [dbo].[RssFeeds] ([RssFeedId], [Name], [URL], [ParentFeedId]) VALUES (10, N'Czysta Polska', N'https://www.polsatnews.pl/rss/czysta-polska.xml', 1)
GO
INSERT [dbo].[RssFeeds] ([RssFeedId], [Name], [URL], [ParentFeedId]) VALUES (11, N'Tvn24', N'https://tvn24.pl/najnowsze.xml', NULL)
GO
INSERT [dbo].[RssFeeds] ([RssFeedId], [Name], [URL], [ParentFeedId]) VALUES (12, N'Onet', N'https://wiadomosci.onet.pl/.feed', NULL)
GO
INSERT [dbo].[RssFeeds] ([RssFeedId], [Name], [URL], [ParentFeedId]) VALUES (13, N'Wirtualna Polska', N'https://wiadomosci.wp.pl/RSS.XML', NULL)
GO
INSERT [dbo].[RssFeeds] ([RssFeedId], [Name], [URL], [ParentFeedId]) VALUES (14, N'Interia', N'https://wydarzenia.interia.pl/feed', NULL)
GO
INSERT [dbo].[RssFeeds] ([RssFeedId], [Name], [URL], [ParentFeedId]) VALUES (15, N'Polska', N'https://wydarzenia.interia.pl/polska/feed', 14)
GO
INSERT [dbo].[RssFeeds] ([RssFeedId], [Name], [URL], [ParentFeedId]) VALUES (16, N'Wywiady', N'https://wydarzenia.interia.pl/wywiady/feed', 14)
GO
INSERT [dbo].[RssFeeds] ([RssFeedId], [Name], [URL], [ParentFeedId]) VALUES (17, N'Swiat', N'https://wydarzenia.interia.pl/swiat/feed', 14)
GO
INSERT [dbo].[RssFeeds] ([RssFeedId], [Name], [URL], [ParentFeedId]) VALUES (18, N'Zagranica', N'https://wydarzenia.interia.pl/zagranica/feed', 14)
GO
INSERT [dbo].[RssFeeds] ([RssFeedId], [Name], [URL], [ParentFeedId]) VALUES (19, N'Kultura', N'https://wydarzenia.interia.pl/kultura/feed', 14)
GO
INSERT [dbo].[RssFeeds] ([RssFeedId], [Name], [URL], [ParentFeedId]) VALUES (20, N'Historia', N'https://wydarzenia.interia.pl/historia/feed', 14)
GO
INSERT [dbo].[RssFeeds] ([RssFeedId], [Name], [URL], [ParentFeedId]) VALUES (21, N'Nauka', N'https://wydarzenia.interia.pl/nauka/feed', 14)
GO
INSERT [dbo].[RssFeeds] ([RssFeedId], [Name], [URL], [ParentFeedId]) VALUES (22, N'Religia', N'https://wydarzenia.interia.pl/historia/feed', 14)
GO
INSERT [dbo].[RssFeeds] ([RssFeedId], [Name], [URL], [ParentFeedId]) VALUES (23, N'Ciekawostki', N'https://wydarzenia.interia.pl/ciekawostki/feed', 14)
GO
INSERT [dbo].[RssFeeds] ([RssFeedId], [Name], [URL], [ParentFeedId]) VALUES (24, N'Autorzy', N'https://wydarzenia.interia.pl/autorzy/feed', 14)
GO
INSERT [dbo].[RssFeeds] ([RssFeedId], [Name], [URL], [ParentFeedId]) VALUES (25, N'Opinie', N'https://wydarzenia.interia.pl/opinie/feed', 14)
GO
INSERT [dbo].[RssFeeds] ([RssFeedId], [Name], [URL], [ParentFeedId]) VALUES (26, N'Sport', N'https://sport.interia.pl/feed', 14)
GO
INSERT [dbo].[RssFeeds] ([RssFeedId], [Name], [URL], [ParentFeedId]) VALUES (27, N'Kobieta', N'https://kobieta.interia.pl/feed', 14)
GO
INSERT [dbo].[RssFeeds] ([RssFeedId], [Name], [URL], [ParentFeedId]) VALUES (28, N'Menway', N'https://menway.interia.pl/feed', 14)
GO
INSERT [dbo].[RssFeeds] ([RssFeedId], [Name], [URL], [ParentFeedId]) VALUES (29, N'Gry', N'https://gry.interia.pl/feed', 14)
GO
INSERT [dbo].[RssFeeds] ([RssFeedId], [Name], [URL], [ParentFeedId]) VALUES (30, N'Nowe Technologie', N'https://nt.interia.pl/feed', 14)
GO
SET IDENTITY_INSERT [dbo].[RssFeeds] OFF
GO
