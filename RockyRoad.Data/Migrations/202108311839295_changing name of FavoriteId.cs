namespace RockyRoad.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changingnameofFavoriteId : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.FavoritePath", name: "FavoriteId", newName: "FavID");
            RenameIndex(table: "dbo.FavoritePath", name: "IX_FavoriteId", newName: "IX_FavID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.FavoritePath", name: "IX_FavID", newName: "IX_FavoriteId");
            RenameColumn(table: "dbo.FavoritePath", name: "FavID", newName: "FavoriteId");
        }
    }
}
