using RegistrationTaskMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace RegistrationTaskMVC.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
		[HttpPost]
		public ActionResult ValidateLoginDetails(LoginCreddentials ld)
		{
				bool userValidation = userExist(ld.userName,ld.password);
			if (userValidation) {
				return View("SuccessfullLogin");
			}
			else 
			   return View("UnSuccessFullLogin");
		}
		[NonAction]
		bool userExist(string uname,string password)
		{
			bool userValidity=true;
			IEnumerable<User> users = GetAllUsers();
			
			userValidity=users.Where(u => u.UserName == uname && u.Password == password).Any();
			return userValidity;
		}
		[NonAction]
		public static IEnumerable<User> GetAllUsers()
		{
			IEnumerable<User> users = new List<User>();
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri("http://localhost:52844/api/");
				//HTTP GET
				var responseTask = client.GetAsync("Users");
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<IList<User>>();
					readTask.Wait();

					users = readTask.Result;
				}
				else //web api sent error response 
				{
					//log response status here..

					users = Enumerable.Empty<User>();

					//ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
				}

			}
			return users;
		}


	}
}