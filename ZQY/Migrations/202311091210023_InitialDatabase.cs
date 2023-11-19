namespace ZQY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Everies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Length = c.String(),
                        Description = c.String(),
                        SeasonID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Seasons", t => t.SeasonID)
                .Index(t => t.SeasonID);
            
            CreateTable(
                "dbo.Seasons",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Everies", "SeasonID", "dbo.Seasons");
            DropIndex("dbo.Everies", new[] { "SeasonID" });
            DropTable("dbo.Seasons");
            DropTable("dbo.Everies");
        }
    }
}
