using Facebook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Facebook.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        string userId;
        public ProfileController() {
            userId =System.Web.HttpContext.Current.User.Identity.GetUserId();

        }
        ApplicationDbContext db = new ApplicationDbContext();
        // de 3ashn lw feh ay Object n3mlo Delete //
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        // GET: Profile
        public ActionResult Index()
        {
            userId = System.Web.HttpContext.Current.User.Identity.GetUserId();

            return View(db.Users.FirstOrDefault(o=>o.Id==userId));
        }
        #region Post
        [HttpGet]
        public ActionResult CreatePost()
        {
            userId = System.Web.HttpContext.Current.User.Identity.GetUserId();

            ViewBag.UserImage = (db.Users.FirstOrDefault(o => o.Id == userId)).image;
            return PartialView();
        }
        [HttpPost]
        public ActionResult CreatePost(Post post,bool Home)
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
         
            return RedirectToAction("GetAll_Posts");

        }
        public ActionResult GetAll_Posts()
        {
            List<Post> UserPosts = new List<Post>();
            UserPosts = db.post.Where(s => s.Deleted == false && s.UserId == userId).OrderByDescending(o=>o.Post_Date).ToList();
            //inner join with comments and likes corresponding  to this post
            return PartialView(UserPosts);
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
        
                return RedirectToAction("GetAll_Posts");
        }


        [HttpGet]
        public ActionResult DeletePost(int Id)
        {
            Post p = db.post.FirstOrDefault(pp => pp.PostId == Id);
            p.Deleted = true;
            db.SaveChanges();
          
                return RedirectToAction("GetAll_Posts");
        }
        #endregion

        #region Comment
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
                Id= userId
            });
            db.SaveChanges();
       
                return RedirectToAction("GetAll_Posts");
        }
        public ActionResult GetCommentByPostID(int PostID)
        {
            
           List<Comment>Comments=db.comment.Include("ApplicationUser").Where(o => o.Post_ID == PostID).OrderByDescending(o=>o.Comment_Date).Take(10).ToList();
            
            return PartialView(Comments);
        }

        public ActionResult Like(int PostId)
        {

            db.like.FirstOrDefault(s => s.PostID == PostId).LikeCounter+=1;
            db.SaveChanges();
         
                return RedirectToAction("GetAll_Posts");
        }
        #endregion

        #region UserBioInfo

        #region Skills
        [HttpPost]
        public ActionResult create_skill(string skill)
        {
            
            Skill s = new Skill()
            {
                skill = skill,
                UserId = userId
            };
            db.skill.Add(s);
            db.SaveChanges();
            List<Skill> l = db.skill.Where(m => m.UserId == userId).ToList();

            return PartialView("GetAllSkills",l);
        }
        [HttpPost]
        public ActionResult delete_skill(int id)
        {
            db.skill.Remove(db.skill.FirstOrDefault(m => m.ID == id ));
            db.SaveChanges();
            List<Skill> l = db.skill.Where(m => m.UserId == userId).ToList();

            return PartialView("GetAllSkills",l);
        }
        public ActionResult update_skill(int id)
        {
            var res = db.skill.FirstOrDefault(m => m.ID == id);
           
            return PartialView(res);
        }

        [HttpPost]
        public ActionResult updateskill(string skill,int id)
        {
            var res = db.skill.FirstOrDefault(m =>m.ID == id);
            res.skill = skill;
            db.SaveChanges();
            List<Skill> l = db.skill.Where(m => m.UserId == userId).ToList();

            return PartialView("GetAllSkills",l);
        }
        public ActionResult GetAllSkills()
        {
            List<Skill> l = db.skill.Where(m => m.UserId == userId).ToList();
            return PartialView(l);
        }
        #endregion

        #region Education
        public ActionResult CreateEducation(string EDU)
        {
            Education e = new Education()
            {
                UserId = userId,
                education = EDU
                
            };
            db.education.Add(e);
            db.SaveChanges();
            List<Education> l = db.education.Where(m => m.UserId == userId).ToList();

            return PartialView("GetAllEducation",l);
        }

        public ActionResult GetAllEducation()
        {
            List<Education> l = db.education.Where(m => m.UserId == userId).ToList();
            return PartialView(l);
        }

        public ActionResult delete_education(int id)
        {
            db.education.Remove(db.education.FirstOrDefault(m => m.EDUid == id ));
            db.SaveChanges();
            List<Education> edulist = db.education.Where(o => o.UserId == userId).ToList();
            return PartialView("GetAllEducation", edulist);
        }

        public ActionResult update_EDU(int id)
        {
            var res = db.education.FirstOrDefault(m => m.EDUid == id);

            return PartialView(res);
        }

        [HttpPost]
        public ActionResult updateEDU(string education, int id)
        {
            var res = db.education.FirstOrDefault(m => m.EDUid == id);
            res.education = education;
            db.SaveChanges();
            List<Education> edu = db.education.Where(o => o.UserId == userId).ToList();
            return PartialView("GetAllEducation", edu);
        }
        #endregion

        #region Work
        public ActionResult CreateWork(string work)
        {
            Work e = new Work()
            {
                UserId = userId,
                work = work
            };
            db.work.Add(e);
            db.SaveChanges();
            List<Work> l = db.work.Where(m => m.UserId == userId).ToList();

            return PartialView("GetAllWork",l);
        }

        public ActionResult GetAllWork()
        {
            List<Work> l = db.work.Where(m => m.UserId == userId).ToList();

            return PartialView(l);
        }
        public ActionResult delete_work(int id)
        {
            db.work.Remove(db.work.FirstOrDefault(m => m.id == id ));
            db.SaveChanges();
            List<Work> l = db.work.Where(m => m.UserId == userId).ToList();

            return PartialView("GetAllWork", l);
        }

        public ActionResult update_Work(int id)
        {
            var res = db.work.FirstOrDefault(m => m.id == id);

            return PartialView(res);
        }

        [HttpPost]
        public ActionResult updatework(string work, int id)
        {
            var res = db.work.FirstOrDefault(m => m.id == id);
            res.work = work;
            db.SaveChanges();
           List<Work> WorkList = db.work.Where(m => m.UserId == userId).ToList();
            return PartialView("GetAllWork", WorkList);
        }
        #endregion

        #endregion

    }

}