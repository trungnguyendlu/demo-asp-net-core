using System.ComponentModel;

namespace Demo.Data
{
    public enum TitleTemplateType
    {
        [Description("Tên trang")]
        PageName = 1,
        [Description("Tên website")]
        SiteName = 2,
        [Description("Tên trang | Tên website")]
        PageNameBeforeSiteName = 3,
        [Description("Tên website | Tên trang")]
        SiteNameBeforePageName = 4
    }

    public enum PageSizeType
    {
        [Description("5")]
        Five = 5,
        [Description("10")]
        Ten = 10,
        [Description("15")]
        Fifteen = 15,
        [Description("20")]
        Twenty = 20
    }
    
    public enum MediaType
    {
        [Description("Tổng hợp")]
        Common = 1,
        [Description("Blog")]
        Blog = 6
    }

    public enum FunctionType
    {
        [Description("Xem phân quyền")]
        RoleView = 1,
        [Description("Thêm phân quyền mới")]
        RoleCreate = 2,
        [Description("Cập nhật phân quyền")]
        RoleEdit = 3,
        [Description("Xóa phân quyền")]
        RoleDelete = 4,

        [Description("Xem người dùng")]
        UserView = 10,
        [Description("Thêm người dùng mới")]
        UserCreate = 11,
        [Description("Cập nhật người dùng")]
        UserEdit = 12,
        [Description("Xóa người dùng")]
        UserDelete = 13,

        [Description("Xem bài viết")]
        PostView = 20,
        [Description("Thêm bài viết mới")]
        PostCreate = 21,
        [Description("Cập nhật bài viết")]
        PostEdit = 22,
        [Description("Xuất bản bài viết")]
        PostPublish = 23,
        [Description("Xóa bài viết")]
        PostDelete = 24,

        [Description("Xem thư viện")]
        MediaView = 30,
        [Description("Thêm thư viện mới")]
        MediaCreate = 31,
        [Description("Cập nhật thư viện")]
        MediaEdit = 32,
        [Description("Xóa thư viện")]
        MediaDelete = 33,

        [Description("Xem danh mục")]
        CategoryView = 40,
        [Description("Thêm danh mục mới")]
        CategoryCreate = 41,
        [Description("Cập nhật danh mục")]
        CategoryEdit = 42,
        [Description("Xóa danh mục")]
        CategoryDelete = 43,

        [Description("Xem đối tác")]
        PartnerView = 50,
        [Description("Thêm đối tác mới")]
        PartnerCreate = 51,
        [Description("Cập nhật đối tác")]
        PartnerEdit = 52,
        [Description("Xóa đối tác")]
        PartnerDelete = 53,

        [Description("Xem tin nhắn")]
        MessageView = 60,
        [Description("Xóa tin nhắn")]
        MessageDelete = 61,

        [Description("Thiết lập")]
        SettingsEdit = 70,
        [Description("Giao diện")]
        ThemeEdit = 71,
        [Description("Thay đổi giao diện")]
        ThemeChange = 72,

        [Description("Xem hỗ trợ")]
        TicketView = 80,
        [Description("Thêm hỗ trợ mới")]
        TicketCreate = 81,
        [Description("Xóa hỗ trợ")]
        TicketDelete = 82,

        [Description("Xem sản phẩm")]
        ProductView = 90,
        [Description("Thêm sản phẩm mới")]
        ProductCreate = 91,
        [Description("Cập nhật sản phẩm")]
        ProductEdit = 92,
        [Description("Xóa sản phẩm")]
        ProductDelete = 93,

        [Description("Xem dịch vụ")]
        ServiceView = 100,
        [Description("Thêm dịch vụ mới")]
        ServiceCreate = 101,
        [Description("Cập nhật dịch vụ")]
        ServiceEdit = 102,
        [Description("Xóa dịch vụ")]
        ServiceDelete = 103,

        [Description("Xem dự án")]
        ProjectView = 110,
        [Description("Thêm dự án mới")]
        ProjectCreate = 111,
        [Description("Cập nhật dự án")]
        ProjectEdit = 112,
        [Description("Xóa dự án")]
        ProjectDelete = 113,

        [Description("Xem Slider")]
        SliderView = 120,
        [Description("Thêm Slider mới")]
        SliderCreate = 121,
        [Description("Cập nhật Slider")]
        SliderEdit = 122,
        [Description("Xóa Slider")]
        SliderDelete = 123,

        [Description("Xem trang")]
        PageView = 130,
        [Description("Thêm trang mới")]
        PageCreate = 131,
        [Description("Cập nhật trang")]
        PageEdit = 132,
        [Description("Xóa trang")]
        PageDelete = 133,

        [Description("Xem Landing Page")]
        LandingPageView = 140,
        [Description("Thêm Landing Page mới")]
        LandingPageCreate = 141,
        [Description("Cập Nhật Landing Page")]
        LandingPageEdit = 142,
        [Description("Xóa Landing Page")]
        LandingPageDelete = 143,

        [Description("Xem widget")]
        WidgetView = 150,
        [Description("Thêm widget mới")]
        WidgetCreate = 151,
        [Description("Cập nhật widget")]
        WidgetEdit = 152,
        [Description("Xóa widget")]
        WidgetDelete = 153,

        [Description("Xem Subscribe")]
        SubscribeView = 160,
        [Description("Thêm Subscribe mới")]
        SubscribeCreate = 161,
        [Description("Cập nhật Subscribe")]
        SubscribeEdit = 162,
        [Description("Xóa Subscribe")]
        SubscribeDelete = 163,

        [Description("Xem Gallery")]
        GalleryView = 170,
        [Description("Thêm Gallery mới")]
        GalleryCreate = 171,
        [Description("Cập nhật Gallery")]
        GalleryEdit = 172,
        [Description("Xóa Gallery")]
        GalleryDelete = 173,

        [Description("Xem  loạidịch vụ")]
        ServiceCategoryView = 180,
        [Description("Thêm loại dịch vụ mới")]
        ServiceCategoryCreate = 181,
        [Description("Cập nhật loại dịch vụ")]
        ServiceCategoryEdit = 182,
        [Description("Xóa loại dịch vụ")]
        ServiceCategoryDelete = 183,

        [Description("Xem loại sản phẩm")]
        ProductCategoryView = 190,
        [Description("Thêm loại sản phẩm mới")]
        ProductCategoryCreate = 191,
        [Description("Cập nhật loại sản phẩm")]
        ProductCategoryEdit = 192,
        [Description("Xóa loại sản phẩm")]
        ProductCategoryDelete = 193,

        [Description("Thêm thông báo mới")]
        NotificationCreate = 990,
        [Description("Sửa thông báo")]
        NotificationEdit = 991,
        [Description("Xóa thông báo")]
        NotificationDelete = 992,
    }

    public enum WidgetPosition
    {
        [Description("Đầu trang bên trái")]
        TopLeft = 1,
        [Description("Đầu trang ở giữa")]
        TopMiddle = 2,
        [Description("Đầu trang bên phải")]
        TopRight = 3,
        [Description("Đầu trang")]
        Top = 4,
        [Description("Giữa trang")]
        Middle = 5,
        [Description("Cuối trang")]
        Bottom = 6,
        [Description("Cuối trang bên trái")]
        BottomLeft = 7,
        [Description("Cuối trang ở giữa")]
        BottomMiddle = 8,
        [Description("Cuối trang bên phải")]
        BottomRight = 9
    }

    public enum UserStatus
    {
        [Description("Kích hoạt")]
        Active = 1,
        [Description("Khóa")]
        Inactive = 2
    }
    
    public enum CategoryType
    {
        [Description("Sản Phẩm")]
        Product = 1,
        [Description("Bài Viết")]
        Blog = 2,
        [Description("Dự Án")]
        Project = 3
    }    
}
