using Library.Context;
using Library.Domains;
using Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace Library.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        [HttpGet("GetBooksOfAllTime")]
        public IEnumerable<BookOfAllTime> GetBooksOfAllTime()
        {
            List<BookOfAllTime> books = new List<BookOfAllTime>();

            books.Add(new BookOfAllTime { Id = 0, Name = "David Copperfield", Img = System.IO.File.Exists("C:\\Users\\asd\\source\\.Net\\Library\\Library\\ClientApp\\src\\components\\HomePage\\Img\\DavidCopperfield.jpg") ? System.IO.File.ReadAllBytes("C:\\Users\\asd\\source\\.Net\\Library\\Library\\ClientApp\\src\\components\\HomePage\\Img\\DavidCopperfield.jpg") : null, isHearted = true });
            books.Add(new BookOfAllTime { Id = 1, Name = "The Little Prince", Img = System.IO.File.Exists("C:\\Users\\asd\\source\\.Net\\Library\\Library\\ClientApp\\src\\components\\HomePage\\Img\\aLittlePrince.jpg") ? System.IO.File.ReadAllBytes("C:\\Users\\asd\\source\\.Net\\Library\\Library\\ClientApp\\src\\components\\HomePage\\Img\\aLittlePrince.jpg") : null, isHearted = false });
            books.Add(new BookOfAllTime { Id = 2, Name = "Don Quixote", Img = System.IO.File.Exists("C:\\Users\\asd\\source\\.Net\\Library\\Library\\ClientApp\\src\\components\\HomePage\\Img\\DonQuixote.jpg") ? System.IO.File.ReadAllBytes("C:\\Users\\asd\\source\\.Net\\Library\\Library\\ClientApp\\src\\components\\HomePage\\Img\\DonQuixote.jpg") : null, isHearted = true });
            books.Add(new BookOfAllTime { Id = 3, Name = "Frankenstein", Img = System.IO.File.Exists("C:\\Users\\asd\\source\\.Net\\Library\\Library\\ClientApp\\src\\components\\HomePage\\Img\\Frankenstain.jpg") ? System.IO.File.ReadAllBytes("C:\\Users\\asd\\source\\.Net\\Library\\Library\\ClientApp\\src\\components\\HomePage\\Img\\Frankenstain.jpg") : null, isHearted = false });
            books.Add(new BookOfAllTime { Id = 4, Name = "Harry Potter", Img = System.IO.File.Exists("C:\\Users\\asd\\source\\.Net\\Library\\Library\\ClientApp\\src\\components\\HomePage\\Img\\HarryPotter.jpg") ? System.IO.File.ReadAllBytes("C:\\Users\\asd\\source\\.Net\\Library\\Library\\ClientApp\\src\\components\\HomePage\\Img\\HarryPotter.jpg") : null, isHearted = true });
            books.Add(new BookOfAllTime { Id = 5, Name = "Hunger Games", Img = System.IO.File.Exists("C:\\Users\\asd\\source\\.Net\\Library\\Library\\ClientApp\\src\\components\\HomePage\\Img\\hungreGames.jpg") ? System.IO.File.ReadAllBytes("C:\\Users\\asd\\source\\.Net\\Library\\Library\\ClientApp\\src\\components\\HomePage\\Img\\hungreGames.jpg") : null, isHearted = false });
            books.Add(new BookOfAllTime { Id = 6, Name = "King Arthur", Img = System.IO.File.Exists("C:\\Users\\asd\\source\\.Net\\Library\\Library\\ClientApp\\src\\components\\HomePage\\Img\\KingArtur.jpg") ? System.IO.File.ReadAllBytes("C:\\Users\\asd\\source\\.Net\\Library\\Library\\ClientApp\\src\\components\\HomePage\\Img\\KingArtur.jpg") : null, isHearted = true });
            books.Add(new BookOfAllTime { Id = 7, Name = "The Great Gatsby", Img = System.IO.File.Exists("C:\\Users\\asd\\source\\.Net\\Library\\Library\\ClientApp\\src\\components\\HomePage\\Img\\TheGreatHutsby.jpg") ? System.IO.File.ReadAllBytes("C:\\Users\\asd\\source\\.Net\\Library\\Library\\ClientApp\\src\\components\\HomePage\\Img\\TheGreatHutsby.jpg") : null, isHearted = true });

            return books;
        }

        [HttpPost("UpdateHeartedStatus")]
        public IActionResult UpdateHeartedStatus([FromBody] UpdateHeartedStatusModel updateModel)
        {
            /*var userBook = _context.UserBooks.FirstOrDefault(ub => ub.Email == updateModel.Email && ub.BookId == updateModel.BookId);

            if (userBook != null)
            {
                userBook.IsHearted = updateModel.IsHearted;
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }*/
            return Ok();
        }


    }
}
