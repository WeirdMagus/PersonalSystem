namespace PersonalSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tst : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Schemata", newName: "Schedules");
            RenameColumn(table: "dbo.Groups", name: "Schema_Id", newName: "Schedule_Id");
            RenameIndex(table: "dbo.Groups", name: "IX_Schema_Id", newName: "IX_Schedule_Id");
            CreateIndex("dbo.Companies", "Name", unique: true);
            CreateIndex("dbo.Groups", "Name", unique: true);
            CreateIndex("dbo.Departments", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Departments", new[] { "Name" });
            DropIndex("dbo.Groups", new[] { "Name" });
            DropIndex("dbo.Companies", new[] { "Name" });
            RenameIndex(table: "dbo.Groups", name: "IX_Schedule_Id", newName: "IX_Schema_Id");
            RenameColumn(table: "dbo.Groups", name: "Schedule_Id", newName: "Schema_Id");
            RenameTable(name: "dbo.Schedules", newName: "Schemata");
        }
    }
}
