namespace Marketplace.Infrastructure.SecurityContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedVersioningAndAuditFromValueObjects : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Contacts", "ObjectState_State");
            DropColumn("dbo.Contacts", "Version_Id");
            DropColumn("dbo.UserAppTokens", "ObjectState_State");
            DropColumn("dbo.UserAppTokens", "Version_Id");
            DropColumn("dbo.Permissions", "AuditInfo_ModifiedOn");
            DropColumn("dbo.Permissions", "AuditInfo_ModifiedBy");
            DropColumn("dbo.Permissions", "AuditInfo_CreatedOn");
            DropColumn("dbo.Permissions", "AuditInfo_CreatedBy");
            DropColumn("dbo.Permissions", "ObjectState_State");
            DropColumn("dbo.Permissions", "Version_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Permissions", "Version_Id", c => c.String());
            AddColumn("dbo.Permissions", "ObjectState_State", c => c.Short(nullable: false));
            AddColumn("dbo.Permissions", "AuditInfo_CreatedBy", c => c.Guid());
            AddColumn("dbo.Permissions", "AuditInfo_CreatedOn", c => c.DateTime());
            AddColumn("dbo.Permissions", "AuditInfo_ModifiedBy", c => c.Guid());
            AddColumn("dbo.Permissions", "AuditInfo_ModifiedOn", c => c.DateTime());
            AddColumn("dbo.UserAppTokens", "Version_Id", c => c.String());
            AddColumn("dbo.UserAppTokens", "ObjectState_State", c => c.Short(nullable: false));
            AddColumn("dbo.Contacts", "Version_Id", c => c.String());
            AddColumn("dbo.Contacts", "ObjectState_State", c => c.Short(nullable: false));
        }
    }
}
