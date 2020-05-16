using PersonalManager.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace PersonalManager.Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateCategoryCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(50).WithMessage("Title must not exceed 50 characters.")
                .MustAsync(BeUniqueName).WithMessage("The specified name already exists.");
            RuleFor(v => v.IconUrl)
                .NotEmpty().WithMessage("Icon url is required.")
                .MaximumLength(150).WithMessage("Icon url must not exceed 150 characters.");
            RuleFor(v => v.ParentId)
                .MustAsync(BeValidId).WithMessage("The specified parent category id does not exists.");
        }

        public async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
        {
            return await _context.Categories
                .AllAsync(c => c.Name != name);
        }

        public async Task<bool> BeValidId(int? categoryId, CancellationToken cancellationToken)
        {
            return !categoryId.HasValue || await _context.Categories
                .AnyAsync(c => c.Id == categoryId);
        }
    }
}
