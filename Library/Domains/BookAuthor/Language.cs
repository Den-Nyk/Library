using System.ComponentModel.DataAnnotations;

namespace Library.Domains.BookAuthor
{
    public class Language
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PublishedHouseId { get; set; }

        [Required, MaxLength(128)]
        public string? LanguageName { get; set; }

        [Required]
        public int NumberOfPages { get; set; }

        [Required]
        public int YearOfPublication { get; set; }

        public MoreInfoAboutBook MoreInfo { get; set; } = null!;
    }
}