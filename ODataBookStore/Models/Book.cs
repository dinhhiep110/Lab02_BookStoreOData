using MessagePack;
using System.ComponentModel;

namespace ODataBookStore.Models
{
    public class Book
    {

        public int Id { get; set; }

        //International Standard Book Number
        public string? ISBN { get; set; }

        public string? Title { get; set; }

        public string? Author { get; set; }

        public decimal Price { get; set; }

        public Address? Location { get; set; }

        public Press? Press { get; set; }

    }
}
