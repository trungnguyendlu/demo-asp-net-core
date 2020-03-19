using AutoMapper;
using Demo.Data;
using Demo.Entity;
using Demo.Infrastructure.Utils;
using Demo.Model;
using Demo.Model.Admin.Role;
using Demo.Model.Admin.User;
using Demo.Model.Category;
using Demo.Model.Post;
using Demo.Util;

namespace Demo.Infrastructure
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
            : this("MyProfile")
        {
        }

        protected AutoMapperConfiguration(string profileName)
        : base(profileName)
        {
            CreateMap<BaseEntity, BaseModel>()
                .ForMember(a => a.CreatedDate, b => b.MapFrom(c => c.CreatedDate.ConvertToLocal(Common.IsDevelopment)))
                .ForMember(a => a.UpdatedDate, b => b.MapFrom(c => c.UpdatedDate.ConvertToLocalNull(Common.IsDevelopment)));

            CreateMap<BaseModel, BaseEntity>()
                .ForMember(a => a.CreatedDate, b => b.MapFrom(c => c.CreatedDate.ConvertToUtc(Common.IsDevelopment)))
                .ForMember(a => a.UpdatedDate, b => b.MapFrom(c => c.UpdatedDate.ConvertToUtcNull(Common.IsDevelopment)));

            CreateMap<Post, PostModel>()
                .ForMember(a => a.PublishedDate, b => b.MapFrom(c => c.PublishedDate.ConvertToLocalNull(Common.IsDevelopment)));
            CreateMap<Model.Admin.Post.PostModel, Post>()
                .ForMember(a => a.PublishedDate, b => b.MapFrom(c => c.PublishedDate.ConvertToUtcNull(Common.IsDevelopment)));
            CreateMap<Post, Model.Admin.Post.PostModel>()
                .ForMember(a => a.PublishedDate, b => b.MapFrom(c => c.PublishedDate.ConvertToLocalNull(Common.IsDevelopment)));
            CreateMap<Model.Admin.Post.PostIndexModel, PostFindRequest>()
                .ForMember(a => a.PageNumber, b => b.MapFrom(c => c.Paging.PageNumber))
                .ForMember(a => a.PageSize, b => b.MapFrom(c => c.Paging.PageSize));
            CreateMap<Model.Admin.Post.ImageModel, Image>();
            CreateMap<Image, Model.Admin.Post.ImageModel>();

            CreateMap<Category, CategoryModel>();
            CreateMap<Model.Admin.Category.CategoryModel, Category>();
            CreateMap<Category, Model.Admin.Category.CategoryModel>();
            CreateMap<Model.Admin.Category.CategoryIndexModel, CategoryFindRequest>()
                .ForMember(a => a.PageNumber, b => b.MapFrom(c => c.Paging.PageNumber))
                .ForMember(a => a.PageSize, b => b.MapFrom(c => c.Paging.PageSize));

            CreateMap<Model.Admin.Widget.WidgetModel, Widget>()
                .ForMember(a => a.Position, b => b.MapFrom(c => (int)c.Position));
            CreateMap<Widget, Model.Admin.Widget.WidgetModel>()
                .ForMember(a => a.Position, b => b.MapFrom(c => (WidgetPosition)c.Position));
            CreateMap<Model.Admin.Widget.WidgetIndexModel, WidgetFindRequest>()
                .ForMember(a => a.PageNumber, b => b.MapFrom(c => c.Paging.PageNumber))
                .ForMember(a => a.PageSize, b => b.MapFrom(c => c.Paging.PageSize));

            CreateMap<Model.Admin.Media.MediaModel, Media>();
            CreateMap<Media, Model.Admin.Media.MediaModel>();

            CreateMap<Model.Admin.Settings.SettingsModel, Settings>()
                .ForMember(a => a.OpeningDate, b => b.MapFrom(c => c.OpeningDate.ConvertToUtcNull(Common.IsDevelopment)))
                .ForMember(a => a.TitleTemplateType, b => b.MapFrom(c => (int)c.TitleTemplateType))
                .ForMember(a => a.PageSize, b => b.MapFrom(c => (int)c.PageSize));
            CreateMap<Settings, Model.Admin.Settings.SettingsModel>()
                .ForMember(a => a.OpeningDate, b => b.MapFrom(c => c.OpeningDate.ConvertToLocalNull(Common.IsDevelopment)))
                .ForMember(a => a.TitleTemplateType, b => b.MapFrom(c => (TitleTemplateType)c.TitleTemplateType))
                .ForMember(a => a.PageSize, b => b.MapFrom(c => (PageSizeType)c.PageSize));

            CreateMap<Role, RoleModel>();
            CreateMap<RoleModel, Role>();
            CreateMap<PermissionModel, Permission>()
                .ForMember(a => a.Function, b => b.MapFrom(c => (int)c.Function));
            CreateMap<Permission, PermissionModel>()
                .ForMember(a => a.Function, b => b.MapFrom(c => (FunctionType)c.Function));
            CreateMap<User, AuthorModel>();
            CreateMap<User, UserModel>()
                .ForMember(a => a.LastLoginDate, b => b.MapFrom(c => c.LastLoginDate.ConvertToLocalNull(Common.IsDevelopment)))
                .ForMember(a => a.Status, b => b.MapFrom(c => (FunctionType)c.Status));
            CreateMap<UserModel, User>()
                .ForMember(a => a.LastLoginDate, b => b.MapFrom(c => c.LastLoginDate.ConvertToUtcNull(Common.IsDevelopment)))
                .ForMember(a => a.Status, b => b.MapFrom(c => (int)c.Status));
        }
    }
}
