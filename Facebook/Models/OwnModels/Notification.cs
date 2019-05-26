using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Facebook.Models
{
    public class Notification
    {
        [Key]
        public int Notification_id { get; set; }
        [ForeignKey("Post")]
        public int Post_id { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserID { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Post Post { get; set; }
    }
}