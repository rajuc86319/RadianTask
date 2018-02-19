using RegistrationTaskMVC.Models;
using System;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Collections.Generic;
using System.Linq;

namespace RegistrationTaskMVC.Controllers
{
	public class RegisterController : Controller
	{
		// GET: Register
		public ActionResult RegisterUser()
		{
			IEnumerable<Countries> countries = GetAllCountries();
			var query = countries.Select(c => new { c.countryId, c.countryName });
			ViewBag.Countries = new SelectList(query.AsEnumerable(), "countryId", "countryName");

			return View();
		}
		[HttpPost]
		public ActionResult RegisterUser(User U)
		{
			int id = Convert.ToInt32(U.Country);
			Countries country = GetCountrybyId(id);
			U.Country = country.countryName;
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri("http://localhost:52844/api/users");

				//HTTP POST
				var postTask = client.PostAsJsonAsync<User>("Users", U);
				postTask.Wait();

				var result = postTask.Result;
				if (result.IsSuccessStatusCode)
				{
					return View("UserDetails", U);
				}
			}


			return View("UserDetails", U);

		}
		[NonAction]
		IEnumerable<Countries> GetAllCountries()
		{
			IEnumerable<Countries> users = new List<Countries>();
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri("http://localhost:52844/api/");
				//HTTP GET
				var responseTask = client.GetAsync("countries");
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<IList<Countries>>();
					readTask.Wait();

					users = readTask.Result;
				}
				else //web api sent error response 
				{
					//log response status here..

					users = Enumerable.Empty<Countries>();

					//ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
				}
				return users;
			}
		}
		[NonAction]
		Countries GetCountrybyId(int id)
		{
			Countries contrybyID = new Countries();
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri("http://localhost:52844/api/");
				//HTTP GET
				var responseTask = client.GetAsync("countries/"+id);
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<Countries>();
					readTask.Wait();

					contrybyID = readTask.Result;
				}
				else //web api sent error response 
				{
					//log response status here..

					contrybyID = new Countries();

					//ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
				}
				return contrybyID;
			}
		}




	}
}