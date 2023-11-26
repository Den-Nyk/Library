using Library.Domains.BookAuthor;
using Microsoft.EntityFrameworkCore;

namespace Library.Data
{
    public class BookAutorDbContext : DbContext
    {
        private readonly IConfiguration configuration;

        public BookAutorDbContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Image> Images { get; set; } = null!;
        public DbSet<BookType> Types { get; set; } = null!;
        public DbSet<PublishingHouse> PublishingHouses { get; set; } = null!;
        public DbSet<Language> Languages { get; set; } = null!;
        public DbSet<MoreInfoAboutBook> MoreInfoAboutBooks { get; set; } = null!;
        public DbSet<Author> Authors { get; set; } = null!;
        public DbSet<BookAuthor> BookAuthors { get; set; } = null!;
        public DbSet<IsBookHearted> IsBooksHearted { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("BooksAuthorsDbContextConnection"));
        }
    }
}
