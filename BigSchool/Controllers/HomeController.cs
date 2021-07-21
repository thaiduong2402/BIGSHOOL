using BigSchool.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BigSchool.Controllers
{
    public class HomeController : Controller
    {
        BigSchoolContext context = new BigSchoolContext();
        public ActionResult Index()
        {
            var upcommingCourse = context.Courses.Where(x => x.DateTime > DateTime.Now).OrderBy(x => x.DateTime).ToList();
            var userID = User.Identity.GetUserId();
            foreach (Course item in upcommingCourse)
            {
                ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().
                    GetUserManager<ApplicationUserManager>().FindById(item.LecturerId);
                item.Name = user.Name;

                if (userID != null)
                {
                    item.isLogin = true;
                    Attendance find = context.Attendances.FirstOrDefault(p => p.CourseID == item.Id && p.Attendee == userID);
                    if (find == null)
                    {
                        item.isShowGoing = true;
                    }
                    Following findFollow = context.Followings.FirstOrDefault(p => p.FollowerId == userID && p.FolloweeId == item.LecturerId);
                    if (findFollow == null)
                    {
                        item.isShowFollow = true;
                    }
                }
            }

            return View(upcommingCourse);
        }
    }
}