using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Data;
using Demo.Entity;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Demo.Repository
{
    public partial class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(DatabaseContext context)
            : base(context, context.Categories)
        { }

        public Task<BaseFindResponse<Category>> FindAsync(CategoryFindRequest request)
        {
            return Task.Run(() =>
            {
                var query = context.CategoriesAsQueryable;
                if (!string.IsNullOrEmpty(request.Name))
                {
                    query = query.Where(a => a.Name.ToLowerInvariant().Contains(request.Name.ToLowerInvariant()));
                }

                var response = query.OrderBy(a => a.Name)
                    .Skip(request.Skip)
                    .Take(request.PageSize)
                    .ToList();

                return new BaseFindResponse<Category>
                {
                    Results = response,
                    TotalRecords = query.Count()
                };
            });
        }

        public Task<List<Reference>> GetReferencesAsync()
        {
            return collection.Find(a => a.IsActive)
                .SortBy(a => a.Name)
                .Project(a => new Reference(a.Id, a.Name))
                .ToListAsync();
        }

        public Task<List<Category>> GetAllCategoriesAsync()
        {
            return collection.Find(a => a.IsActive)
                .SortBy(a => a.Name)
                .ToListAsync();
        }

        public Task<List<Category>> GetCategoriesByIdsAsync(List<ObjectId> ids)
        {
            return collection.Find(a => a.IsActive && ids.Contains(a.Id))
                .SortBy(a => a.Name)
                .ToListAsync();
        }

        public Task<Category> GetCategoryBySlugAsync(string slug)
        {
            return collection.Find(a => a.IsActive && a.Slug.Equals(slug))
                .SortBy(a => a.Name)
                .FirstOrDefaultAsync();
        }

        protected override async Task<bool> ValidateDataDuplicate(Category entity)
        {
            return (await collection.CountDocumentsAsync(a => a.Id != entity.Id && a.Slug.Equals(entity.Slug))) == 0;
        }

        protected override async Task<bool> IsInUseAsync(ObjectId id)
        {
            return (await context.Posts.CountDocumentsAsync(a => a.CategoryId == id)) > 0;
        }

        protected override string Title => "Chuyên Mục";
    }
}
