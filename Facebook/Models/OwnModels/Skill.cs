using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Facebook.Models
{
    public class Skill
    {
        public Skill()
        {
            ApplicationUser = new HashSet<ApplicationUser>();
        }
        [Key]
        public int ID { get; set; }
        [ ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        [StringLength(50)]
        public string skill { get; set; }
        public virtual ICollection<ApplicationUser> ApplicationUser { get; set; }
    }
}