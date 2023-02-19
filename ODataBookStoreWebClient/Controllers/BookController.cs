using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using ODataBookStore.Models;
using static System.Net.WebRequestMethods;

namespace ODataBookStoreWebClient.Controllers
{
    public class BookController : Controller
    {

        private readonly HttpClient client;
        private string BookApiUrl;

        public BookController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            BookApiUrl = $"https://localhost:7035/odata/Books";
        }

        // GET: Book
        public async Task<IActionResult> Index()
        {
            
            return View(await GetBooks());
        }

        // GET: Book/Details/5
        public async Task<IActionResult> Details(int id)
        {
            List<Book> listBooks = await GetBooks();
            Book book = listBooks.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            ViewData["Category"] = GetCategories();
            return View(new Book());
        }

        private List<string> GetCategories()
        {
            return new List<string> { Category.BOOK.ToString(),Category.EBOOK.ToString(),Category.MAGAZINE.ToString() };
        }

        // POST: Book/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateBook([FromForm]Book book)
        {
            try
            {
                if (book == null)
                {
                    return BadRequest();
                }
                string url = "https://localhost:7035/api/Books/Post";
                await client.PostAsJsonAsync(url, book);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Book/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            List<Book> listBooks = await GetBooks();
            Book book = listBooks.FirstOrDefault(b => b.Id == id);
            if(book == null || id == null)
            {
                return NotFound();
            }
            ViewData["Category"] = GetCategories();
            return View(book);
        }

        [HttpPost]
        // POST: Book/Edit/5
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditBook([FromForm] Book book)
        {
            try
            {
                if (book == null)
                {
                    return BadRequest();
                }
                string url = "https://localhost:7035/api/Books/Put";
                await client.PutAsJsonAsync(url, book);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Book/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            List<Book> products = await GetBooks();
            Book product = products.FirstOrDefault(b => b.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            String urlForDelete = "https://localhost:7035/api/Books";
            String url = urlForDelete + "/Delete/id?id=" + id;
            await client.DeleteAsync(url);
            return Redirect("/Book/Index");
        }

        private async Task<List<Book>> GetBooks()
        {
            using(HttpResponseMessage response = await client.GetAsync(BookApiUrl))
            {
                string strData = await response.Content.ReadAsStringAsync();
                dynamic temp = JObject.Parse(strData);
                var lst = temp.value;
                List<Book> items = ((JArray)temp.value).Select(x => new Book
                {
                    Id = (int)x["Id"],
                    Author = (string)x["Author"],
                    ISBN = (string)x["ISBN"],
                    Title = (string)x["Title"],
                    Price = (decimal)x["Price"]
                }).ToList();
                return items;
            }
            
        }
    }
}
