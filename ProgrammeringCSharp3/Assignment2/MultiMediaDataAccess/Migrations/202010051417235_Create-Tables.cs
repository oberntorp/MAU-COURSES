namespace MultiMediaDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ImageModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SourceUrl = c.String(),
                        PreviewUrl = c.String(),
                        FileExtention = c.String(),
                        Width = c.Int(nullable: false),
                        Height = c.Int(nullable: false),
                        PlaylistModel_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PlaylistModels", t => t.PlaylistModel_Id)
                .Index(t => t.PlaylistModel_Id);
            
            CreateTable(
                "dbo.PlaylistModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        PlaylistPlaybackDelayBetweenMediaSec = c.Int(nullable: false),
                        ParentNode_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TreeViewNodeModels", t => t.ParentNode_Id)
                .Index(t => t.ParentNode_Id);
            
            CreateTable(
                "dbo.TreeViewNodeModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Name = c.String(),
                        TreeViewNodeModel_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TreeViewNodeModels", t => t.TreeViewNodeModel_Id)
                .Index(t => t.TreeViewNodeModel_Id);
            
            CreateTable(
                "dbo.VideoModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SourceUrl = c.String(),
                        PreviewUrl = c.String(),
                        FileExtention = c.String(),
                        LengthInSeconds = c.Double(nullable: false),
                        PlaylistModel_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PlaylistModels", t => t.PlaylistModel_Id)
                .Index(t => t.PlaylistModel_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VideoModels", "PlaylistModel_Id", "dbo.PlaylistModels");
            DropForeignKey("dbo.PlaylistModels", "ParentNode_Id", "dbo.TreeViewNodeModels");
            DropForeignKey("dbo.TreeViewNodeModels", "TreeViewNodeModel_Id", "dbo.TreeViewNodeModels");
            DropForeignKey("dbo.ImageModels", "PlaylistModel_Id", "dbo.PlaylistModels");
            DropIndex("dbo.VideoModels", new[] { "PlaylistModel_Id" });
            DropIndex("dbo.TreeViewNodeModels", new[] { "TreeViewNodeModel_Id" });
            DropIndex("dbo.PlaylistModels", new[] { "ParentNode_Id" });
            DropIndex("dbo.ImageModels", new[] { "PlaylistModel_Id" });
            DropTable("dbo.VideoModels");
            DropTable("dbo.TreeViewNodeModels");
            DropTable("dbo.PlaylistModels");
            DropTable("dbo.ImageModels");
        }
    }
}
