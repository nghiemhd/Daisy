namespace Daisy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserTable : DbMigration
    {
        public override void Up()
        {
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
            CreateIndex("User", "Username", true, "uni_Username"); 
        }
        
        public override void Down()
        {
            DropTable("User");
        }
    }
}
