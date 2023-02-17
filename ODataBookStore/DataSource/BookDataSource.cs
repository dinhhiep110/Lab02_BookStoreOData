using ODataBookStore.Models;

namespace ODataBookStore.DataSource
{
    public static class BookDataSource
    {
        private static IList<Book> books { get; set; }

        public static IList<Book> GetBooks()
        {
            if (books != null)
            {
                return books;
            }
            books = new List<Book>();
            Book book = new Book
            {
                Id = 1,
                ISBN = "978-1-56619-909-4",
                Title = "Head First Python",
                Price = 59.99m,
                Author = "AKJS",
                Location = new Address
                {
                    City = "Ha Noi",
                    Street = "Dong Da"
                },
                Press = new Press
                {
                    Id = 1,
                    Name = "Paul Barry",
                    Category = Category.BOOK
                }
            };
            books.Add(book);
            book = new Book
            {
                Id = 2,
                ISBN = "978-1-56619-909-5",
                Title = "Head First Java",
                Price = 69.99m,
                Author = "KLJSK",
                Location = new Address
                {
                    City = "Ha Noi",
                    Street = "Ba Dinh"
                },
                Press = new Press
                {
                    Id = 2,
                    Name = "Kathy Sierra",
                    Category = Category.BOOK
                }
            };
            books.Add(book);
            book = new Book
            {
                Id = 3,
                ISBN = "978-1-56619-909-6",
                Title = "Head First C",
                Price = 49.99m,
                Author = "IHFISII",
                Location = new Address
                {
                    City = "Ha Noi",
                    Street = "Thanh Xuan"
                },
                Press = new Press
                {
                    Id = 3,
                    Name = "Abcd",
                    Category = Category.EBOOK
                }
            };
            books.Add(book);
            book = new Book
            {
                Id = 4,
                ISBN = "978-1-56619-909-7",
                Title = "Business",
                Price = 59.99m,
                Author = "PLSSJK",
                Location = new Address
                {
                    City = "HCM",
                    Street = "Quan 1"
                },
                Press = new Press
                {
                    Id = 4,
                    Name = "Bcde",
                    Category = Category.MAGAZINE
                }
            };
            return books;
        }
    }
}
