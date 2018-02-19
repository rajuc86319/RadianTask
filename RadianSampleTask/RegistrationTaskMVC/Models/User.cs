using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace RegistrationTaskMVC.Models

{[MetadataType(typeof(UserMetadata))]
	public class User
	{
		public string LastName { get; set; }
		public string MiddleName { get; set; }
		public string FirstName { get; set; }
		public string Country { get; set; }
		public string State { get; set; }
		public string EmailAddress { get; set; }
		[Remote("IsUserNameExist", "Validation", ErrorMessage = "User Name already Exist")]
		public string UserName { get; set; }
		public string Password { get; set; }


	}
	public class UserMetadata
	{
		[Required(ErrorMessage = "Please Enter Last Name")]
		[Display(Name = "Last Name")]
		public string LastName { get; set; }
		[Display(Name = "Middle Name")]
		
		public string MiddleName { get; set; }
		[Display(Name = "First Name")]
		[Required(ErrorMessage = "Please Enter First Name")]
		public string FirstName { get; set; }
		[Required(ErrorMessage = "Please Enter Country")]
		public string Country { get; set; }
		[Required(ErrorMessage = "Please Enter state")]
		public string State { get; set; }
		[Required(ErrorMessage = "Please Enter email")]
		[Display(Name = "Email")]
		public string EmailAddress { get; set; }
		[Required(ErrorMessage = "Please Enter password")]
		public string Password { get; set; }

		[Required(ErrorMessage = "Please Enter User Name")]
		[Display(Name = "User Name")]
		public string UserName { get; set; }

	}
}