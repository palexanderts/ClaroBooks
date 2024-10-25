using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Books
{
    using FluentValidation;
    using System;

    public class BookDtoValidator : AbstractValidator<Book>
    {
        public BookDtoValidator()
        {
            RuleFor(book => book.Title)
                .NotEmpty()
                .WithMessage("El Título es obligatorio.");

            RuleFor(book => book.Description)
                .NotEmpty()
                .WithMessage("La Descripción es obligatoria.");

            RuleFor(book => book.PageCount)
                .GreaterThan(0)
                .WithMessage("El Número de Páginas debe ser mayor a 0.");

            RuleFor(book => book.Excerpt)
                .NotEmpty()
                .WithMessage("El Extracto es obligatorio.");

            RuleFor(book => book.PublishDate)
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("La Fecha de Publicación no puede ser en el futuro.");
        }
    }

}
