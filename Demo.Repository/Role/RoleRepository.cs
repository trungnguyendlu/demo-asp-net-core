using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Data;
using Demo.Entity;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Demo.Repository
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(DatabaseContext context)
            : base(context, context.Roles)
        { }

        public Task<BaseFindResponse<Role>> FindAsync(RoleFindRequest request)
        {
            return Task.Run(() =>
            {
                var query = context.RolesAsQueryable;
                if (!string.IsNullOrEmpty(request.Name))
                {
                    query = query.Where(a => a.Name.ToLowerInvariant().Contains(request.Name.ToLowerInvariant()));
                }

                var response = query.OrderBy(a => a.Name)
                    .Skip(request.Skip)
                    .Take(request.PageSize)
                    .ToList();

                return new BaseFindResponse<Role>
                {
                    Results = response,
                    TotalRecords = query.Count()
                };
            });
        }

        public Task<List<Reference>> GetReferencesAsync()
        {
            return collection.Find(a => true)
                .Project(a => new Reference(a.Id, a.Name))
                .ToListAsync();
        }

        protected override async Task<bool> IsInUseAsync(ObjectId id)
        {
            return (await context.Users.CountDocumentsAsync(a => a.RoleId == id)) > 0;
        }

        protected override string Title => "Phân quyền";
    }
}
