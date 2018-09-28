using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Runtime.Session;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Authorization;
using tibs.stem.QPayments.Dto;
using tibs.stem.Quotations;

namespace tibs.stem.QPayments
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_QPayment)]
    public class QPaymentAppService : stemAppServiceBase, IQPaymentAppService
    {
        private readonly IRepository<QPayment> _QPaymentRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IAbpSession _session;
        private readonly IRepository<Quotation> _QuotationRepository;
 
        public QPaymentAppService(IRepository<QPayment> QPaymentRepository, IUnitOfWorkManager unitOfWorkManager, IAbpSession session, IRepository<Quotation> QuotationRepository)
        {
            _QPaymentRepository = QPaymentRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _session = session;
            _QuotationRepository = QuotationRepository;
        }

        public ListResultDto<QPaymentListDto> GetPayments(GetQPaymentInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                var query = _QPaymentRepository.GetAll()
                 .WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    u =>
                        u.PaymentCode.Contains(input.Filter) ||
                        u.PaymentName.Contains(input.Filter))
                .ToList();

                return new ListResultDto<QPaymentListDto>(query.MapTo<List<QPaymentListDto>>());
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_QPayment_Edit)]
        public async Task<GetQPayments> GetPaymentForEdit(NullableIdDto input)
        {
            var output = new GetQPayments
            {
            };

            var payment = _QPaymentRepository
                .GetAll().Where(p => p.Id == input.Id).FirstOrDefault();
            output.GetPayments = payment.MapTo<QPaymentInputDto>();

            return output;

        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_QPayment_Create)]
        public async Task CreateOrUpdatePayment(QPaymentInputDto input)
        {
            if (input.Id != 0)
            {
                await UpdatePayment(input);
            }
            else
            {
                await CreatePayment(input);
            }
        }

        public async Task CreatePayment(QPaymentInputDto input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                input.TenantId = (int)_session.TenantId;
                var pack = input.MapTo<QPayment>();
                var val = _QPaymentRepository
                 .GetAll().Where(p => p.PaymentCode == input.PaymentCode || p.PaymentName == input.PaymentName).FirstOrDefault();

                if (val == null)
                {
                    await _QPaymentRepository.InsertAsync(pack);
                }
                else
                {
                    throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in PaymentCode '" + input.PaymentCode + "' or PaymentName '" + input.PaymentName + "'...");
                }
            }
        }
        public async Task UpdatePayment(QPaymentInputDto input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                input.TenantId = (int)_session.TenantId;
                var pack = input.MapTo<QPayment>();

                var val = _QPaymentRepository
                .GetAll().Where(p => (p.PaymentCode == input.PaymentCode || p.PaymentName == input.PaymentName) && p.Id != input.Id).FirstOrDefault();

                if (val == null)
                {
                    await _QPaymentRepository.UpdateAsync(pack);
                }
                else
                {
                    throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in PaymentCode '" + input.PaymentCode + "' or PaymentName '" + input.PaymentName + "'...");
                }
            }

        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_QPayment_Delete)]
        public async Task GetDeletePayment(EntityDto input)
        {
            var Qpayment = _QPaymentRepository.GetAll().Where(c => c.Id == input.Id);
            var p = (from c in Qpayment
                     join r in _QuotationRepository.GetAll() on c.Id equals r.PaymentId
                     select r).FirstOrDefault();
            if (p == null)
            {
                await _QPaymentRepository.DeleteAsync(input.Id);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "payment cannot be deleted '");
            }
        }
    }
}
