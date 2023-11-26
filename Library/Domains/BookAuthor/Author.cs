using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Library.Domains.BookAuthor
{
    public class Author
    {
        [Required]
        public int Id { get; set; }

        [Required, PersonalData, MaxLength(128)]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime DateOfBirthday { get; set; }


        public ICollection<BookAuthor> BookAuthors { get; set; } = null!;

    }
}