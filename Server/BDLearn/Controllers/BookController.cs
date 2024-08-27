using BDLearn.Models;
using BDLearn.response;
using LibraryBLL;
using Microsoft.AspNetCore.Mvc;

namespace BDLearn.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        readonly IConfiguration _configuration;
        readonly AppDbContext context;
        public BookController(AppDbContext _context, IConfiguration configuration) { context = _context; _configuration = configuration; }

        [HttpPost("addbook")]
        public async Task<IActionResult> AddBook(BookModel _bookM)
        {
            if (_bookM.IdBook == null && _bookM.IdUser == null) { return NotFound(new { message = "Id == Null" }); }
            try
            {
                var user = await context.User.FindAsync(Guid.Parse(_bookM.IdUser));
                if (user != null) {
                    if(!user.Books.Any(x => x == _bookM.IdBook))
                    {
                        user.Books.Add(_bookM.IdBook);
                        await context.SaveChangesAsync();
                    }
                    return Ok();
                }
                return NotFound(new { message = "User == null" });
            }
            catch (Exception ex) {

                return NotFound(ex.Message);
            }
        }

        [HttpPost("deletebook")]
        public async Task<IActionResult> DeleteBook(BookModel _bookM)
        {
            if (_bookM.IdBook == null && _bookM.IdUser == null) { return NotFound(new { message = "Id == Null" }); }
            try
            {
                var user = await context.User.FindAsync(Guid.Parse(_bookM.IdUser));
                if (user != null)
                {
                    if (user.Books.Any(x => x == _bookM.IdBook))
                    {
                        user.Books.Remove(_bookM.IdBook);
                        await context.SaveChangesAsync();
                    }
                    return Ok();
                }
                return NotFound(new { message = "User == null" });
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
        }

        [HttpGet("showuserbook")]
        public async Task<IActionResult> ShowAllUserBook([FromQuery] string token)
        {
            if (token == null) { return NotFound(new { message = "Id == Null" }); }
            try
            {
                var user = await context.User.FindAsync(Guid.Parse(new JWT().GetUserIdFromToken(token)));
                if (user != null)
                {
                    return Ok(user.Books);
                }
                return NotFound(new { message = "User == null" });
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
        }

        [HttpGet("showbook")]
        public async Task<IActionResult> ShowAllBook([FromQuery] string token, [FromQuery] int IdScroll)
        {
            if (token == null && IdScroll == null) { return NotFound(new { message = "Id == Null" }); }
            try
            {
                var user = await context.User.FindAsync(Guid.Parse(new JWT().GetUserIdFromToken(token)));
                if (user != null)
                {
                    string apiKey = _configuration["GoogleBook:key"];
                    string url = $"https://www.googleapis.com/books/v1/volumes?q=flowers&orderBy=relevance&maxResults=10&startIndex={IdScroll}&key={apiKey}";
                    var booksResponse = await new BooksRes().GetBooks(url);
                    var filteredBooks = new Recommendations().FilterBooksAsync(booksResponse.Items, user.Books);
                    return Ok(filteredBooks);
                }
                return NotFound(new { message = "User == null" });
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
        }
    }
}
