using Demo.Data;
using Demo.Entity;
using Demo.Util;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Repository
{
    public partial class BlogRepository : BaseRepository<Post>, IBlogRepository
    {
        public BlogRepository(DatabaseContext context)
            : base(context, context.Posts)
        { }

        public Task<BaseFindResponse<Post>> FindAsync(PostFindRequest request)
        {
            return Task.Run(() =>
            {
                var query = context.PostsAsQueryable;
                if (request.CategoryId.HasValue())
                {
                    query = query.Where(a => a.CategoryId == request.CategoryId);
                }
                if (!string.IsNullOrEmpty(request.Title))
                {
                    query = query.Where(a => a.Title.ToLowerInvariant().Contains(request.Title.ToLowerInvariant()));
                }

                var response = query.OrderByDescending(a => a.CreatedDate)
                    .Skip(request.Skip)
                    .Take(request.PageSize)
                    .ToList();

                return new BaseFindResponse<Post>
                {
                    Results = response,
                    TotalRecords = query.Count()
                };
            });
        }

        public Task<BaseFindResponse<Post>> SearchPostsAsync(PostSearchRequest request)
        {
            return Task.Run(() =>
            {
                var query = context.PostsAsQueryable.Where(a => a.IsPublished);
                if (request.CategoryId.HasValue())
                {
                    query = query.Where(a => a.CategoryId == request.CategoryId);
                }
                if (!string.IsNullOrEmpty(request.Keyword))
                {
                    query = query.Where(a => a.Title.ToLowerInvariant().Contains(request.Keyword.ToLowerInvariant()) ||
                        a.Description.ToLowerInvariant().Contains(request.Keyword.ToLowerInvariant()));
                }

                var posts = query.OrderByDescending(a => a.PublishedDate)
                        .Skip(request.Skip)
                        .Take(request.PageSize)
                        .ToList();

                return new BaseFindResponse<Post>
                {
                    Results = posts,
                    TotalRecords = query.Count()
                };
            });
        }

        public Task<List<Post>> GetAllPostsForSitemapAsync()
        {
            return collection.Find(a => a.IsPublished)
                .Project(a => new Post
                {
                    CategoryId = a.CategoryId,
                    Slug = a.Slug,
                    PublishedDate = a.PublishedDate,
                    UpdatedDate = a.UpdatedDate
                }).ToListAsync();
        }

        public Task<List<Reference>> GetAsReferencesAsync()
        {
            return collection.Find(a => a.IsPublished)
                .SortByDescending(a => a.CreatedDate)
                .Project(a => new Reference(a.Id, a.Title))
                .ToListAsync();
        }

        public Task<List<Post>> GetRelatedPostsAsync(RelatedPostFindRequest request)
        {
            return collection.Find(a => a.IsPublished &&
                    a.CategoryId == request.CategoryId &&
                    a.Id != request.PostId)
                .SortByDescending(a => a.CreatedDate)
                .Limit(request.Top)
                .ToListAsync();
        }


        public Task<List<Post>> GetPopularPostsAsync(int top = 5)
        {
            return collection.Find(a => a.IsPublished && a.IsFeaturePost)
                .SortByDescending(a => a.CreatedDate)
                .Limit(top)
                .Project(a => new Post
                {
                    Id = a.Id,
                    Title = a.Title,
                    Slug = a.Slug,
                    ThumbnailUrl = a.ThumbnailUrl,
                    PhotoUrl = a.PhotoUrl,
                    CategoryId = a.CategoryId,
                    Description = a.Description,
                    CreatedDate = a.CreatedDate,
                    PublishedDate = a.PublishedDate
                })
                .ToListAsync();
        }

        public Task<List<Post>> GetFeaturePostsAsync(int top = 3)
        {
            return collection.Find(a => a.IsPublished && a.IsFeaturePost)
                .SortByDescending(a => a.CreatedDate)
                .Limit(top)
                .Project(a => new Post
                {
                    Id = a.Id,
                    Title = a.Title,
                    Slug = a.Slug,
                    ThumbnailUrl = a.ThumbnailUrl,
                    PhotoUrl = a.PhotoUrl,
                    CategoryId = a.CategoryId,
                    Description = a.Description,
                    CreatedDate = a.CreatedDate,
                    PublishedDate = a.PublishedDate
                })
                .ToListAsync();
        }

        public async Task<Post> GetPostBySlugAsync(string slug)
        {
            var post = await collection.Find(a => a.Slug.Equals(slug) && a.IsPublished)
                .FirstOrDefaultAsync();

            return post;
        }

        protected override async Task<bool> ValidateDataDuplicate(Post entity)
        {
            return (await collection.CountDocumentsAsync(a => a.Id != entity.Id && a.Slug.Equals(entity.Slug))) == 0;
        }

        protected override string Title => "Bài Viết";
    }
}
