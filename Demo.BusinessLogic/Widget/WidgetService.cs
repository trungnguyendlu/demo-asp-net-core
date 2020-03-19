using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Demo.Entity;
using Demo.Model.Admin.Widget;
using Demo.Data;
using Demo.Repository;
using Demo.Util;

namespace Demo.BusinessLogic
{
    public partial class WidgetService : BaseService<Widget>, IWidgetService
    {
        private readonly IWidgetRepository _widgetRepository;
        private readonly IMapper _mapper;
        private readonly ICacheHelper _cache;

        public WidgetService(IWidgetRepository widgetRepository, IMapper mapper, ICacheHelper cache)
            : base(widgetRepository)
        {
            _widgetRepository = widgetRepository;
            _mapper = mapper;
            _cache = cache;
        }

        public Task<List<WidgetModel>> GetWidgetsAsync()
        {
            return _cache.GetOrSetAsync(CacheKey.GetAllWidgets, async() =>
            {
                return _mapper.Map<List<WidgetModel>>(await _widgetRepository.GetWidgetsAsync());
            });
        }

        public async Task<WidgetModel> GetByPositionAsync(WidgetPosition position)
        {
            return _mapper.Map<WidgetModel>(await _widgetRepository.GetByPositionAsync(position));
        }
    }
}
