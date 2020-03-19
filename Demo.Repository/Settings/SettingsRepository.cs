using Demo.Entity;

namespace Demo.Repository
{
    public class SettingsRepository : BaseRepository<Settings>, ISettingsRepository
    {
        public SettingsRepository(DatabaseContext context) : base(context, context.Settings)
        {
        }

        //protected override void CreateIndex(LiteCollection<Settings> collection)
        //{
        //}

        protected override string Title => "Thiết Lập Hệ Thống";
    }
}
