namespace MultiMediaDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTables2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ImageModels", newName: "Images");
            RenameTable(name: "dbo.PlaylistModels", newName: "Playlists");
            RenameTable(name: "dbo.TreeViewNodeModels", newName: "TreeViewNodes");
            RenameTable(name: "dbo.VideoModels", newName: "Videos");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Videos", newName: "VideoModels");
            RenameTable(name: "dbo.TreeViewNodes", newName: "TreeViewNodeModels");
            RenameTable(name: "dbo.Playlists", newName: "PlaylistModels");
            RenameTable(name: "dbo.Images", newName: "ImageModels");
        }
    }
}
