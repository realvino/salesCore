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
using tibs.stem.Quotations;
using tibs.stem.Warrantys;
using tibs.stem.Warrantyss.Dto;

namespace tibs.stem.Warrantyss
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_Warranty)]
    public class WarrantyAppService : stemAppServiceBase, IWarrantyAppService
    {
        private readonly IRepository<Warranty> _warrantyRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IAbpSession _session;
        private readonly IRepository<Quotation> _QuotationRepository;
 
 
        public WarrantyAppService(IRepository<Warranty> warrantyRepository, IUnitOfWorkManager unitOfWorkManager, IAbpSession session, IRepository<Quotation> QuotationRepository)
        {
            _warrantyRepository = warrantyRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _session = session;
            _QuotationRepository = QuotationRepository;
        }

        public ListResultDto<WarrantyListDto> GetWarranty(GetWarrantyInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                var query = _warrantyRepository.GetAll()
                 .WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    u =>
                        u.WarrantyCode.Contains(input.Filter) ||
                        u.WarrantyName.Contains(input.Filter))
                .ToList();

                return new ListResultDto<WarrantyListDto>(query.MapTo<List<WarrantyListDto>>());
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_Warranty_Edit)]
        public async Task<GetWarranty> GetWarrantyForEdit(NullableIdDto input)
        {
            var output = new GetWarranty
            {
            };

            var warranty = _warrantyRepository
                .GetAll().Where(p => p.Id == input.Id).FirstOrDefault();
            output.warranty = warranty.MapTo<CreateWarrantyInput>();

            return output;

        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_Warranty_Create)]
        public async Task CreateOrUpdateWarranty(CreateWarrantyInput input)
        {
            if (input.Id != 0)
            {
                await UpdateWarranty(input);
            }
            else
            {
                await CreateWarranty(input);
            }
        }

        public async Task CreateWarranty(CreateWarrantyInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                input.TenantId = (int)_session.TenantId;
                var warranty = input.MapTo<Warranty>();
                var val = _warrantyRepository
                 .GetAll().Where(p => p.WarrantyCode == input.WarrantyCode || p.WarrantyName == input.WarrantyName).FirstOrDefault();

                if (val == null)
                {
                    await _warrantyRepository.InsertAsync(warranty);
                }
                else
                {
                    throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in WarrantyCode '" + input.WarrantyCode + "' orWarrantyName '" + input.WarrantyName + "'...");
                }
            }
        }
        public async Task UpdateWarranty(CreateWarrantyInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                input.TenantId = (int)_session.TenantId;
                var warranty = input.MapTo<Warranty>();

                var val = _warrantyRepository
                .GetAll().Where(p => (p.WarrantyCode == input.WarrantyCode || p.WarrantyName == input.WarrantyName) && p.Id != input.Id).FirstOrDefault();

                if (val == null)
                {
                    await _warrantyRepository.UpdateAsync(warranty);
                }
                else
                {
                    throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in WarrantyCode '" + input.WarrantyCode + "' or WarrantyName  '" + input.WarrantyName + "'...");
                }
            }

        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_Warranty_Delete)]
        public async Task GetDeleteWarranty(EntityDto input)
        {
            var warranty = _warrantyRepository.GetAll().Where(c => c.Id == input.Id);
            var w = (from c in warranty
                     join r in _QuotationRepository.GetAll() on c.Id equals r.WarrantyId
                     select r).FirstOrDefault();
            if (w == null)
            {
                await _warrantyRepository.DeleteAsync(input.Id);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "warranty cannot be deleted '");
            }
        }

    }
}
