using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Facebook.Models
{
    public class Post
    {
        public Post()
        {
            Deleted = false;
            Post_Date = DateTime.Now;
        }
        [Key]
        public int PostId { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        [Required]
        public string Post_Body { get; set; }
        [Required]
        public DateTime Post_Date { get; set; }

        public virtual ICollection<Like> Likes { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public bool Deleted { get; set; }
    }
}