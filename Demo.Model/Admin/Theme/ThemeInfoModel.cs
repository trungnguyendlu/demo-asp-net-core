using System;

namespace Corporation.Models.Admin.Theme
{
    [Serializable]
    public class ThemeInfoModel : BaseModel
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public string PhotoUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Description { get; set; }
        public bool IsCurrentTheme { get; set; }

        public string GetPhotoUrl()
        {
            return !string.IsNullOrEmpty(PhotoUrl) ? $"/themes/{Id}{PhotoUrl}" : "/images/placeholder.png";
        }

        public string GetThumbnailUrl()
        {
            return !string.IsNullOrEmpty(ThumbnailUrl) ? $"/themes/{Id}{ThumbnailUrl}" : "/images/placeholder.png";
        }
    }
}
