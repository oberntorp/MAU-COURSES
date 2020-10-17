namespace MultiMediaDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTables1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ImageModels", "SortInPlaylist", c => c.Int(nullable: false));
            AddColumn("dbo.VideoModels", "SortInPlaylist", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.VideoModels", "SortInPlaylist");
            DropColumn("dbo.ImageModels", "SortInPlaylist");
        }
    }
}
