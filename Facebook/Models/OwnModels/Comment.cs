using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Facebook.Models
{
    public class Comment
    {
        public Comment()
        {
            Deleted = false;
            Comment_Date = DateTime.Now;
        }
        [Key]
        public int Comment_ID { get; set; }
        [Required]
        public string Comment_Body { get; set; }
        [Required]
        public DateTime Comment_Date { get; set; }
        [ForeignKey("Post")]
        public int Post_ID { get; set; }
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Post Post { get; set; }
        public bool Deleted { get; set; }

    }
}