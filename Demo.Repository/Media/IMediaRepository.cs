using System.Threading.Tasks;
using Demo.Data;
using Demo.Entity;

namespace Demo.Repository
{
    public interface IMediaRepository : IBaseRepository<Media>
    {
        Task<BaseFindResponse<Media>> FindAsync(MediaFindRequest request);
    }
}