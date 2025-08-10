namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_add_message_flags : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "IsDraft", c => c.Boolean(nullable: false));
            AddColumn("dbo.Messages", "IsSpam", c => c.Boolean(nullable: false));
            AddColumn("dbo.Messages", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "IsDeleted");
            DropColumn("dbo.Messages", "IsSpam");
            DropColumn("dbo.Messages", "IsDraft");
        }
    }
}
