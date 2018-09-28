using System.Threading.Tasks;
using tibs.stem.Sessions.Dto;

namespace tibs.stem.Web.Session
{
    public interface IPerRequestSessionCache
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformationsAsync();
    }
}
