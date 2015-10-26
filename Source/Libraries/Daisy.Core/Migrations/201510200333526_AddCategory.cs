namespace Daisy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Category",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(maxLength: 100, unicode: true, nullable: false),
                    Description = c.String(),
                    TemplateId = c.Int(nullable: false),
                    MetaKeywords = c.String(nullable: true, maxLength: 400),
                    MetaDescription = c.String(nullable: true),
                    MetaTitle = c.String(nullable: true, maxLength: 400),
                    ParentCategoryId = c.Int(nullable: true),
                    PhotoId = c.Int(nullable: true),
                    PageSize = c.Int(nullable: false),
                    IsPublished = c.Boolean(nullable: false, defaultValue: false),
                    DisplayOrder = c.Int(nullable: false),
                    IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                    UpdatedDate = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedBy = c.String(nullable: false, maxLength: 50, unicode: false, defaultValueSql: "suser_name()"),
                    CreatedDate = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = c.String(nullable: false, maxLength: 50, unicode: false, defaultValueSql: "suser_name()"),
                    RowRevision = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "CategoryPhoto",
                c => new
                {
                    CategoryId = c.Int(nullable: false),
                    PhotoId = c.Int(nullable: false),
                    DisplayOrder = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.CategoryId, t.PhotoId })
                .ForeignKey("Category", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("Photo", t => t.PhotoId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.PhotoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CategoryPhoto", "PhotoId", "dbo.Photo");
            DropForeignKey("dbo.CategoryPhoto", "CategoryId", "dbo.Category");
            
            DropIndex("dbo.CategoryPhoto", new[] { "PhotoId" });
            DropIndex("dbo.CategoryPhoto", new[] { "CategoryId" });
           
            DropTable("dbo.CategoryPhoto");
            DropTable("dbo.Category");
           
        }
    }
}
