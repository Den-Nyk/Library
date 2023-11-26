using System.ComponentModel.DataAnnotations;

namespace Library.Domains.BookAuthor
{
    public class PublishingHouse
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required, MaxLength(128)]
        public string? PublishingHouseName { get; set; }

        public ICollection<Language> Languages { get; set; } = null!;
    }
}