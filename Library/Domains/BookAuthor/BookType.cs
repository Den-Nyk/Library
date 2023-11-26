using System.ComponentModel.DataAnnotations;

namespace Library.Domains.BookAuthor
{
    public class BookType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required, MaxLength(128)]
        public string? TypeName { get; set; }
    }
}
