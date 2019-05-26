using Facebook.Models;
using Facebook.Models.OwnModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Facebook.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        ApplicationDbContext DB = new ApplicationDbContext();
        protected override void Dispose(bool disposing)
        {
            DB.Dispose();
            base.Dispose(disposing);
        }
        MessageViewObject Messageinfo;
        //will be changed with the identity
        ApplicationUser Current = new ApplicationUser();
        public ChatController()
        {


            var ID = System.Web.HttpContext.Current.User.Identity.GetUserId();
            Current = DB.Users.FirstOrDefault(o => o.Id == ID);
        }
        public ActionResult Index()
        {

            return View(Current);
        }

        public ActionResult GetAllContacts()
        {

            List<string> FriendID = (DB.userFriend.Where(o => o.UserID == Current.Id)).Select(o => o.FriendID).ToList();
            FriendID.AddRange((DB.userFriend.Where(o => o.FriendID == Current.Id)).Select(o => o.UserID).ToList());
            List<ApplicationUser> Friends = DB.Users.Where(o => FriendID.Contains(o.Id)).ToList();
            return PartialView(Friends);
        }

        public ActionResult MessageChat(string FriendID)
        {

            List<Messages> mess = DB.Messages.Where(M => ((M.UserResiver.Id == Current.Id) && (M.UserSender.Id == FriendID)) || ((M.UserResiver.Id == FriendID) && (M.UserSender.Id == Current.Id))).OrderByDescending(o => o.Mess_Date).Take(10).ToList();

            ApplicationUser FriendCurr = DB.Users.FirstOrDefault(o => o.Id == FriendID);
            List<Messageinfo> MInfo = new List<Messageinfo>();

            for (int i = 0; i < mess.Count(); i++)
            {
                MInfo.Add(new Messageinfo()
                {
                    Sender = mess[i].UserSender,
                    Resever = mess[i].UserResiver,
                    MessageTxt = mess[i].Message
                });
            }

            Messageinfo = (new MessageViewObject()
            {
                CurrentUser = Current,
                CurrentFriend = FriendCurr,
                MessageSinfo = MInfo
            });

            return PartialView(Messageinfo);
        }
    }
}