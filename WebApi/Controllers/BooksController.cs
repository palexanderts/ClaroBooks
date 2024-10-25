using Domain.Books;
using Domain.DomainEvents;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ServiceBoocks _serviceBoocks;

        public BooksController(ServiceBoocks serviceBoocks)
        {
            this._serviceBoocks = serviceBoocks;
        }


        [ApiExplorerSettings(IgnoreApi = true)]
        private IActionResult GetBadRequest(Exception exception)
        {
            return BadRequest(new DomainEventMesage
            {
                Message = exception?.InnerException?.Message,
                Type = DomainEventMesage.EventType.Error
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetBooksAsync()
        {
            try
            {
                var books = await _serviceBoocks.GetBooksAsync();
                if (books.Type == DomainEventMesage.EventType.Ok)
                    return Ok(books);
                else
                    return Conflict(books);
            }
            catch (Exception ex)
            {
                return GetBadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookAsync(int id)
        {
            try
            {
                var book = await _serviceBoocks.GetBookByIdAsync(id);
                if (book.Type == DomainEventMesage.EventType.Ok)
                    return Ok(book);
                else
                    return Conflict(book);
            }
            catch (Exception ex)
            {
                return GetBadRequest(ex);
            }
       
        }

        [HttpPost]
        public async Task<IActionResult> PostBookAsync([FromBody] Book book)
        {
            try
            {
                var createdBook = await _serviceBoocks.AddBookAsync(new Domain.BookAggregate(book));

                if (createdBook.Type != DomainEventMesage.EventType.Ok)
                    return Conflict(createdBook);
                else
                    return Created(string.Empty, createdBook);
            }
            catch (Exception ex)
            {
                return GetBadRequest(ex);
            }
        }


        [HttpPut]
        public async Task<IActionResult> PutBookAsync([FromBody] Book book)
        {
            try
            {
                var updated = await _serviceBoocks.UpdateBookAsync(new Domain.BookAggregate(book));
                if (updated.Type != DomainEventMesage.EventType.Ok)
                    return Conflict(updated);
                else
                    return Ok(updated);
            }
            catch (Exception ex)
            {
                return GetBadRequest(ex);
            }
          
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookAsync(int id)
        {
            try
            {
                var deleted = await _serviceBoocks.DeleteBookByIdAsync(id);
                if (deleted.Type != DomainEventMesage.EventType.Ok)
                    return Conflict(deleted);
                else
                    return Ok(deleted);
            }
            catch (Exception ex)
            {
                return GetBadRequest(ex);
            }

        }
    }
}
