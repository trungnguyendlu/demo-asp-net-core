using System.Threading.Tasks;
using Demo.Data;
using Demo.Entity;

namespace Demo.BusinessLogic
{
    public partial class WidgetService
    {
        public Task<BaseFindResponse<Widget>> FindAsync(WidgetFindRequest request)
        {
            return _widgetRepository.FindAsync(request);
        }

    }
}
