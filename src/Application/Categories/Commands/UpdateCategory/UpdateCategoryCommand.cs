using PersonalManager.Application.Common.Exceptions;
using PersonalManager.Application.Common.Interfaces;
using PersonalManager.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace PersonalManager.Application.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? ParentId { get; set; }

        public string IconUrl { get; set; }
    }

    public class UpdateCategoryDetailCommandHandler : IRequestHandler<UpdateCategoryCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateCategoryDetailCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Categories.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Category), request.Id);
            }

            entity.Name = request.Name;
            entity.ParentId = request.ParentId;
            entity.IconUrl = request.IconUrl;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
