namespace AppStaff.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedGroupTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Employees", "GroupId", c => c.Int(nullable: false));
            CreateIndex("dbo.Employees", "GroupId");
            AddForeignKey("dbo.Employees", "GroupId", "dbo.Groups", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "GroupId", "dbo.Groups");
            DropIndex("dbo.Employees", new[] { "GroupId" });
            DropColumn("dbo.Employees", "GroupId");
            DropTable("dbo.Groups");
        }
    }
}
