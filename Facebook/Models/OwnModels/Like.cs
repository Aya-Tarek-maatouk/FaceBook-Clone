using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Facebook.Models
{
    public class Like
    {
        public Like()
        {
            like = false;
        }
        [Key, Column(Order = 0)]
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Post")]
        public int PostID { get; set; }
        public bool like { get; set; }
        public int LikeCounter { get; set; }
        public virtual Post Post { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}