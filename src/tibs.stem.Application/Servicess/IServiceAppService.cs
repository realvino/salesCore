using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Servicess.Dto;

namespace tibs.stem.Servicess
{
    public interface IServiceAppService : IApplicationService
    {
        ListResultDto<ServiceList> GetService(GetServiceInput input);
        Task<GetService> GetServiceForEdit(NullableIdDto input);
        Task CreateOrUpdateService(CreateServiceInput input);
        Task DeleteService(EntityDto input);

    }
}
