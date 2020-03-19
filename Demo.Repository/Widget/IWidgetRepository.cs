using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.Entity;
using Demo.Data;

namespace Demo.Repository
{
    public partial interface IWidgetRepository : IBaseRepository<Widget>
    {
        Task<BaseFindResponse<Widget>> FindAsync(WidgetFindRequest request);
        Task<List<Widget>> GetWidgetsAsync();
        Task<Widget> GetByPositionAsync(WidgetPosition position);
    }
}