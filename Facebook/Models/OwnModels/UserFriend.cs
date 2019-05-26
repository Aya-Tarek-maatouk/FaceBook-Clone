using Facebook.Models.OwnModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Facebook.Models
{
    public class UserFriend
    {
        public UserFriend()
        {
            Deleted = false;
            ApplicationUser = new HashSet<ApplicationUser>();
        }
        [Key, Column(Order = 0)]
        public string UserID { get; set; }
        [Key, Column(Order = 1)]
        public string FriendID { get; set; }
        public virtual ICollection<ApplicationUser> ApplicationUser { get; set; }
        public bool Deleted { get; set; }
        public bool Friends { get; set; }

        
        //public virtual ApplicationUser RequestedBy { get; set; }
        //public virtual ApplicationUser RequestedTo { get; set; }
        // public FriendRequestFlag FriendRequestFlag { get; set; }

        //public bool Approved => FriendRequestFlag == FriendRequestFlag.Approved;

    }
}