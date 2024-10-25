using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Books
{
    public interface IRepositoryBoocks
    {
        Task AddAsync(Book book);
        Task UpdateAsync(Book book);
        Task DeleteAsync(int id);
        Task<IEnumerable<Book>> GetAllAsync();
         Task<Book> GetAsync(int id);
    }
}
