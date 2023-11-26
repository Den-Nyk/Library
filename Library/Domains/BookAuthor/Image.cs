using System.ComponentModel.DataAnnotations;

namespace Library.Domains.BookAuthor
{
    public class Image
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required]
        public byte[] Img { get; set; }
    }
}