namespace Marketplace.Infrastructure.SecurityContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VersioningAndAudit : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.BusinessUnits", new[] { "Name" });
            AddColumn("dbo.BusinessUnits", "ObjectState_State", c => c.Short(nullable: false));
            AddColumn("dbo.BusinessUnits", "Version_Id", c => c.String());
            AddColumn("dbo.Users", "ObjectState_State", c => c.Short(nullable: false));
            AddColumn("dbo.Users", "Version_Id", c => c.String());
            AddColumn("dbo.Contacts", "ObjectState_State", c => c.Short(nullable: false));
            AddColumn("dbo.Contacts", "Version_Id", c => c.String());
            AddColumn("dbo.UserAppTokens", "ObjectState_State", c => c.Short(nullable: false));
            AddColumn("dbo.UserAppTokens", "Version_Id", c => c.String());
            AddColumn("dbo.ClientApps", "ObjectState_State", c => c.Short(nullable: false));
            AddColumn("dbo.ClientApps", "Version_Id", c => c.String());
            AddColumn("dbo.UserGroups", "ObjectState_State", c => c.Short(nullable: false));
            AddColumn("dbo.UserGroups", "Version_Id", c => c.String());
            AddColumn("dbo.Permissions", "ObjectState_State", c => c.Short(nullable: false));
            AddColumn("dbo.Permissions", "Version_Id", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Permissions", "Version_Id");
            DropColumn("dbo.Permissions", "ObjectState_State");
            DropColumn("dbo.UserGroups", "Version_Id");
            DropColumn("dbo.UserGroups", "ObjectState_State");
            DropColumn("dbo.ClientApps", "Version_Id");
            DropColumn("dbo.ClientApps", "ObjectState_State");
            DropColumn("dbo.UserAppTokens", "Version_Id");
            DropColumn("dbo.UserAppTokens", "ObjectState_State");
            DropColumn("dbo.Contacts", "Version_Id");
            DropColumn("dbo.Contacts", "ObjectState_State");
            DropColumn("dbo.Users", "Version_Id");
            DropColumn("dbo.Users", "ObjectState_State");
            DropColumn("dbo.BusinessUnits", "Version_Id");
            DropColumn("dbo.BusinessUnits", "ObjectState_State");
            CreateIndex("dbo.BusinessUnits", "Name");
        }
    }
}
