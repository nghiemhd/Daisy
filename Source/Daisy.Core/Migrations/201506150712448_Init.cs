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
                        IsDisplayed = c.Boolean(nullable: false, defaultValue:true),
                        FlickrAlbumId = c.String(maxLength: 100),
                        IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                        UpdatedDate = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                        UpdatedBy = c.String(nullable: false, maxLength: 50, unicode: false, defaultValueSql: "suser_name()"),
                        CreatedDate = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                        CreatedBy = c.String(nullable: false, maxLength: 50, unicode: false, defaultValueSql: "suser_name()"),
                        RowRevision = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Photo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Url = c.String(),
                        IsDisplayed = c.Boolean(nullable: false, defaultValue:true),
                        IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                        UpdatedDate = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                        UpdatedBy = c.String(nullable: false, maxLength: 50, unicode: false, defaultValueSql: "suser_name()"),
                        CreatedDate = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                        CreatedBy = c.String(nullable: false, maxLength: 50, unicode: false, defaultValueSql: "suser_name()"),
                        RowRevision = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 50, unicode: false),
                        Password = c.String(nullable: false, maxLength: 255, unicode: false),
                        Email = c.String(maxLength: 100, unicode: false),
                        IsActive = c.Boolean(nullable: false, defaultValue:true),
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