using Abp.Application.Services;
using tibs.stem.Dto;
using tibs.stem.Logging.Dto;

namespace tibs.stem.Logging
{
    public interface IWebLogAppService : IApplicationService
    {
        GetLatestWebLogsOutput GetLatestWebLogs();

        FileDto DownloadWebLogs();
    }
}
