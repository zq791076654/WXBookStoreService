namespace WXBookStoreService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addComment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(),
                        BookId = c.Int(),
                        Content = c.String(),
                        CommentTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserInfo", t => t.UserId)
                .ForeignKey("dbo.Book", t => t.BookId)
                .Index(t => t.UserId)
                .Index(t => t.BookId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comment", "BookId", "dbo.Book");
            DropForeignKey("dbo.Comment", "UserId", "dbo.UserInfo");
            DropIndex("dbo.Comment", new[] { "BookId" });
            DropIndex("dbo.Comment", new[] { "UserId" });
            DropTable("dbo.Comment");
        }
    }
}
