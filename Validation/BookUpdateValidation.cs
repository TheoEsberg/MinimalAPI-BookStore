using FluentValidation;
using MinimalAPI_BookStore.Models.DTOs;

namespace MinimalAPI_BookStore.Validation
{
	public class BookUpdateValidation : AbstractValidator<BookUpdateDTO>
	{
        public BookUpdateValidation()
        {
			RuleFor(model => model.Title).NotEmpty();
			RuleFor(model => model.Description).NotEmpty();
			RuleFor(model => model.Author).NotEmpty();
			RuleFor(model => model.Genre).NotEmpty();
		}
    }
}
