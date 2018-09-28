using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Deliveryy.Dto;

namespace tibs.stem.Deliveryy
{
    public interface IDeliveryAppService : IApplicationService
    {
        ListResultDto<DeliveryList> GetDelivery(GetDeliveryInput input);
        Task<GetDelivery> GetDeliveryForEdit(NullableIdDto input);
        Task CreateOrUpdateDelivery(CreateDeliveryInput input);
        Task GetDeleteDelivery(EntityDto input);
    }
}
