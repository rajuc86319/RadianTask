using RegistrationTaskMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RegistrationTaskMVC.Controllers
{
    public class ValidationController : Controller
    {
        // GET: Validation
        public ActionResult Index()
        {
            return View();
        }

		[HttpGet]
		public JsonResult IsUserNameExist(string UserName)
		{
			IEnumerable<User> users = LoginController.GetAllUsers();
			bool isExist = users.Where(u => u.UserName == UserName).Any();
			return Json(!isExist, JsonRequestBehavior.AllowGet);
		}
	}
}