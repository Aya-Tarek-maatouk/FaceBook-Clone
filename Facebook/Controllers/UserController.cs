using Facebook.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Facebook.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        List<Education> UserEducations = new List<Education>();
        List<Skill> UserSkills = new List<Skill>();
        List<Work> UserWorks = new List<Work>();
        List<Post> UserPosts = new List<Post>();
        ApplicationUser CurrentUser = new ApplicationUser();


        public ActionResult ShowFriendProfile(string FriendId)
        {
            bool Confirm = false;
            bool Sended = false;
            Session["friendid"] = FriendId;
            string CurrenUserID = System.Web.HttpContext.Current.User.Identity.GetUserId();
            ViewBag.currentID = (db.Users.FirstOrDefault(o => o.Id == CurrenUserID)).Id;
            var InFriendRequest = db.FriendRequests.FirstOrDefault(f => f.UserID == CurrenUserID && f.ReqFriendID==FriendId);
            var InFriendRequest2 = db.FriendRequests.FirstOrDefault(f => f.UserID == FriendId && f.ReqFriendID== CurrenUserID);
            if (InFriendRequest != null)
            {
                Confirm = InFriendRequest.Confirm;
                Sended = InFriendRequest.Sended;
            }
            else if (InFriendRequest2 != null)
            {
                Confirm = InFriendRequest2.Confirm;
                Sended = InFriendRequest2.Sended;
            }
            if (Sended == true && Confirm == false)
            {
                ViewBag.btnAddFriendTxt = "Responding";
            }
            else if (Sended == true && Confirm == true)
            {
                ViewBag.btnAddFriendTxt = "Friends";
            }
            else if (Sended == false && Confirm == false)
            {
                ViewBag.btnAddFriendTxt = "AddFriend";
            }
                return View(db.Users.FirstOrDefault(F => F.Id == FriendId));
        }
        public ActionResult GetallFriendPosts()
        {

            string FriendID = Session["friendid"].ToString();
            UserPosts = db.post.Where(p => p.UserId == FriendID && p.Deleted == false).OrderByDescending(o => o.Post_Date).ToList();
            ViewBag.currentUser =CurrentUser;
            return PartialView(UserPosts);
        }

        public ActionResult GetAllWork()
        {

            string FriendID = Session["friendid"].ToString();
          UserWorks = db.work.Where(w => w.UserId == FriendID).ToList();

            return PartialView(UserWorks);
        }

        public ActionResult GetAllEducation()
        {
            string FriendID = Session["friendid"].ToString();
            UserEducations = db.education.Where(e => e.UserId == FriendID).ToList();

            return PartialView(UserEducations);
        }

        public ActionResult GetAllSkills()
        {
            string FriendID = Session["friendid"].ToString();

            UserSkills = db.skill.Where(s => s.UserId == FriendID).ToList();
            return PartialView(UserSkills);
        }

        public ActionResult CreateComent(int postid)
        {
            ViewBag.PostID = postid;
            string uId = System.Web.HttpContext.Current.User.Identity.GetUserId();

            ViewBag.Image = (db.Users.FirstOrDefault(o => o.Id == uId)).image;
            return PartialView();
        }
        [HttpPost]
        public ActionResult CreateComment(int PostID, string Comment_Body)
        {
            string uId = System.Web.HttpContext.Current.User.Identity.GetUserId();


            db.comment.Add(new Comment()
            {
                Comment_Body = Comment_Body,
                Post_ID = PostID,
                Id = uId
            });
            db.SaveChanges();
            return RedirectToAction("GetallFriendPosts");
        }
        [HttpPost]
        public ActionResult AddFriendRequest(string user,string UserFriend)
        {
            FriendRequest friendRequest = new FriendRequest();
            friendRequest.UserID = user;
            friendRequest.ReqFriendID = UserFriend;
            friendRequest.Sended = true;
            friendRequest.Confirm = false;
            db.FriendRequests.Add(friendRequest);
            db.SaveChanges();
            return View("ShowFriendProfile");
        }

        public ActionResult allFreindRequests(string user)
        {
            List<string> Requests = (db.FriendRequests.Where(o => o.ReqFriendID == user && o.Confirm == false && o.deleted == false).Select(o => o.UserID)).ToList();
            List<ApplicationUser> UserWhoSentTheRequest = db.Users.Where(o => Requests.Contains(o.Id)).ToList();
            return PartialView(UserWhoSentTheRequest);
            
        }

        public ActionResult AcceptFriend(string userID, string FriendID)
        {
            FriendRequest Requests = db.FriendRequests.FirstOrDefault(o => o.ReqFriendID == userID && o.UserID == FriendID && o.Confirm == false);
            Requests.Confirm = true;
            Requests.Sended = true;

            db.userFriend.Add(new UserFriend()
            {
                UserID = userID,
                FriendID = FriendID,
                Friends = true
            });
            db.SaveChanges();
            return RedirectToAction("allFreindRequests");
        }

        public ActionResult RejectFriendRequest(string userID, string FriendID)
        {
            FriendRequest Requests = db.FriendRequests.FirstOrDefault(o => o.ReqFriendID == userID && o.UserID == FriendID && o.Confirm == false);
            Requests.deleted = true;
            Requests.Confirm = false;
            Requests.Sended = false;
            db.SaveChanges();
            return RedirectToAction("allFreindRequests", routeValues: new { user = userID });
        }

    }
}
