using System.Linq;
using System.Threading.Tasks;
using Demo.Data;
using Demo.Entity;
using MongoDB.Driver;

namespace Demo.Repository
{
    public class MediaRepository : BaseRepository<Media>, IMediaRepository
    {
        public MediaRepository(DatabaseContext context)
            : base(context, context.Media)
        { }

        public Task<BaseFindResponse<Media>> FindAsync(MediaFindRequest request)
        {
            return Task.Run(() =>
            {
                var query = context.MediaAsQueryable;
                if (!string.IsNullOrEmpty(request.Name))
                {
                    query = query.Where(a => a.Title.ToLowerInvariant().Contains(request.Name.ToLowerInvariant()));
                }
                if (request.Type.HasValue)
                {
                    query = query.Where(a => a.Type == request.Type);
                }

                var response = query.OrderByDescending(a => a.CreatedDate)
                    .Skip(request.Skip)
                    .Take(request.PageSize)
                    .ToList();

                return new BaseFindResponse<Media>
                {
                    Results = response,
                    TotalRecords = query.Count()
                };
            });
        }
        
        protected override string Title => "Thư viện";
    }
}
