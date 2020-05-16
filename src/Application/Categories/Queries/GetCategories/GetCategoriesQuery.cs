using AutoMapper;
using AutoMapper.QueryableExtensions;
using PersonalManager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PersonalManager.Application.Categories.Queries.GetCategories
{
    public class GetCategoriesQuery : IRequest<CategoriesVm>
    {
    }

    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, CategoriesVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCategoriesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CategoriesVm> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            List<CategoryDto> categories = new List<CategoryDto>();

            var parentCategories = await _context.Categories
                .Where(c => !c.ParentId.HasValue)
                .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                .OrderBy(c => c.Name)
                .ToListAsync(cancellationToken);

            foreach (var parent in parentCategories)
            {
                categories.Add(parent);

                var childCategories = await _context.Categories
                    .Where(c => c.ParentId == parent.Id)
                    .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                    .OrderBy(c => c.Name)
                    .ToListAsync(cancellationToken);
                categories.AddRange(childCategories);
            }

            return new CategoriesVm
            {
                Categories = categories
            };
        }
    }
}
