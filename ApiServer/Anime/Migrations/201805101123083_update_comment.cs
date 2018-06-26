namespace Anime.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_comment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "IsEditable", c => c.Boolean(nullable: false));
            AddColumn("dbo.Comments", "EditingTime", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "EditingTime");
            DropColumn("dbo.Comments", "IsEditable");
        }
    }
}
