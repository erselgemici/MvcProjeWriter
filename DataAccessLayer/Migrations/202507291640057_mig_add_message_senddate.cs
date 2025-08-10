namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_add_message_senddate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "SendDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "SendDate");
        }
    }
}
