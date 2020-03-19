using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Data;
using Demo.Entity;
using Demo.Util;
using MongoDB.Driver;

namespace Demo.Repository
{
    public partial class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DatabaseContext context)
            : base(context, context.Users)
        {
        }

        public Task<BaseFindResponse<User>> FindAsync(UserFindRequest request)
        {
            return Task.Run(() =>
            {
                var query = context.UsersAsQueryable.Where(a => !a.IsAdmin);
                if (request.RoleId.HasValue())
                {
                    query = query.Where(a => a.RoleId == request.RoleId);
                }
                if (!string.IsNullOrEmpty(request.Email))
                {
                    query = query.Where(a => a.Email.ToLowerInvariant().Contains(request.Email.ToLowerInvariant()));
                }

                var response = query.OrderByDescending(a => a.CreatedDate)
                    .Skip(request.Skip)
                    .Take(request.PageSize)
                    .ToList();

                return new BaseFindResponse<User>
                {
                    Results = response.ToList(),
                    TotalRecords = query.Count()
                };
            });
        }

        public Task<List<Reference>> GetReferencesAsync()
        {
            return collection.Find(a => !a.IsAdmin)
                .Project(a => new Reference(a.Id, a.FullName))
                .ToListAsync();
        }

        public Task<User> GetByEmailAsync(string email)
        {
            return collection.Find(a => a.Email.Equals(email)).FirstOrDefaultAsync();
        }

        public Task<User> LoginAsync(string email, string password)
        {
            return collection.Find(a => a.Email.Equals(email) && a.Password.Equals(password))
                .FirstOrDefaultAsync();
        }

        public Task UpdateLastLoginDateAsync(string email)
        {
            return collection.FindOneAndUpdateAsync(a => a.Email.Equals(email),
                Builders<User>.Update.Set(a => a.LastLoginDate, DateTime.UtcNow));
        }

        protected override string Title => "Người Dùng";
    }
}
