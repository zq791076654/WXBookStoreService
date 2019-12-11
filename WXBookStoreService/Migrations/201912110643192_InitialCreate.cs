namespace WXBookStoreService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Book",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Author = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CoverUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(),
                        BookId = c.Int(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserInfo", t => t.UserId)
                .ForeignKey("dbo.Book", t => t.BookId)
                .Index(t => t.UserId)
                .Index(t => t.BookId);
            
            CreateTable(
                "dbo.UserInfo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Token = c.String(),
                        OpenId = c.String(),
                        SessionKey = c.String(),
                        Unionid = c.String(),
                        NickName = c.String(),
                        Gender = c.Int(nullable: false),
                        City = c.String(),
                        Province = c.String(),
                        Country = c.String(),
                        AvatarUrl = c.String(),
                        AppId = c.String(),
                        TimeStamp = c.Int(nullable: false),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Order", "BookId", "dbo.Book");
            DropForeignKey("dbo.Order", "UserId", "dbo.UserInfo");
            DropIndex("dbo.Order", new[] { "BookId" });
            DropIndex("dbo.Order", new[] { "UserId" });
            DropTable("dbo.UserInfo");
            DropTable("dbo.Order");
            DropTable("dbo.Book");
        }
    }
}
