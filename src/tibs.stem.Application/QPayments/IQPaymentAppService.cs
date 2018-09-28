using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.QPayments.Dto;

namespace tibs.stem.QPayments
{
    public interface IQPaymentAppService : IApplicationService
    {
        ListResultDto<QPaymentListDto> GetPayments(GetQPaymentInput input);
        Task<GetQPayments> GetPaymentForEdit(NullableIdDto input);
        Task CreateOrUpdatePayment(QPaymentInputDto input);
        Task GetDeletePayment(EntityDto input);
    }
}
