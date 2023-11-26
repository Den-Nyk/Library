using System.ComponentModel.DataAnnotations;

namespace Library.Domains.BookAuthor
{
    public class BookAuthor
    {
        [Key]
        public int Id { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
