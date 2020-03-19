using Demo.Entity;
using Demo.Repository;

namespace Demo.BusinessLogic
{
    public class SettingsService : BaseService<Settings>, ISettingsService
    {
        public SettingsService(ISettingsRepository settingRepository) 
            : base(settingRepository)
        {
        }
    }
}
