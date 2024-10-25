using Domain.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public sealed class BookAggregate : IAggregate
    {
        public BookAggregate(Book book)
        {
            Book = book;
        }

        public Book Book { get; }
    }
}
