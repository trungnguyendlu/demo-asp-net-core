using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.Entity;
using Demo.Model.Admin.Widget;
using Demo.Data;

namespace Demo.BusinessLogic
{
    public partial interface IWidgetService : IBaseService<Widget>
    {
        Task<List<WidgetModel>> GetWidgetsAsync();
        Task<WidgetModel> GetByPositionAsync(WidgetPosition position);
    }
}