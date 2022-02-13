using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibApp.Models
{
    public class Book
    {
        public int Id { get; set; }
		[Required(ErrorMessage = "Please enter name")]
		[StringLength(255)]
		public string Name { get; set; }
		[Required(ErrorMessage = "Please enter author name")]
		public string AuthorName { get; set; }
		public Genre Genre { get; set; }
		[Display(Name = "Genre")]
		[Required(ErrorMessage = "Please choose genre")]
		public byte GenreId { get; set; }
		public DateTime DateAdded { get; set; }
		[Display(Name = "Release Date")]
		[Required(ErrorMessage = "Please enter release date")]
		public DateTime? ReleaseDate { get; set; }
		[Display(Name = "Number in Stock")]
		[Range(1, 20, ErrorMessage = "Range must be between 1 and 20")]
		[Required(ErrorMessage = "Please enter number")]
		public int? NumberInStock { get; set; }
		public int NumberAvailable { get; set; }
	}
      
}
//[Range(1, byte.MaxValue, ErrorMessage = "Please choose genre")]
