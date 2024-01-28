using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace LibraryProj.Models
{
    public class Book
    {
        public int BookId { get; set; }

        [Required(ErrorMessage = "Please enter a name.")]
        public string Name { get; set; } = string.Empty;

		[Required(ErrorMessage = "Please enter the book edition.")]
		public string Edition { get; set; } = string.Empty;

		[Required(ErrorMessage = "Please enter an author.")]
        public int AuthorId { get; set; } = int.MaxValue;

        [ValidateNever]

        public Author Author { get; set; } = null!;

    }
}