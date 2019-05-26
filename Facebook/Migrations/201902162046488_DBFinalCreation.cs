namespace Facebook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBFinalCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Comment_ID = c.Int(nullable: false, identity: true),
                        Comment_Body = c.String(nullable: false),
                        Comment_Date = c.DateTime(nullable: false),
                        Post_ID = c.Int(nullable: false),
                        Id = c.String(maxLength: 128),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Comment_ID)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .ForeignKey("dbo.Posts", t => t.Post_ID, cascadeDelete: true)
                .Index(t => t.Post_ID)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 50),
                        Age = c.Int(),
                        Phone = c.String(),
                        image = c.String(),
                        Country = c.String(maxLength: 20),
                        City = c.String(maxLength: 20),
                        Deleted = c.Boolean(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Educations",
                c => new
                    {
                        EDUid = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        education = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.EDUid);
            
            CreateTable(
                "dbo.FriendRequests",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 128),
                        ReqFriendID = c.String(nullable: false, maxLength: 128),
                        Confirm = c.Boolean(nullable: false),
                        Sended = c.Boolean(nullable: false),
                        deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserID, t.ReqFriendID });
            
            CreateTable(
                "dbo.Likes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        PostID = c.Int(nullable: false),
                        like = c.Boolean(nullable: false),
                        LikeCounter = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.PostID })
                .ForeignKey("dbo.AspNetUsers", t => t.Id, cascadeDelete: true)
                .ForeignKey("dbo.Posts", t => t.PostID, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.PostID);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        PostId = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        Post_Body = c.String(nullable: false),
                        Post_Date = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PostId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Notification_id = c.Int(nullable: false, identity: true),
                        Post_id = c.Int(nullable: false),
                        UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Notification_id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .ForeignKey("dbo.Posts", t => t.Post_id, cascadeDelete: true)
                .Index(t => t.Post_id)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        skill = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UserFriends",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 128),
                        FriendID = c.String(nullable: false, maxLength: 128),
                        Deleted = c.Boolean(nullable: false),
                        Friends = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserID, t.FriendID });
            
            CreateTable(
                "dbo.Works",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        work = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Message_ID = c.Int(nullable: false, identity: true),
                        Message = c.String(nullable: false, maxLength: 200),
                        Mess_Date = c.DateTime(nullable: false, storeType: "date"),
                        Read = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        UserResiver_Id = c.String(maxLength: 128),
                        UserSender_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Message_ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserResiver_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserSender_Id)
                .Index(t => t.UserResiver_Id)
                .Index(t => t.UserSender_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.UserConnections",
                c => new
                    {
                        ConnectionID = c.String(nullable: false, maxLength: 128),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ConnectionID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.EducationApplicationUsers",
                c => new
                    {
                        Education_EDUid = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Education_EDUid, t.ApplicationUser_Id })
                .ForeignKey("dbo.Educations", t => t.Education_EDUid, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.Education_EDUid)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.FriendRequestApplicationUsers",
                c => new
                    {
                        FriendRequest_UserID = c.String(nullable: false, maxLength: 128),
                        FriendRequest_ReqFriendID = c.String(nullable: false, maxLength: 128),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.FriendRequest_UserID, t.FriendRequest_ReqFriendID, t.ApplicationUser_Id })
                .ForeignKey("dbo.FriendRequests", t => new { t.FriendRequest_UserID, t.FriendRequest_ReqFriendID }, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => new { t.FriendRequest_UserID, t.FriendRequest_ReqFriendID })
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.SkillApplicationUsers",
                c => new
                    {
                        Skill_ID = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Skill_ID, t.ApplicationUser_Id })
                .ForeignKey("dbo.Skills", t => t.Skill_ID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.Skill_ID)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.UserFriendApplicationUsers",
                c => new
                    {
                        UserFriend_UserID = c.String(nullable: false, maxLength: 128),
                        UserFriend_FriendID = c.String(nullable: false, maxLength: 128),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserFriend_UserID, t.UserFriend_FriendID, t.ApplicationUser_Id })
                .ForeignKey("dbo.UserFriends", t => new { t.UserFriend_UserID, t.UserFriend_FriendID }, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => new { t.UserFriend_UserID, t.UserFriend_FriendID })
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.WorkApplicationUsers",
                c => new
                    {
                        Work_id = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Work_id, t.ApplicationUser_Id })
                .ForeignKey("dbo.Works", t => t.Work_id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.Work_id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserConnections", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Messages", "UserSender_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "UserResiver_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "Post_ID", "dbo.Posts");
            DropForeignKey("dbo.Comments", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.WorkApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.WorkApplicationUsers", "Work_id", "dbo.Works");
            DropForeignKey("dbo.UserFriendApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserFriendApplicationUsers", new[] { "UserFriend_UserID", "UserFriend_FriendID" }, "dbo.UserFriends");
            DropForeignKey("dbo.SkillApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.SkillApplicationUsers", "Skill_ID", "dbo.Skills");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Notifications", "Post_id", "dbo.Posts");
            DropForeignKey("dbo.Notifications", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Likes", "PostID", "dbo.Posts");
            DropForeignKey("dbo.Posts", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Likes", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.FriendRequestApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.FriendRequestApplicationUsers", new[] { "FriendRequest_UserID", "FriendRequest_ReqFriendID" }, "dbo.FriendRequests");
            DropForeignKey("dbo.EducationApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.EducationApplicationUsers", "Education_EDUid", "dbo.Educations");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.WorkApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.WorkApplicationUsers", new[] { "Work_id" });
            DropIndex("dbo.UserFriendApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.UserFriendApplicationUsers", new[] { "UserFriend_UserID", "UserFriend_FriendID" });
            DropIndex("dbo.SkillApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.SkillApplicationUsers", new[] { "Skill_ID" });
            DropIndex("dbo.FriendRequestApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.FriendRequestApplicationUsers", new[] { "FriendRequest_UserID", "FriendRequest_ReqFriendID" });
            DropIndex("dbo.EducationApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.EducationApplicationUsers", new[] { "Education_EDUid" });
            DropIndex("dbo.UserConnections", new[] { "User_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Messages", new[] { "UserSender_Id" });
            DropIndex("dbo.Messages", new[] { "UserResiver_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.Notifications", new[] { "UserID" });
            DropIndex("dbo.Notifications", new[] { "Post_id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.Posts", new[] { "UserId" });
            DropIndex("dbo.Likes", new[] { "PostID" });
            DropIndex("dbo.Likes", new[] { "Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Comments", new[] { "Id" });
            DropIndex("dbo.Comments", new[] { "Post_ID" });
            DropTable("dbo.WorkApplicationUsers");
            DropTable("dbo.UserFriendApplicationUsers");
            DropTable("dbo.SkillApplicationUsers");
            DropTable("dbo.FriendRequestApplicationUsers");
            DropTable("dbo.EducationApplicationUsers");
            DropTable("dbo.UserConnections");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Messages");
            DropTable("dbo.Works");
            DropTable("dbo.UserFriends");
            DropTable("dbo.Skills");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Notifications");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.Posts");
            DropTable("dbo.Likes");
            DropTable("dbo.FriendRequests");
            DropTable("dbo.Educations");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Comments");
        }
    }
}
