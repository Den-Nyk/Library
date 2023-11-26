using Library.Domains.BookAuthor;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Library.Domains.BookAuthor
{
    public class IsBookHearted
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserEmail { get; set; }

        [Required]
        public int BookId { get; set; }
        public Book Book { get; set; }

        [Required]
        public bool IsHearted { get; set; }
    }
}
