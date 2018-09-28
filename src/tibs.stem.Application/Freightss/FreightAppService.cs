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
using System.Threading.Tasks;
using tibs.stem.Authorization;
using tibs.stem.Freights;
using tibs.stem.Freightss.Dto;
using tibs.stem.Quotations;

namespace tibs.stem.Freightss
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_Freight)]
    public class FreightAppService : stemAppServiceBase, IFreightAppService
    {
        private readonly IRepository<Freight> _freightRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IAbpSession _session;
        private readonly IRepository<Quotation> _QuotationRepository;

        public FreightAppService(IRepository<Quotation> QuotationRepository, IRepository<Freight> freightRepository, IUnitOfWorkManager unitOfWorkManager, IAbpSession session)
        {
            _freightRepository = freightRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _session = session;
            _QuotationRepository = QuotationRepository;
        }

        public ListResultDto<FreightListDto> GetFreight(GetFreightInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                var query = _freightRepository.GetAll()
                 .WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    u =>
                        u.FreightCode.Contains(input.Filter) ||
                        u.FreightName.Contains(input.Filter))
                .ToList();

                return new ListResultDto<FreightListDto>(query.MapTo<List<FreightListDto>>());
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_Freight_Edit)]
        public async Task<GetFreight> GetFreightForEdit(NullableIdDto input)
        {
            var output = new GetFreight
            {
            };

            var freight = _freightRepository
                .GetAll().Where(p => p.Id == input.Id).FirstOrDefault();
            output.freight = freight.MapTo<CreateFreightInput>();

            return output;

        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_Freight_Create)]
        public async Task CreateOrUpdateFreight(CreateFreightInput input)
        {
            if (input.Id != 0)
            {
                await UpdateFreight(input);
            }
            else
            {
                await CreateFreight(input);
            }
        }
        public async Task CreateFreight(CreateFreightInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                input.TenantId = (int)_session.TenantId;
                var freight = input.MapTo<Freight>();
                var val = _freightRepository
                 .GetAll().Where(p => p.FreightCode == input.FreightCode || p.FreightName == input.FreightName).FirstOrDefault();

                if (val == null)
                {
                    await _freightRepository.InsertAsync(freight);
                }
                else
                {
                    throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in FreightCode '" + input.FreightCode + "' orFreightName '" + input.FreightName + "'...");
                }
            }
        }

        public async Task UpdateFreight(CreateFreightInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                input.TenantId = (int)_session.TenantId;
                var freight = input.MapTo<Freight>();

                var val = _freightRepository
                .GetAll().Where(p => (p.FreightCode == input.FreightCode || p.FreightName == input.FreightName) && p.Id != input.Id).FirstOrDefault();

                if (val == null)
                {
                    await _freightRepository.UpdateAsync(freight);
                }
                else
                {
                    throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in FreightCode '" + input.FreightCode + "' or FreightName  '" + input.FreightName + "'...");
                }
            }

        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_Freight_Delete)]
        public async Task GetDeleteFreight(EntityDto input)
        {
            var freight = _freightRepository.GetAll().Where(c => c.Id == input.Id);
            var f = (from c in freight
                     join r in _QuotationRepository.GetAll() on c.Id equals r.FreightId
                     select r).FirstOrDefault();
            if (f == null)
            {
                await _freightRepository.DeleteAsync(input.Id);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "freight cannot be deleted '");
            }

        }


    }
}
