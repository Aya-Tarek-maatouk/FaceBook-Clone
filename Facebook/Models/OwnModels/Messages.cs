using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Facebook.Models.OwnModels
{
    public class Messages
    {
        [Key]
        public int Message_ID { get; set; }
        [Required]
        [StringLength(200)]
        public string Message { get; set; }
        [Column(TypeName = "date")]
        public DateTime Mess_Date { get; set; }
        public bool Read { get; set; }
        public bool Deleted { get; set; }
        public virtual ApplicationUser UserSender { get; set; }
        public virtual ApplicationUser UserResiver { get; set; }

        public Messages()
        {
            Mess_Date = DateTime.Now;
            Read = false;
            Deleted = false;
        }

    }
}