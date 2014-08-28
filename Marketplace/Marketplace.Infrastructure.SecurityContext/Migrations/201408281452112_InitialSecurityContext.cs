namespace Marketplace.Infrastructure.SecurityContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialSecurityContext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BusinessUnits",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        IsAdminUnit = c.Boolean(nullable: false),
                        ParentId = c.Guid(),
                        AuditInfo_ModifiedOn = c.DateTime(),
                        AuditInfo_ModifiedBy = c.Guid(),
                        AuditInfo_CreatedOn = c.DateTime(),
                        AuditInfo_CreatedBy = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BusinessUnits", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Username = c.String(),
                        Password = c.String(),
                        PasswordSalt = c.String(),
                        LCID = c.Int(nullable: false),
                        BusinessUnitId = c.Guid(),
                        AuditInfo_ModifiedOn = c.DateTime(),
                        AuditInfo_ModifiedBy = c.Guid(),
                        AuditInfo_CreatedOn = c.DateTime(),
                        AuditInfo_CreatedBy = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BusinessUnits", t => t.BusinessUnitId)
                .Index(t => t.BusinessUnitId);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Tel = c.String(),
                        Name = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.UserAppTokens",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ClientAppId = c.Guid(nullable: false),
                        Token = c.String(),
                        TokenExpiration = c.DateTime(),
                        AppUser_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClientApps", t => t.ClientAppId)
                .ForeignKey("dbo.Users", t => t.AppUser_Id, cascadeDelete: true)
                .Index(t => t.ClientAppId)
                .Index(t => t.AppUser_Id);
            
            CreateTable(
                "dbo.ClientApps",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        ClientToken = c.String(),
                        TokenValidity = c.Int(nullable: false),
                        PersistentTokenValidity = c.Int(nullable: false),
                        RequestTimeOffset = c.Int(nullable: false),
                        AuditInfo_ModifiedOn = c.DateTime(),
                        AuditInfo_ModifiedBy = c.Guid(),
                        AuditInfo_CreatedOn = c.DateTime(),
                        AuditInfo_CreatedBy = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserGroups",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        IsAdminGroup = c.Boolean(nullable: false),
                        AuditInfo_ModifiedOn = c.DateTime(),
                        AuditInfo_ModifiedBy = c.Guid(),
                        AuditInfo_CreatedOn = c.DateTime(),
                        AuditInfo_CreatedBy = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TypeName = c.String(),
                        AccessMode = c.Short(nullable: false),
                        AuditInfo_ModifiedOn = c.DateTime(),
                        AuditInfo_ModifiedBy = c.Guid(),
                        AuditInfo_CreatedOn = c.DateTime(),
                        AuditInfo_CreatedBy = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserGroupPermission",
                c => new
                    {
                        PermissionModel_Id = c.Guid(nullable: false),
                        UserGroupModel_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.PermissionModel_Id, t.UserGroupModel_Id })
                .ForeignKey("dbo.Permissions", t => t.PermissionModel_Id, cascadeDelete: true)
                .ForeignKey("dbo.UserGroups", t => t.UserGroupModel_Id, cascadeDelete: true)
                .Index(t => t.PermissionModel_Id)
                .Index(t => t.UserGroupModel_Id);
            
            CreateTable(
                "dbo.UserUserGroup",
                c => new
                    {
                        UserModel_Id = c.Guid(nullable: false),
                        UserGroupModel_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserModel_Id, t.UserGroupModel_Id })
                .ForeignKey("dbo.Users", t => t.UserModel_Id, cascadeDelete: true)
                .ForeignKey("dbo.UserGroups", t => t.UserGroupModel_Id, cascadeDelete: true)
                .Index(t => t.UserModel_Id)
                .Index(t => t.UserGroupModel_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "BusinessUnitId", "dbo.BusinessUnits");
            DropForeignKey("dbo.UserUserGroup", "UserGroupModel_Id", "dbo.UserGroups");
            DropForeignKey("dbo.UserUserGroup", "UserModel_Id", "dbo.Users");
            DropForeignKey("dbo.UserGroupPermission", "UserGroupModel_Id", "dbo.UserGroups");
            DropForeignKey("dbo.UserGroupPermission", "PermissionModel_Id", "dbo.Permissions");
            DropForeignKey("dbo.UserAppTokens", "AppUser_Id", "dbo.Users");
            DropForeignKey("dbo.UserAppTokens", "ClientAppId", "dbo.ClientApps");
            DropForeignKey("dbo.Contacts", "Id", "dbo.Users");
            DropForeignKey("dbo.BusinessUnits", "ParentId", "dbo.BusinessUnits");
            DropIndex("dbo.UserUserGroup", new[] { "UserGroupModel_Id" });
            DropIndex("dbo.UserUserGroup", new[] { "UserModel_Id" });
            DropIndex("dbo.UserGroupPermission", new[] { "UserGroupModel_Id" });
            DropIndex("dbo.UserGroupPermission", new[] { "PermissionModel_Id" });
            DropIndex("dbo.UserAppTokens", new[] { "AppUser_Id" });
            DropIndex("dbo.UserAppTokens", new[] { "ClientAppId" });
            DropIndex("dbo.Contacts", new[] { "Id" });
            DropIndex("dbo.Users", new[] { "BusinessUnitId" });
            DropIndex("dbo.BusinessUnits", new[] { "ParentId" });
            DropIndex("dbo.BusinessUnits", new[] { "Name" });
            DropTable("dbo.UserUserGroup");
            DropTable("dbo.UserGroupPermission");
            DropTable("dbo.Permissions");
            DropTable("dbo.UserGroups");
            DropTable("dbo.ClientApps");
            DropTable("dbo.UserAppTokens");
            DropTable("dbo.Contacts");
            DropTable("dbo.Users");
            DropTable("dbo.BusinessUnits");
        }
    }
}
