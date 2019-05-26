using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Facebook.Models
{
    public class FriendRequest
    {
        public FriendRequest()
        {
            Sended = false;
            deleted = false;
            Confirm = false;
            ApplicationUser = new HashSet<ApplicationUser>();
        }
        [Key, Column(Order = 0)]
        [ForeignKey("ApplicationUser")]
        public string UserID { get; set; }
        [Key, Column(Order = 1)]
        [ForeignKey("ApplicationUser")]
        public string ReqFriendID { get; set; }
        public virtual ICollection<ApplicationUser> ApplicationUser { get; set; }
        public bool Confirm { get; set; }
        public bool Sended { get; set; }

        public bool deleted { get; set; }

    }
}