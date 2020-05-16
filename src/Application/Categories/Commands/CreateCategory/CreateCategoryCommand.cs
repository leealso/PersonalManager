using PersonalManager.Application.Common.Interfaces;
using PersonalManager.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace PersonalManager.Application.Categories.Commands.CreateCategory
{
    public partial class CreateCategoryCommand : IRequest<int>
    {
        public string Name { get; set; }

        public int? ParentId { get; set; }

        public string IconUrl { get; set; }
    }

    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateCategoryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = new Category
            {
                Name = request.Name,
                ParentId = request.ParentId,
                IconUrl = request.IconUrl
            };

            _context.Categories.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
