using Domain.DomainEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Books
{
    public sealed class AggregateBooks : IAggregate
    {
        public AggregateBooks(Book book)
        {
            Book = book;
        }
        public Book Book { get; }
    }
}
