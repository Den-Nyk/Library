using Library.Context;
using Library.Data;
using Library.Domains;
using Library.Domains.BookAuthor;
using Library.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace Library.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly UserManager<UserDomain> _userManager;
        private readonly BookAutorDbContext _context;

        public BooksController(
            UserManager<UserDomain> userManager,
            BookAutorDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        private readonly List<string> BooksOfAllTimes = new List<string> { "Penguin Readers. Level 5. David Copperfield", "The Little Prince", "Don Quixote", "Убивство на полі для гольфу", "Гаррі Поттер і філософський камінь", "Голодні ігри. У 3 книгах. Книга 1. Голодні ігри", "King Arthur Stories", "The Great Gatsby" };

        [HttpGet("GetBooksOfAllTime")]
        public async Task<IEnumerable<BookShowModel>> GetBooksOfAllTime()
        {
            List<BookShowModel> books = new List<BookShowModel>();

            bool userIsAuthenticated = false;
            UserDomain user = new UserDomain();
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                userIsAuthenticated = true;
                user = await _userManager.GetUserAsync(User);
            }
            foreach (var book in BooksOfAllTimes)
            {
                Book existingBook = _context.Books.FirstOrDefault(b => b.Name == book);

                if (existingBook != null)
                {
                    var existImg = _context.Images.FirstOrDefault(i => i.BookId == existingBook.Id);
                    if (existImg != null)
                    {
                        try
                        {
                            bool isBookHearted = false;
                            if (userIsAuthenticated)
                            {
                                var BookHearted = _context.IsBooksHearted.FirstOrDefault(b => b.UserEmail == user.Email && b.BookId == existingBook.Id);
                                if (BookHearted != null)
                                {
                                    isBookHearted = BookHearted.IsHearted;
                                }
                            }

                            books.Add(new BookShowModel
                            {
                                Id = existingBook.Id,
                                Img = existImg.Img,
                                Name = existingBook.Name,
                                isHearted = isBookHearted
                            });
                        }
                        catch (Exception ex)
                        {
                            return new List<BookShowModel>();
                        }
                    }
                }
            }

            return books;
        }

        [HttpGet("GetDependBook")]
        public async Task<BookAllDataModel> GetDependBook(int BookId)
        {
            BookAllDataModel book = new BookAllDataModel();
            var existBook = _context.Books.FirstOrDefault(b => b.Id == BookId);
            if (existBook != null)
            {
                book.Id = existBook.Id;
                book.Name = existBook.Name;
                book.YearOfCreation = existBook.YearOfCreation;
                book.Description = existBook.Description != null ? existBook.Description : "No description";
                book.LinkToYaBook = existBook.LinkToYaBook;
            }
            else return null;

            bool isBookHearted = false;
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                var BookHearted = _context.IsBooksHearted.FirstOrDefault(b => b.UserEmail == user.Email && b.BookId == book.Id);
                if (BookHearted != null)
                {
                    isBookHearted = BookHearted.IsHearted;
                }
                book.isHearted = isBookHearted;
            }

            var types = _context.Types.Where(t => t.BookId == BookId).Select(t => t.TypeName).ToList();
            book.Types = new List<string>();
            foreach (var type in types)
            {
                book.Types.Add(type.ToString().ToLower());
            }

            var imgs = _context.Images.Where(i => i.BookId == BookId).Select(i => i.Img).ToList();
            book.Imgs = new List<byte[]>();
            foreach (var img in imgs)
            {
                book.Imgs.Add(img);
            }

            var AuthorId = _context.BookAuthors.FirstOrDefault(a => a.BookId == BookId);
            if (AuthorId != null)
            {
                var Author = _context.Authors.FirstOrDefault(Author => Author.Id == AuthorId.AuthorId);
                if (Author != null)
                {
                    book.AuthorId = Author.Id;
                    book.Author = Author.Name;
                    book.AuthorLinkToYaBook = Author.LinkToYaBook;
                    book.AuthorDescription = Author.Description;
                }
            }

            var PublishingHouses = _context.PublishingHouses.Where(p => p.BookId == BookId).ToList();
            book.PublishingHouses = new List<string>();
            foreach (var publishingHouse in PublishingHouses)
            {
                book.PublishingHouses.Add(publishingHouse.PublishingHouseName);

                var Languages = _context.Languages.Where(l => l.PublishingHouseId == publishingHouse.Id).ToList();
                book.Languages = new List<string>();
                book.YearsOfPublication = new List<int>();
                foreach (var language in Languages)
                {
                    if (!book.Languages.Contains(language.LanguageName))
                        book.Languages.Add(language.LanguageName);

                    if (!book.YearsOfPublication.Contains(language.YearOfPublication))
                        book.YearsOfPublication.Add(language.YearOfPublication);
                }
                if (Languages.Count > 0)
                {
                    var moreInfo = _context.MoreInfoAboutBooks.FirstOrDefault(m => m.LanguageId == Languages[0].Id);
                    if (moreInfo != null)
                    {
                        book.Weight = moreInfo.Weight;
                        book.Illustrations = moreInfo.Illustrations;
                        book.Format = moreInfo.Format;
                        book.BookType = moreInfo.TypeOfCover;
                    }
                }
            }

            return book;
        }

        [Authorize]
        [HttpPost("UpdateHeartedStatus")]
        public async Task<IActionResult> UpdateHeartedStatus([FromBody] UpdateHeartedStatusModel updateModel)
        {
            var user = await _userManager.GetUserAsync(User);
            var BookHearted = _context.IsBooksHearted.FirstOrDefault(b => b.UserEmail == user.Email && b.BookId == updateModel.BookId);
            if (BookHearted != null)
            {
                BookHearted.IsHearted = updateModel.isHearted;
                _context.SaveChanges();
                return Ok("Book was changed.");
            }
            else
            {
                var book = _context.Books.FirstOrDefault(b => b.Id == updateModel.BookId);
                if (book != null)
                {
                    var isBookHearted = new IsBookHearted
                    {
                        Book = book,
                        BookId = book.Id,
                        IsHearted = updateModel.isHearted,
                        UserEmail = user.Email
                    };
                    _context.IsBooksHearted.Add(isBookHearted);
                    book.IsBooksHearted.Add(isBookHearted);
                    _context.SaveChanges();
                    return Ok("Book was add and changed");
                }
                else
                {
                    return BadRequest(new { error = "Error: no such book" });
                }
            }
        }

        [HttpGet("GetBooks")]
        public async Task<BookListResponse> GetBooksToShow(string page, string title)
        {
            if (title == "nothing")
            {
                title = "";
            }
            int pageInt = Int32.Parse(page);
            int pageSize = 9;
            int skipCount = (pageInt - 1) * pageSize;

            bool userIsAuthenticated = false;
            UserDomain user = new UserDomain();
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                userIsAuthenticated = true;
                user = await _userManager.GetUserAsync(User);
            }

            var bookModels = await _context.Books
                .Where(b => b.Name.Contains(title))
                .Skip(skipCount)
                .Take(pageSize)
                .ToListAsync();

            BookListResponse booksResponse = new BookListResponse
            {
                Books = new List<BookShowModel>(),
                IsLastPage = false,
            };

            var lastBookId = _context.Books
                .OrderByDescending(book => book.Id)
                .Select(book => book.Id)
                .FirstOrDefault();

            foreach (var book in bookModels)
            {
                var existImg = _context.Images.FirstOrDefault(i => i.BookId == book.Id);
                if (existImg != null)
                {
                    try
                    {
                        bool isBookHearted = false;
                        if (userIsAuthenticated)
                        {
                            var BookHearted = _context.IsBooksHearted.FirstOrDefault(b => b.UserEmail == user.Email && b.BookId == book.Id);
                            if (BookHearted != null)
                            {
                                isBookHearted = BookHearted.IsHearted;
                            }
                        }

                        var bookModel = new BookShowModel
                        {
                            Id = book.Id,
                            Img = existImg.Img,
                            Name = book.Name,
                            isHearted = isBookHearted
                        };
                        if (bookModel.Id == lastBookId)
                            booksResponse.IsLastPage = true;

                        booksResponse.Books.Add(bookModel);
                    }
                    catch (Exception ex)
                    {
                        return booksResponse;
                    }
                }
            }

            return booksResponse;
        }

        [HttpGet("Search")]
        public ActionResult<IEnumerable<Book>> Search(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return BadRequest("Search query cannot be empty.");
            }

            // Simulating a basic case-insensitive search based on the book name.
            var filteredBooks = _context.Books
            .Where(book => EF.Functions.Like(book.Name, $"%{query}%")) // Like for case-insensitive search
            .ToList();

            return Ok(filteredBooks);
        }
    }
}
