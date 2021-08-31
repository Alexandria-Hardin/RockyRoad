namespace RockyRoad.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserChange : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Climber",
                c => new
                    {
                        ClimberId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        LevelOfExperience = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ClimberId)
                .ForeignKey("dbo.ApplicationUser", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ApplicationUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.IdentityRole", t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id);
            
            CreateTable(
                "dbo.Favorite",
                c => new
                    {
                        FavoriteId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ClimberId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FavoriteId)
                .ForeignKey("dbo.Climber", t => t.ClimberId, cascadeDelete: true)
                .Index(t => t.ClimberId);
            
            CreateTable(
                "dbo.FavoritePath",
                c => new
                    {
                        PathId = c.Int(nullable: false),
                        FavoriteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PathId, t.FavoriteId })
                .ForeignKey("dbo.Favorite", t => t.FavoriteId, cascadeDelete: true)
                .ForeignKey("dbo.Path", t => t.PathId, cascadeDelete: true)
                .Index(t => t.PathId)
                .Index(t => t.FavoriteId);
            
            CreateTable(
                "dbo.Path",
                c => new
                    {
                        PathId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        TypeOfRoute = c.Int(nullable: false),
                        LevelOfDifficulty = c.Int(nullable: false),
                        LocationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PathId)
                .ForeignKey("dbo.Location", t => t.LocationId, cascadeDelete: true)
                .Index(t => t.LocationId);
            
            CreateTable(
                "dbo.Location",
                c => new
                    {
                        LocationId = c.Int(nullable: false, identity: true),
                        State = c.String(nullable: false),
                        City = c.String(nullable: false),
                        Lattitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Longitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Address = c.String(),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.LocationId);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.FavoritePath", "PathId", "dbo.Path");
            DropForeignKey("dbo.Path", "LocationId", "dbo.Location");
            DropForeignKey("dbo.FavoritePath", "FavoriteId", "dbo.Favorite");
            DropForeignKey("dbo.Favorite", "ClimberId", "dbo.Climber");
            DropForeignKey("dbo.Climber", "UserId", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserRole", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserLogin", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserClaim", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropIndex("dbo.Path", new[] { "LocationId" });
            DropIndex("dbo.FavoritePath", new[] { "FavoriteId" });
            DropIndex("dbo.FavoritePath", new[] { "PathId" });
            DropIndex("dbo.Favorite", new[] { "ClimberId" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Climber", new[] { "UserId" });
            DropTable("dbo.IdentityRole");
            DropTable("dbo.Location");
            DropTable("dbo.Path");
            DropTable("dbo.FavoritePath");
            DropTable("dbo.Favorite");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.Climber");
        }
    }
}
