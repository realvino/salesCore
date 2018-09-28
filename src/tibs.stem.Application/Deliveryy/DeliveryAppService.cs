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
using tibs.stem;
using tibs.stem.Authorization;
using tibs.stem.Deliverys;
using tibs.stem.Deliveryy;
using tibs.stem.Deliveryy.Dto;
using tibs.stem.Quotations;

namespace tibs.sc.Deliveryy
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_Delivery)]
    public class DeliveryAppService : stemAppServiceBase, IDeliveryAppService
    {
        private readonly IRepository<Delivery> _DeliveryRepository;
        private readonly IRepository<Quotation> _QuotationRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IAbpSession _session;
        public DeliveryAppService(IRepository<Delivery> DeliveryRepository, IUnitOfWorkManager unitOfWorkManager, IAbpSession session, IRepository<Quotation> QuotationRepository)
        {
            _DeliveryRepository = DeliveryRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _session = session;
            _QuotationRepository = QuotationRepository;
        }

        
        public ListResultDto<DeliveryList> GetDelivery(GetDeliveryInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                var query = _DeliveryRepository.GetAll()
                 .WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    u =>
                        u.DeliveryCode.Contains(input.Filter) ||
                        u.DeliveryName.Contains(input.Filter))
                .ToList();

                return new ListResultDto<DeliveryList>(query.MapTo<List<DeliveryList>>());
            }  
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_Delivery_Edit)]
        public async Task<GetDelivery> GetDeliveryForEdit(NullableIdDto input)
        {
            var output = new GetDelivery
            {
            };

            var Delivery = _DeliveryRepository
                .GetAll().Where(p => p.Id == input.Id).FirstOrDefault();
            output.delivery = Delivery.MapTo<CreateDeliveryInput>();

            return output;

        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_Delivery_Create)]
        public async Task CreateOrUpdateDelivery(CreateDeliveryInput input)
        {
            if (input.Id != 0)
            {
                await UpdateDelivery(input);
            }
            else
            {
                await CreateDelivery(input);
            }
        }

        public async Task CreateDelivery(CreateDeliveryInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                input.TenantId = (int)_session.TenantId;
                var Delivery = input.MapTo<Delivery>();
                var val = _DeliveryRepository
                 .GetAll().Where(p => p.DeliveryCode == input.DeliveryCode || p.DeliveryName == input.DeliveryName).FirstOrDefault();

                if (val == null)
                {
                    await _DeliveryRepository.InsertAsync(Delivery);
                }
                else
                {
                    throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in DeliveryName '" + input.DeliveryName + "' or DeliveryCode '" + input.DeliveryCode + "'...");
                }
            }
        }

        public async Task UpdateDelivery(CreateDeliveryInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                input.TenantId = (int)_session.TenantId;
                var Delivery = input.MapTo<Delivery>();

                var val = _DeliveryRepository
                .GetAll().Where(p => (p.DeliveryCode == input.DeliveryCode || p.DeliveryName == input.DeliveryName) && p.Id != input.Id).FirstOrDefault();

                if (val == null)
                {
                    await _DeliveryRepository.UpdateAsync(Delivery);
                }
                else
                {
                    throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in DeliveryName '" + input.DeliveryName + "' or DeliveryCode '" + input.DeliveryCode + "'...");
                }
            }

        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_Delivery_Delete)]
        public async Task GetDeleteDelivery(EntityDto input)
        {
            var delivery = _DeliveryRepository.GetAll().Where(c => c.Id == input.Id);
            var d = (from c in delivery
                     join r in _QuotationRepository.GetAll() on c.Id equals r.DeliveryId
                     select r).FirstOrDefault();
            if (d == null)
            {
                await _DeliveryRepository.DeleteAsync(input.Id);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "delivery cannot be deleted '");
            }
        }
    }
}
