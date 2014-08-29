namespace Marketplace.Infrastructure.SecurityContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedVersioningAndAuditFromValueObjects2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BusinessUnits", "Created_On", c => c.DateTime());
            AddColumn("dbo.BusinessUnits", "Created_By", c => c.Guid());
            AddColumn("dbo.BusinessUnits", "Modified_On", c => c.DateTime());
            AddColumn("dbo.BusinessUnits", "Modified_By", c => c.Guid());
            AddColumn("dbo.Users", "Created_On", c => c.DateTime());
            AddColumn("dbo.Users", "Created_By", c => c.Guid());
            AddColumn("dbo.Users", "Modified_On", c => c.DateTime());
            AddColumn("dbo.Users", "Modified_By", c => c.Guid());
            AddColumn("dbo.ClientApps", "Created_On", c => c.DateTime());
            AddColumn("dbo.ClientApps", "Created_By", c => c.Guid());
            AddColumn("dbo.ClientApps", "Modified_On", c => c.DateTime());
            AddColumn("dbo.ClientApps", "Modified_By", c => c.Guid());
            AddColumn("dbo.UserGroups", "Created_On", c => c.DateTime());
            AddColumn("dbo.UserGroups", "Created_By", c => c.Guid());
            AddColumn("dbo.UserGroups", "Modified_On", c => c.DateTime());
            AddColumn("dbo.UserGroups", "Modified_By", c => c.Guid());
            DropColumn("dbo.BusinessUnits", "AuditInfo_ModifiedOn");
            DropColumn("dbo.BusinessUnits", "AuditInfo_ModifiedBy");
            DropColumn("dbo.BusinessUnits", "AuditInfo_CreatedOn");
            DropColumn("dbo.BusinessUnits", "AuditInfo_CreatedBy");
            DropColumn("dbo.Users", "AuditInfo_ModifiedOn");
            DropColumn("dbo.Users", "AuditInfo_ModifiedBy");
            DropColumn("dbo.Users", "AuditInfo_CreatedOn");
            DropColumn("dbo.Users", "AuditInfo_CreatedBy");
            DropColumn("dbo.ClientApps", "AuditInfo_ModifiedOn");
            DropColumn("dbo.ClientApps", "AuditInfo_ModifiedBy");
            DropColumn("dbo.ClientApps", "AuditInfo_CreatedOn");
            DropColumn("dbo.ClientApps", "AuditInfo_CreatedBy");
            DropColumn("dbo.UserGroups", "AuditInfo_ModifiedOn");
            DropColumn("dbo.UserGroups", "AuditInfo_ModifiedBy");
            DropColumn("dbo.UserGroups", "AuditInfo_CreatedOn");
            DropColumn("dbo.UserGroups", "AuditInfo_CreatedBy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserGroups", "AuditInfo_CreatedBy", c => c.Guid());
            AddColumn("dbo.UserGroups", "AuditInfo_CreatedOn", c => c.DateTime());
            AddColumn("dbo.UserGroups", "AuditInfo_ModifiedBy", c => c.Guid());
            AddColumn("dbo.UserGroups", "AuditInfo_ModifiedOn", c => c.DateTime());
            AddColumn("dbo.ClientApps", "AuditInfo_CreatedBy", c => c.Guid());
            AddColumn("dbo.ClientApps", "AuditInfo_CreatedOn", c => c.DateTime());
            AddColumn("dbo.ClientApps", "AuditInfo_ModifiedBy", c => c.Guid());
            AddColumn("dbo.ClientApps", "AuditInfo_ModifiedOn", c => c.DateTime());
            AddColumn("dbo.Users", "AuditInfo_CreatedBy", c => c.Guid());
            AddColumn("dbo.Users", "AuditInfo_CreatedOn", c => c.DateTime());
            AddColumn("dbo.Users", "AuditInfo_ModifiedBy", c => c.Guid());
            AddColumn("dbo.Users", "AuditInfo_ModifiedOn", c => c.DateTime());
            AddColumn("dbo.BusinessUnits", "AuditInfo_CreatedBy", c => c.Guid());
            AddColumn("dbo.BusinessUnits", "AuditInfo_CreatedOn", c => c.DateTime());
            AddColumn("dbo.BusinessUnits", "AuditInfo_ModifiedBy", c => c.Guid());
            AddColumn("dbo.BusinessUnits", "AuditInfo_ModifiedOn", c => c.DateTime());
            DropColumn("dbo.UserGroups", "Modified_By");
            DropColumn("dbo.UserGroups", "Modified_On");
            DropColumn("dbo.UserGroups", "Created_By");
            DropColumn("dbo.UserGroups", "Created_On");
            DropColumn("dbo.ClientApps", "Modified_By");
            DropColumn("dbo.ClientApps", "Modified_On");
            DropColumn("dbo.ClientApps", "Created_By");
            DropColumn("dbo.ClientApps", "Created_On");
            DropColumn("dbo.Users", "Modified_By");
            DropColumn("dbo.Users", "Modified_On");
            DropColumn("dbo.Users", "Created_By");
            DropColumn("dbo.Users", "Created_On");
            DropColumn("dbo.BusinessUnits", "Modified_By");
            DropColumn("dbo.BusinessUnits", "Modified_On");
            DropColumn("dbo.BusinessUnits", "Created_By");
            DropColumn("dbo.BusinessUnits", "Created_On");
        }
    }
}
