using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.NewInfoTypes.Dto;
using tibs.stem.Dto;
using tibs.stem.NewInfoTypes.Dto;

namespace tibs.stem.NewInfoTypes 
{
    interface IInfoTypeAppService : IApplicationService
    {
        Task<FileDto> GetInfoTypeToExcel();
        Task<PagedResultDto<NewInfoTypeListDto>> GetNewInfoType(GetNewInfoTypeInput input);
        Task<GetNewInfoType> GetNewInfoTypeForEdit(NullableIdDto input);
        Task CreateOrUpdateNewInfoType(NewInfoTypeInputDto input);

    }
}
