using Microsoft.AspNetCore.Http;

namespace Demo.Model.Admin.Media
{
    public class MediaUploadModel
    {
        public IFormFile File { get; set; }
        public MediaModel Entity { get; set; }
    }
}