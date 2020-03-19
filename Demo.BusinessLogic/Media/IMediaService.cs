using System.Threading.Tasks;
using Demo.Data;
using Demo.Entity;

namespace Demo.BusinessLogic
{
    public interface IMediaService : IBaseService<Media>
    {
        Task<BaseFindResponse<Media>> FindAsync(MediaFindRequest request);
    }
}