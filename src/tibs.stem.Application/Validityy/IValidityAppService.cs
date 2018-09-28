using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Validityy.Dto;

namespace tibs.stem.Validityy
{
    public interface IValidityAppService : IApplicationService
    {
        ListResultDto<ValidityList> GetValidity(GetValidityInput input);
        Task<GetValidity> GetValidityForEdit(NullableIdDto input);
        Task CreateOrUpdateValidity(CreateValidityInput input);
        Task GetDeleteValidity(EntityDto input);

    }
}
