using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Facebook.Models.OwnModels
{
    public class UserConnection
    {
        [Key]
        public string ConnectionID { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}