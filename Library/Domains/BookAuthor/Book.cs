using Library.Domains.BookAuthor;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Library.Domains.BookAuthor
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required, PersonalData, MaxLength(128)]
        public string Name { get; set; }

        public int YearOfCreation { get; set; }

        public string? Description { get; set; }


        public ICollection<IsBookHearted> UserBookInteractions { get; set; } = null!;

        public ICollection<BookAuthor> BookAuthors { get; set; } = null!;

        public ICollection<Image> Images { get; set; } = null!;

        public ICollection<BookType> Types { get; set; } = null!;

        public ICollection<PublishingHouse> PublishingHouses { get; set; } = null!;
    }
}
