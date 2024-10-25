using Domain.Books;
using Domain.DomainEvents;

namespace WebApp.Blazor
{
    public class BookServiceOnline
    {
        private readonly HttpClient _httpClient;

        public BookServiceOnline(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DomainEventMesage<IEnumerable<Book>>> GetBooksAsync()
        {
            var result = _httpClient.GetAsync("Books").Result;
            return await result.Content.ReadFromJsonAsync<DomainEventMesage<IEnumerable<Book>>>();
        }

        public async Task<DomainEventMesage<Book>> GetBookByIdAsync(int id)
        {
            var result = _httpClient.GetAsync($"Books/{id}").Result;
            return await result.Content.ReadFromJsonAsync<DomainEventMesage<Book>>();
        }

        public async Task<DomainEventMesage> CreateBookAsync(Book book)
        {
            var responce = _httpClient.PostAsJsonAsync("Books", book).Result;
            return responce.Content.ReadFromJsonAsync<DomainEventMesage>().Result;
        }

        public async Task<DomainEventMesage> UpdateBookAsync(Book book)
        {
            var responce = _httpClient.PutAsJsonAsync(string.Empty, book).Result;
            return responce.Content.ReadFromJsonAsync<DomainEventMesage>().Result;
        }

        public async Task<DomainEventMesage> DeleteBookAsync(int id)
        {
            var responce = _httpClient.DeleteAsync($"Books/{id}").Result;
            return responce.Content.ReadFromJsonAsync<DomainEventMesage>().Result;
        }
    }
}

