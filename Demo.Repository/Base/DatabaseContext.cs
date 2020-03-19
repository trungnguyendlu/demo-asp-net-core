using MongoDB.Driver;
using Demo.Entity;
using System.Linq;
using Microsoft.Extensions.Options;
using Demo.Data;

namespace Demo.Repository
{
    public class DatabaseContext
    {
        private readonly IMongoDatabase _database;
        private IMongoCollection<Category> _categories;
        private IMongoCollection<Media> _media;
        private IMongoCollection<Post> _posts;
        private IMongoCollection<Role> _roles;
        private IMongoCollection<Settings> _settings;
        private IMongoCollection<User> _users;
        private IMongoCollection<Widget> _widgets;

        public DatabaseContext(IOptions<DbSetting> settings)
        {
            var client = new MongoClient($"mongodb://{settings.Value.User}:{settings.Value.Pass}@localhost");
            if (client != null)
            {
                _database = client.GetDatabase(settings.Value.Name);
            }
        }

        public IMongoCollection<Category> Categories => _categories ?? (_categories = _database.GetCollection<Category>(typeof(Category).Name));
        public IMongoCollection<Media> Media => _media ?? (_media = _database.GetCollection<Media>(typeof(Media).Name));
        public IMongoCollection<Post> Posts => _posts ?? (_posts = _database.GetCollection<Post>(typeof(Post).Name));
        public IMongoCollection<Role> Roles => _roles ?? (_roles = _database.GetCollection<Role>(typeof(Role).Name));
        public IMongoCollection<Settings> Settings => _settings ?? (_settings = _database.GetCollection<Settings>(typeof(Settings).Name));
        public IMongoCollection<User> Users => _users ?? (_users = _database.GetCollection<User>(typeof(User).Name));
        public IMongoCollection<Widget> Widgets => _widgets ?? (_widgets = _database.GetCollection<Widget>(typeof(Widget).Name));
        
        public IQueryable<Category> CategoriesAsQueryable => Categories.AsQueryable();
        public IQueryable<Media> MediaAsQueryable => Media.AsQueryable();
        public IQueryable<Post> PostsAsQueryable => Posts.AsQueryable();
        public IQueryable<Role> RolesAsQueryable => Roles.AsQueryable();
        public IQueryable<Settings> SettingsAsQueryable => Settings.AsQueryable();
        public IQueryable<User> UsersAsQueryable => Users.AsQueryable();
        public IQueryable<Widget> WidgetsAsQueryable => Widgets.AsQueryable();
    }
}
