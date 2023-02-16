using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using ODataBookStore.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ODataBookStore.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PressesController : ControllerBase
    {

        private BookStoreContext bookStoreContext;

        public PressesController(BookStoreContext bookStoreContext)
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

        [EnableQuery]
        [HttpGet]
        // GET: api/<PressesController>
        public IActionResult Get()
        {
            return Ok(bookStoreContext.Presses);
        }
    }
}
