using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Library.Domains.BookAuthor
{
    public class MoreInfoAboutBook
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int LanguageId { get; set; }

        [Required, MaxLength(128)]
        public string? TypeOfCover { get; set; }

        [Required, MaxLength(128)]
        public string? Format { get; set; }

        [Required]
        public bool Illustrations { get; set; }

        [Required]
        public int Weight { get; set; }
    }
}