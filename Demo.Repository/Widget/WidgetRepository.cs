using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.Entity;
using MongoDB.Driver;
using Demo.Data;
using System.Linq;

namespace Demo.Repository
{
    public partial class WidgetRepository : BaseRepository<Widget>, IWidgetRepository
    {
        public WidgetRepository(DatabaseContext context)
            : base(context, context.Widgets)
        { }

        public Task<BaseFindResponse<Widget>> FindAsync(WidgetFindRequest request)
        {
            return Task.Run(() =>
            {
                var query = context.WidgetsAsQueryable;
                if (!string.IsNullOrEmpty(request.Title))
                {
                    query = query.Where(a => a.Title.ToLowerInvariant().Contains(request.Title.ToLowerInvariant()));
                }

                var response = query.OrderBy(a => a.Title)
                    .Skip(request.Skip)
                    .Take(request.PageSize)
                    .ToList();

                return new BaseFindResponse<Widget>
                {
                    Results = response,
                    TotalRecords = query.Count()
                };
            });
        }

        public Task<List<Widget>> GetWidgetsAsync()
        {
            return collection.Find(a => a.IsActive)
                .ToListAsync();
        }

        public Task<Widget> GetByPositionAsync(WidgetPosition position)
        {
            return collection.Find(a => a.Position == (int)position)
                .FirstOrDefaultAsync();
        }

        protected override string Title => "Widget";
    }
}
