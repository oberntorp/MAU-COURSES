namespace MultiMediaDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SourceUrl = c.String(),
                        PreviewUrl = c.String(),
                        FileExtention = c.String(),
                        Width = c.Int(nullable: false),
                        Height = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Playlists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        PlaylistPlaybackDelayBetweenMediaSec = c.Int(nullable: false),
                        Image_Id = c.Int(),
                        ParentNode_Id = c.Int(),
                        Video_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Videos", t => t.Image_Id)
                .ForeignKey("dbo.TreeViewNodes", t => t.ParentNode_Id)
                .ForeignKey("dbo.Videos", t => t.Video_Id)
                .Index(t => t.Image_Id)
                .Index(t => t.ParentNode_Id)
                .Index(t => t.Video_Id);
            
            CreateTable(
                "dbo.Videos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SourceUrl = c.String(),
                        PreviewUrl = c.String(),
                        FileExtention = c.String(),
                        LengthInSeconds = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TreeViewNodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Name = c.String(),
                        TreeViewNode_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TreeViewNodes", t => t.TreeViewNode_Id)
                .Index(t => t.TreeViewNode_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Playlists", "Video_Id", "dbo.Videos");
            DropForeignKey("dbo.Playlists", "ParentNode_Id", "dbo.TreeViewNodes");
            DropForeignKey("dbo.TreeViewNodes", "TreeViewNode_Id", "dbo.TreeViewNodes");
            DropForeignKey("dbo.Playlists", "Image_Id", "dbo.Videos");
            DropIndex("dbo.TreeViewNodes", new[] { "TreeViewNode_Id" });
            DropIndex("dbo.Playlists", new[] { "Video_Id" });
            DropIndex("dbo.Playlists", new[] { "ParentNode_Id" });
            DropIndex("dbo.Playlists", new[] { "Image_Id" });
            DropTable("dbo.TreeViewNodes");
            DropTable("dbo.Videos");
            DropTable("dbo.Playlists");
            DropTable("dbo.Images");
        }
    }
}
