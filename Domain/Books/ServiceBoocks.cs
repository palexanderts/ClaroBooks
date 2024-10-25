using Domain.DomainEvents;

namespace Domain.Books
{
    public sealed class ServiceBoocks
    {
        private readonly BookDtoValidator _bookDtoValidator = new BookDtoValidator();

        private readonly IRepositoryBoocks _repositoryBoocks;

        public ServiceBoocks(IRepositoryBoocks repositoryBoocks)
        {
            this._repositoryBoocks = repositoryBoocks;
        }

        public async Task<DomainEventMesage> UpdateBookAsync(BookAggregate bookAggregate)
        {
            try
            {
                var validationResult = _bookDtoValidator.Validate(bookAggregate.Book);
                if (!validationResult.IsValid)
                {
                    return new DomainEventMesage
                    {
                        Message = string.Join(Environment.NewLine, validationResult.Errors.Select(x => x.ErrorMessage)),
                        Type = DomainEventMesage.EventType.Error
                    };
                }

                await _repositoryBoocks.UpdateAsync(bookAggregate.Book);
                return new DomainEventMesage
                {
                    Message = DomainEventMesage.SuccessfulMessage,
                    Type = DomainEventMesage.EventType.Ok
                };
            }
            catch (Exception ex)
            {
                return new DomainEventMesage
                {
                    Message = ex.InnerException?.Message ?? ex.Message,
                    Type = DomainEventMesage.EventType.Error
                };
            }
        }

        public async Task<DomainEventMesage> AddBookAsync(BookAggregate bookAggregate)
        {
            try
            {
                var validationResult = _bookDtoValidator.Validate(bookAggregate.Book);
                if (!validationResult.IsValid)
                {
                    return new DomainEventMesage
                    {
                        Message = string.Join(Environment.NewLine, validationResult.Errors.Select(x => x.ErrorMessage)),
                        Type = DomainEventMesage.EventType.Error
                    };
                }

                await _repositoryBoocks.AddAsync(bookAggregate.Book);
                return new DomainEventMesage
                {
                    Message = DomainEventMesage.SuccessfulMessage,
                    Type = DomainEventMesage.EventType.Ok
                };
            }
            catch (Exception ex)
            {
                return new DomainEventMesage
                {
                    Message = ex.InnerException?.Message ?? ex.Message,
                    Type = DomainEventMesage.EventType.Error
                };
            }
        }

        public async Task<DomainEventMesage> DeleteBookByIdAsync(int id)
        {
            await _repositoryBoocks.DeleteAsync(id);

            var result = new DomainEventMesage<Book>
            {
                Type = DomainEventMesage.EventType.Ok,
                Message = DomainEventMesage.SuccessfulMessage,
            };

            return result;
        }

        public async Task<DomainEventMesage<Book>> GetBookByIdAsync(int id)
        {
            var books = await _repositoryBoocks.GetAsync(id);

            var result = new DomainEventMesage<Book>
            {
                Data = books,
                Type = DomainEventMesage.EventType.Ok,
                Message = DomainEventMesage.SuccessfulMessage,
            };

            return result;
        }

        public async Task<DomainEventMesage<IEnumerable<Book>>> GetBooksAsync()
        {
            var books = await _repositoryBoocks.GetAllAsync();

            var result = new DomainEventMesage<IEnumerable<Book>>
            {
                Data = books,
                Type = DomainEventMesage.EventType.Ok,
                Message = DomainEventMesage.SuccessfulMessage,
            };

            return result;
        }
    }
}
