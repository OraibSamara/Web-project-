using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace LibraryProj.Models
{
    public class User
    {
        public int UserId { get; set; }

		[Required(ErrorMessage = "Please enter a name.")]
		public string Name { get; set; } = string.Empty;

		[Required(ErrorMessage = "Please enter a username.")]
        public string Username { get; set; } = string.Empty;

		[Required(ErrorMessage = "Please enter a password.")]
		public string Password { get; set; } = string.Empty;

	}
}