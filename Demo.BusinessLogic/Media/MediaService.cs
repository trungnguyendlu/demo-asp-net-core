using System.Threading.Tasks;
using Demo.Data;
using Demo.Entity;
using Demo.Repository;

namespace Demo.BusinessLogic
{
    public class MediaService : BaseService<Media>, IMediaService
    {
        private readonly IMediaRepository _mediaRepository;

        public MediaService(IMediaRepository mediaRepository)
            : base(mediaRepository)
        {
            _mediaRepository = mediaRepository;
        }

        public Task<BaseFindResponse<Media>> FindAsync(MediaFindRequest request)
        {
            return _mediaRepository.FindAsync(request);
        }
    }
}
