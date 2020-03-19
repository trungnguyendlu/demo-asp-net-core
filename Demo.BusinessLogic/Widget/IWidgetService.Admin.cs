using System.Threading.Tasks;
using Demo.Data;
using Demo.Entity;

namespace Demo.BusinessLogic
{
    public partial interface IWidgetService
    {
        Task<BaseFindResponse<Widget>> FindAsync(WidgetFindRequest request);
    }
}