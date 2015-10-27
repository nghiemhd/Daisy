CREATE TABLE [UrlRecord] (
    [Id] [int] NOT NULL IDENTITY,
    [EntityId] [int] NOT NULL,
    [EntityName] [varchar](100) NOT NULL,
    [Slug] [varchar](100) NOT NULL,
    [IsActive] [bit] NOT NULL DEFAULT 1,
    [LanguageId] [int] NOT NULL DEFAULT 0,
    CONSTRAINT [PK_UrlRecord] PRIMARY KEY ([Id])
)
CREATE TABLE [Language] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [nvarchar](100) NOT NULL,
    [LanguageCulture] [varchar](20) NOT NULL,
    [UniqueSeoCode] [varchar](2),
    [FlagImageFileName] [varchar](50),
    [Rtl] [bit] NOT NULL DEFAULT 0,
    [IsPublished] [bit] NOT NULL DEFAULT 0,
    [DisplayOrder] [int] NOT NULL DEFAULT 0,
    CONSTRAINT [PK_Language] PRIMARY KEY ([Id])
)

INSERT [Language]([Name], [LanguageCulture], [UniqueSeoCode], [FlagImageFileName], [Rtl], [IsPublished], [DisplayOrder])
VALUES
(N'Việt Nam', 'vi-VN', 'vi', 'vn.png', 0, 1, 0),
(N'English', 'en-US', 'en', 'us.png', 0, 0, 1)


CREATE TABLE [tmpBlogPost] (
    [Id] [int] NOT NULL IDENTITY,
    [LanguageId] [int] NOT NULL,
    [Title] [nvarchar](200) NOT NULL,
    [Highlight] [nvarchar](500) NOT NULL,
    [ImageUrl] [nvarchar](max),
    [Content] [nvarchar](max) NOT NULL,
    [IsPublished] [bit] NOT NULL DEFAULT 1,
    [Tags] [nvarchar](max),
    [MetaKeywords] [nvarchar](400),
    [MetaDescription] [nvarchar](max),
    [MetaTitle] [nvarchar](400),
    [IsDeleted] [bit] NOT NULL DEFAULT 0,
    [UpdatedDate] [datetime] NOT NULL DEFAULT GETDATE(),
    [UpdatedBy] [varchar](50) NOT NULL DEFAULT suser_name(),
    [CreatedDate] [datetime] NOT NULL DEFAULT GETDATE(),
    [CreatedBy] [varchar](50) NOT NULL DEFAULT suser_name(),
    [RowRevision] rowversion NOT NULL,
    CONSTRAINT [PK_BlogPost] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_LanguageId] ON [tmpBlogPost]([LanguageId])

ALTER TABLE [tmpBlogPost] ADD CONSTRAINT [FK_BlogPost_Language_LanguageId] FOREIGN KEY ([LanguageId]) REFERENCES [Language] ([Id])

SET IDENTITY_INSERT [tmpBlogPost] ON

INSERT [tmpBlogPost] (
[Id]
,[LanguageId]
,[Title]
,[Highlight]
,[ImageUrl]
,[Content]
,[IsPublished]
,[Tags]
,[MetaKeywords] 
,[MetaDescription]
,[MetaTitle]
,[IsDeleted]
,[UpdatedDate]
,[UpdatedBy]
,[CreatedDate]
,[CreatedBy]
)
SELECT 
[Id]
,1
,[Title]
,[Highlight]
,[ImageUrl]
,[Content]
,[IsPublished]
,NULL
,NULL
,NULL
,NULL
,[IsDeleted]
,[UpdatedDate]
,[UpdatedBy]
,[CreatedDate]
,[CreatedBy]
FROM [BlogPost]

SET IDENTITY_INSERT [tmpBlogPost] OFF

DROP TABLE	[BlogPost]

EXEC sp_rename 'tmpBlogPost', 'BlogPost'

CREATE TABLE [Category] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [nvarchar](100) NOT NULL,
    [Description] [nvarchar](max),
    [TemplateId] [int] NOT NULL,
    [MetaKeywords] [nvarchar](400),
    [MetaDescription] [nvarchar](max),
    [MetaTitle] [nvarchar](400),
    [ParentCategoryId] [int],
    [PhotoId] [int],
    [PageSize] [int] NOT NULL,
    [IsPublished] [bit] NOT NULL DEFAULT 0,
    [DisplayOrder] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
    [IsDeleted] [bit] NOT NULL DEFAULT 0,
    [UpdatedDate] [datetime] NOT NULL DEFAULT GETDATE(),
    [UpdatedBy] [varchar](50) NOT NULL DEFAULT suser_name(),
    [CreatedDate] [datetime] NOT NULL DEFAULT GETDATE(),
    [CreatedBy] [varchar](50) NOT NULL DEFAULT suser_name(),
    [RowRevision] rowversion NOT NULL,
    CONSTRAINT [PK_Category] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_LanguageId] ON [Category]([LanguageId])
ALTER TABLE [Category] ADD CONSTRAINT [FK_Category_Language_LanguageId] FOREIGN KEY ([LanguageId]) REFERENCES [Language] ([Id])

CREATE TABLE [CategoryPhoto] (
    [CategoryId] [int] NOT NULL,
    [PhotoId] [int] NOT NULL,
    [DisplayOrder] [int] NOT NULL,
    CONSTRAINT [PK_CategoryPhoto] PRIMARY KEY ([CategoryId], [PhotoId])
)
CREATE INDEX [IX_CategoryId] ON [CategoryPhoto]([CategoryId])
CREATE INDEX [IX_PhotoId] ON [CategoryPhoto]([PhotoId])
ALTER TABLE [CategoryPhoto] ADD CONSTRAINT [FK_CategoryPhoto_Category_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Category] ([Id]) ON DELETE CASCADE
ALTER TABLE [CategoryPhoto] ADD CONSTRAINT [FK_CategoryPhoto_Photo_PhotoId] FOREIGN KEY ([PhotoId]) REFERENCES [Photo] ([Id]) ON DELETE CASCADE