using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RegistrationTaskMVC.Models
{
	[MetadataType(typeof(LoginMetadata))]
	public class LoginCreddentials
	{
	    public string userName { get; set; }
		public string password { get; set; }
	}
	public partial class LoginMetadata
	{
		[Required(ErrorMessage = "Please Enter UserName")]
		[Display(Name = "User Name")]
		public string userName { get; set; }

		[Required(ErrorMessage = "Please Enter Password")]
		[Display(Name = "Password")]
		public string password { get; set; }
	}
}