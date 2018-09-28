using System.Threading.Tasks;
using Abp.Application.Services;
using tibs.stem.Sessions.Dto;

namespace tibs.stem.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();

        Task<UpdateUserSignInTokenOutput> UpdateUserSignInToken();
    }
}
