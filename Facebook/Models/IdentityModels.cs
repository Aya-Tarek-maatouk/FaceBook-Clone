using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Facebook.Models.OwnModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Facebook.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Posts = new HashSet<Post>();
            Comments = new HashSet<Comment>();
            Likes = new HashSet<Like>();
            UserFriend = new HashSet<UserFriend>();
            Works = new HashSet<Work>();
            Skills = new HashSet<Skill>();
            Educations = new HashSet<Education>();
            Deleted = false;
            image = "Facebook_default_img.jpg";
            Notifications = new HashSet<Notification>();

        }
        [Required, StringLength(50)]
        public string Name { get; set; }

        [Range(minimum: 18, maximum: 100)]
        public int? Age { get; set; }
        public string Phone { get; set; }

        public string image { get; set; }
        [StringLength(20)]
        public string Country { get; set; }
        [StringLength(20)]
        public string City { get; set; }

        //[StringLength(20)]
        //public string UserType { get; set; }

        public virtual ICollection<Work> Works { get; set; }
        public virtual ICollection<Skill> Skills { get; set; }
        public virtual ICollection<Education> Educations { get; set; }
        public virtual ICollection<UserFriend> UserFriend { get; set; }
        public virtual ICollection<Post> Posts { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Notification> Notifications{ get; set; }
        public virtual ICollection<FriendRequest> FriendRequest { get; set; }

        public bool? Deleted { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public DbSet<Comment> comment { get; set; }
        public DbSet<Post> post { get; set; }
        public DbSet<Education> education { get; set; }
        public DbSet<Like> like { get; set; }
        public DbSet<Skill> skill { get; set; }
        public DbSet<UserFriend> userFriend { get; set; }
        public DbSet<Work> work { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        public virtual DbSet<Messages> Messages { get; set; }
        public virtual DbSet<UserConnection> UserConnections { get; set; }

        public virtual DbSet<FriendRequest> FriendRequests { get; set; }
        public static ApplicationDbContext Create()

        {
            return new ApplicationDbContext();
        }
    }
}