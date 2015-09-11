namespace Daisy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Album",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        ThumbnailUrl = c.String(),
                        IsPublished = c.Boolean(nullable: false, defaultValue: true),
                        FlickrAlbumId = c.String(maxLength: 50, unicode: false),
                        IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                        UpdatedDate = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                        UpdatedBy = c.String(nullable: false, maxLength: 50, unicode: false, defaultValueSql: "suser_name()"),
                        CreatedDate = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                        CreatedBy = c.String(nullable: false, maxLength: 50, unicode: false, defaultValueSql: "suser_name()"),
                        RowRevision = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);

            CreateIndex("Album", "FlickrAlbumId", true, "UNI_Album_FlickrAlbumId");

            CreateTable(
                "Photo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        SmallUrl = c.String(),
                        MediumUrl = c.String(),
                        LargeUrl = c.String(),
                        Large1600Url = c.String(),
                        Large2048Url = c.String(),
                        OriginalUrl = c.String(),  
                        FlickrPhotoId = c.String(maxLength: 50, unicode: false),
                        Content = c.Binary(nullable: true),
                        IsPublished = c.Boolean(nullable: false, defaultValue: true),
                        IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                        UpdatedDate = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                        UpdatedBy = c.String(nullable: false, maxLength: 50, unicode: false, defaultValueSql: "suser_name()"),
                        CreatedDate = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                        CreatedBy = c.String(nullable: false, maxLength: 50, unicode: false, defaultValueSql: "suser_name()"),
                        RowRevision = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);

            CreateIndex("Photo", "FlickrPhotoId", true, "UNI_Photo_FlickrPhotoId");
            
            CreateTable(
                "User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 50, unicode: false),
                        Password = c.String(nullable: false, maxLength: 255, unicode: false),
                        Email = c.String(maxLength: 100, unicode: false),
                        IsActive = c.Boolean(nullable: false, defaultValue: true),
                        IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                        UpdatedDate = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                        UpdatedBy = c.String(nullable: false, maxLength: 50, unicode: false, defaultValueSql: "suser_name()"),
                        CreatedDate = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                        CreatedBy = c.String(nullable: false, maxLength: 50, unicode: false, defaultValueSql: "suser_name()"),
                        RowRevision = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "Slider",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(maxLength: 100, unicode: false),
                    Page = c.String(maxLength: 100, unicode: false),
                    Order = c.Int(nullable: false),
                    IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                    UpdatedDate = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedBy = c.String(nullable: false, maxLength: 50, unicode: false, defaultValueSql: "suser_name()"),
                    CreatedDate = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = c.String(nullable: false, maxLength: 50, unicode: false, defaultValueSql: "suser_name()"),
                    RowRevision = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "AlbumPhoto",
                c => new
                {
                    AlbumId = c.Int(nullable: false),
                    PhotoId = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.AlbumId, t.PhotoId })
                .ForeignKey("Photo", t => t.PhotoId, cascadeDelete: true)
                .ForeignKey("Album", t => t.AlbumId, cascadeDelete: true)
                .Index(t => t.PhotoId)
                .Index(t => t.AlbumId);

            CreateTable(
                "SliderPhoto",
                c => new
                {
                    SliderId = c.Int(nullable: false),
                    PhotoId = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.SliderId, t.PhotoId })
                .ForeignKey("Photo", t => t.PhotoId, cascadeDelete: true)
                .ForeignKey("Slider", t => t.SliderId, cascadeDelete: true)
                .Index(t => t.PhotoId)
                .Index(t => t.SliderId);

            CreateTable(
                "BlogPost",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    LanguageId = c.Int(nullable: false),
                    Title = c.String(nullable: false, maxLength: 200, unicode: true),
                    Highlight = c.String(nullable: false, maxLength: 500, unicode: true),
                    ImageUrl = c.String(nullable: true),
                    Content = c.String(nullable: false, unicode: true),
                    IsPublished = c.Boolean(nullable: false, defaultValue: true),
                    Tags = c.String(nullable: true),
                    MetaKeywords = c.String(nullable: true, maxLength: 400),
                    MetaDescription = c.String(nullable: true),
                    MetaTitle = c.String(nullable: true, maxLength: 400),
                    IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                    UpdatedDate = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedBy = c.String(nullable: false, maxLength: 50, unicode: false, defaultValueSql: "suser_name()"),
                    CreatedDate = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = c.String(nullable: false, maxLength: 50, unicode: false, defaultValueSql: "suser_name()"),
                    RowRevision = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Language", t => t.LanguageId, cascadeDelete: false)
                .Index(t => t.LanguageId);

            //CreateTable(
            //    "BlogPhoto",
            //    c => new
            //    {
            //        BlogId = c.Int(nullable: false),
            //        PhotoId = c.Int(nullable: false),
            //        Order = c.Int(nullable: false),
            //        Position = c.String(nullable: true, maxLength: 50, unicode: false)
            //    })
            //    .PrimaryKey(t => new { t.BlogId, t.PhotoId })
            //    .ForeignKey("Photo", t => t.PhotoId, cascadeDelete: true)
            //    .ForeignKey("Blog", t => t.BlogId, cascadeDelete: true)
            //    .Index(t => t.PhotoId)
            //    .Index(t => t.BlogId);

            CreateTable(
                "UrlRecord",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    EntityId = c.Int(nullable: false),
                    EntityName = c.String(nullable: false, maxLength: 100, unicode: false),
                    Slug = c.String(nullable: false, maxLength: 100, unicode: false),
                    IsActive = c.Boolean(nullable: false, defaultValue: true),
                    LanguageId = c.Int(nullable: false, defaultValue: 0)
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "Language",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 100),
                    LanguageCulture = c.String(nullable: false, maxLength: 20, unicode: false),
                    UniqueSeoCode = c.String(maxLength: 2, unicode: false),
                    FlagImageFileName = c.String(maxLength: 50, unicode: false),
                    Rtl = c.Boolean(nullable: false, defaultValue: false),
                    IsPublished = c.Boolean(nullable: false, defaultValue: false),
                    DisplayOrder = c.Int(nullable: false, defaultValue: 0)
                })
                .PrimaryKey(t => t.Id);
        }
        
        public override void Down()
        {
            DropForeignKey("AlbumPhoto", "AlbumId", "Album");
            DropForeignKey("AlbumPhoto", "PhotoId", "Photo");
            DropIndex("AlbumPhoto", new[] { "AlbumId" });
            DropIndex("AlbumPhoto", new[] { "PhotoId" });
            DropTable("AlbumPhoto");
            DropTable("User");
            DropTable("Photo");
            DropTable("Album");
        }
    }
}
