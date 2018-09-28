using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Dto;
using tibs.stem.NewCustomerTypes.Dto;

namespace tibs.stem.NewCustomerTypes
{
    interface ICustomerTypeAppService : IApplicationService
    {
        Task<FileDto> GetCustomerTypeExcel();
        Task<PagedResultDto<NewCustomerTypeListDto>> GetNewCustomerType(GetNewCustomerTypeInput input);
        Task<GetNewCustomerType> GetNewCustomerTypeForEdit(NullableIdDto input);
        Task CreateOrUpdateNewCustomerType(NewCustomerTypeInputDto input);

    }
}
