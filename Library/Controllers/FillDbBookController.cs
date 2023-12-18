using Library.Data;
using Library.Models;
using Library.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FillDbBookController : Controller
    {
        private readonly BookAutorDbContext _context;

        public FillDbBookController(BookAutorDbContext context)
        {
            _context = context;
        }

        [HttpPost("FillByYaBookUrl")]
        public async Task<IActionResult> FillByYaBookUrlAsync([FromBody] Url request)
        {
            if (!Uri.TryCreate(request.RequestData, UriKind.Absolute, out _))
            {
                return BadRequest(new { error = "The provided data is not a valid URL." });
            }

            try
            {
                await FillDbBookByUrlFromYaBook.FillDbByYaBookUrl(request.RequestData, this._context);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Error: " + ex.ToString() });
            }

            return Ok("Data received successfully");
        }
    }
}
