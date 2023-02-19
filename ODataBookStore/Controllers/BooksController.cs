using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using ODataBookStore.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ODataBookStore.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BooksController : ODataController
    {

        private BookStoreContext bookStoreContext;

        public BooksController(BookStoreContext bookStoreContext)
        {
            this.bookStoreContext = bookStoreContext;
            bookStoreContext.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking;
            if (bookStoreContext.Books == null || bookStoreContext.Books.Count() == 0)
            {
                foreach (var b in DataSource.BookDataSource.GetBooks())
                {
                    bookStoreContext.Books.Add(b);
                    bookStoreContext.Presses.Add(b.Press);
                }
                bookStoreContext.SaveChanges();
            }
        }



        // GET: api/<BooksController>
        [EnableQuery(PageSize = 6)]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(bookStoreContext.Books);
        }

        [EnableQuery]
        [HttpGet("id")]
        // GET api/<BooksController>/5
        public IActionResult Get(int id)
        {
            var book = bookStoreContext.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [EnableQuery]
        [HttpPost]
        // POST api/<BooksController>
        public IActionResult Post([FromBody] Book book)
        {
            if(book == null)
            {
                return NotFound();
            }
            bookStoreContext.Books.Add(book);
            bookStoreContext.SaveChanges();
            return Created(book);
        }

        [EnableQuery]
        [HttpPut]
        // POST api/<BooksController>
        public IActionResult Put([FromBody] Book book)
        {
            if (book == null)
            {
                return NotFound();
            }
            bookStoreContext.Books.Update(book);
            bookStoreContext.SaveChanges();
            return Ok(book);
        }

        [EnableQuery]
        [HttpDelete("id")]

        // DELETE api/<BooksController>/5
        public IActionResult Delete(int id)
        {
            Book book = bookStoreContext.Books.FirstOrDefault(b => b.Id == id);
            if(book == null)
            {
                return NotFound();
            }
            bookStoreContext.Books.Remove(book);
            bookStoreContext.SaveChanges();
            return Ok();
        }
    }
}
