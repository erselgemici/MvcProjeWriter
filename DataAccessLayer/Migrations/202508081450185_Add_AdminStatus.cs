namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_AdminStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Admins", "AdminStatus", c => c.Boolean(nullable: false, defaultValue: true));
        }


        public override void Down()
        {
            AlterColumn("dbo.Admins", "AdminStatus", c => c.String(maxLength: 1));
        }
    }
}
