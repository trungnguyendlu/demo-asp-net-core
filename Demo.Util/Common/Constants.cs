using MongoDB.Bson;
using System.Collections.Generic;

namespace Demo.Util
{
    public static class Constants
    {
        public const string Version = "1.1";
        public const int MaxGalleryItems = 8;
        public const string PlaceholderImageUrl = "/images/placeholder.png";
        public static ObjectId DefaultSettingsId = new ObjectId("5b7a58e19f804a03e8bf2d39");
        public static ObjectId DefaultThemeId = new ObjectId("5b7c045edd4b8603e8fd63e9");
        public static ObjectId CorporationId = new ObjectId("5b7c045edd4b8603e8fd63ef");
        public static ObjectId SystemUserId = new ObjectId("5b7c045edd4b8603e8fd6aff");
        public static List<string> AllowImageExtensions = new List<string> { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };

        public const string DefaultTimeZone = "SE Asia Standard Time";
        public const string DefaultTimeZoneDeploy = "Asia/Ho_Chi_Minh";







        public const int DefaultMaxDisplayPages = 6;

        public const string DefaultPageQueryParameter = "PageIndex";

        public const int DefaultPageCount = 5;

        public const int DefaultPageSize = 20;

        public const int StartPage = 1;

        public const int MaxPageSize = 1000;//elastic search max page size = 10.000

        public const string DefaultPageQueryName = "page";

        public const string DefaultDateFormat = "dd/MM/yyyy";

        public const string ShortDateFormat = "d/M/yy";

        public const string DefaultMonthFormat = "MM/yyyy";

        public const string DefaultDateTimeFormat = "dd/MM/yyyy HH:mm:ss tt";

        public const string DefaultShortDateTimeFormat = "dd/MM/yyyy HH:mm";

        public const string DefaultTimeFormat = "hh:mmtt";

        public const int DefaultDecimalPlaces = 2;

        public const string CategoryBreakCrumbSeperate = " >> ";

        public const string DefaultCountryCode = "VN";

        public const string DefaultMoneyFormat = "c0";

        public const int DefaultAutoCompleteItem = 10;

        public const string NoData = "Không có dữ liệu";

        public const string Message = "message";

        public const string SMessage = "sMessage";

        public const string EMessage = "eMessage";

        public const string CurrentPage = "CurrentPage";


    }
}
