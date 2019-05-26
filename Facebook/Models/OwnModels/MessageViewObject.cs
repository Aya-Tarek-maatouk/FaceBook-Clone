using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Facebook.Models.OwnModels
{
   
  public class MessageViewObject
    {
        public ApplicationUser CurrentFriend { set; get; }
        public ApplicationUser CurrentUser { set; get; }

        public List<Messageinfo> MessageSinfo { set; get; }

    }
    public class Messageinfo
    {

        public ApplicationUser Sender { get; set; }
        public ApplicationUser Resever { get; set; }
        public string MessageTxt { get; set; }

    }
}