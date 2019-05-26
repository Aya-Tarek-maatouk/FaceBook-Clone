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
    public class HomeController : Controller
    {
        string userId;
        public HomeController()
        {
            userId = System.Web.HttpContext.Current.User.Identity.GetUserId();

        }
        public ActionResult Index()
        {
            userId = System.Web.HttpContext.Current.User.Identity.GetUserId();

            return View(db.Users.FirstOrDefault(o => o.Id == userId));
        }

   
        

        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Search(string SearchText)
        {
            var UsersList = db.Users.Where(U => U.Name.ToLower().Contains(SearchText.ToLower())).ToList();
            //var UsersList = db.Users.Where(U => U.Name.ToLower().Contains(SearchText.ToLower())).ToList();
            return PartialView(UsersList);
        }


        public ActionResult HomePosts()
        {
            var CurrentUserID = System.Web.HttpContext.Current.User.Identity.GetUserId();
            List<string> FriendID = (db.userFriend.Where(o => o.UserID == CurrentUserID)).Select(o => o.FriendID).ToList();
            FriendID.AddRange((db.userFriend.Where(o => o.FriendID == CurrentUserID)).Select(o => o.UserID).ToList());
            FriendID.Add(CurrentUserID);
            List<Post> UserPosts = new List<Post>();
            UserPosts = db.post.Where(s => s.Deleted == false && FriendID.Contains(s.UserId)).OrderByDescending(o => o.Post_Date).ToList();
            //inner join with comments and likes corresponding  to this post

            ViewBag.CurrentUserID = CurrentUserID;
            return PartialView(UserPosts);
        }

        [HttpGet]
        public ActionResult CreatePost()
        {
            userId = System.Web.HttpContext.Current.User.Identity.GetUserId();

            ViewBag.UserImage = (db.Users.FirstOrDefault(o => o.Id == userId)).image;
            return PartialView();
        }
        [HttpPost]
        public ActionResult CreatePost(Post post, bool Home)
        {
            if (ModelState.IsValid)
            {

                post.UserId = userId;

                db.post.Add(post);

                db.like.Add(new Like
                {
                    PostID = post.PostId,
                    LikeCounter = 0,
                    Id = userId
                });
                db.SaveChanges();
            }

            return RedirectToAction("HomePosts");

        }




        public ActionResult EditPost(int Id)
        {
            Post p = db.post.Include("ApplicationUser").FirstOrDefault(pp => pp.PostId == Id);
            return PartialView(p);
        }
        [HttpPost]
        public ActionResult EditPosts(Post post)
        {
            // lazem a3ml edit 3la kol Attributes maynf3sh a3ml object l2n kda hyb2a Reference //
            Post p = db.post.FirstOrDefault(pp => pp.PostId == post.PostId);
            p.Post_Body = post.Post_Body;
            //p.Post_Date = post.Post_Date;
            db.SaveChanges();

            return RedirectToAction("HomePosts");
        }


        [HttpGet]
        public ActionResult DeletePost(int Id)
        {
            Post p = db.post.FirstOrDefault(pp => pp.PostId == Id);
            p.Deleted = true;
            db.SaveChanges();
           return RedirectToAction("HomePosts");
        }

        [HttpGet]
        public ActionResult CreateComent(int postid)
        {

            ViewBag.PostID = postid;
            ViewBag.Image = (db.Users.FirstOrDefault(o => o.Id == userId)).image;
            return PartialView();
        }
        [HttpPost]
        public ActionResult CreateComment(int PostID, string Comment_Body)
        {
            db.comment.Add(new Comment()
            {
                Comment_Body = Comment_Body,
                Post_ID = PostID,
                Id = userId
            });
            db.SaveChanges();

            return RedirectToAction("HomePosts");
        }

        public ActionResult GetCommentByPostID(int PostID)
        {

            List<Comment> Comments = db.comment.Include("ApplicationUser").Where(o => o.Post_ID == PostID).OrderByDescending(o => o.Comment_Date).Take(10).ToList();

            return PartialView(Comments);
        }



    }
}