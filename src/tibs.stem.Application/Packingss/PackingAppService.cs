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
using tibs.stem.Packings;
using tibs.stem.Packingss.Dto;
using tibs.stem.Quotations;

namespace tibs.stem.Packingss
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_Packing)]
    public class PackingAppService : stemAppServiceBase, IPackingAppService
    {
        private readonly IRepository<Packing> _packingRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IAbpSession _session;
        private readonly IRepository<Quotation> _QuotationRepository;

        public PackingAppService(IRepository<Packing> packingRepository, IUnitOfWorkManager unitOfWorkManager, IAbpSession session, IRepository<Quotation> QuotationRepository)
        {
            _packingRepository = packingRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _session = session;
            _QuotationRepository = QuotationRepository;
        }

        public ListResultDto<PackingListDto> GetPacking(GetPackingInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                var query = _packingRepository.GetAll()
                 .WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    u =>
                        u.PackingCode.Contains(input.Filter) ||
                        u.PackingName.Contains(input.Filter))
                .ToList();

                return new ListResultDto<PackingListDto>(query.MapTo<List<PackingListDto>>());
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_Packing_Edit)]
        public async Task<GetPacking> GetPackingForEdit(NullableIdDto input)
        {
            var output = new GetPacking
            {
            };

            var pack = _packingRepository
                .GetAll().Where(p => p.Id == input.Id).FirstOrDefault();
            output.pack = pack.MapTo<CreatePackingInput>();

            return output;

        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_Packing_Create)]
        public async Task CreateOrUpdatePacking(CreatePackingInput input)
        {
            if (input.Id != 0)
            {
                await UpdatePacking(input);
            }
            else
            {
                await CreatePacking(input);
            }
        }

        public async Task CreatePacking(CreatePackingInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                input.TenantId = (int)_session.TenantId;
                var pack = input.MapTo<Packing>();
                var val = _packingRepository
                 .GetAll().Where(p => p.PackingCode == input.PackingCode || p.PackingName == input.PackingName).FirstOrDefault();

                if (val == null)
                {
                    await _packingRepository.InsertAsync(pack);
                }
                else
                {
                    throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in PackingCode '" + input.PackingCode + "' orPackingName '" + input.PackingName + "'...");
                }
            }
        }
        public async Task UpdatePacking(CreatePackingInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                input.TenantId = (int)_session.TenantId;
                var pack = input.MapTo<Packing>();

                var val = _packingRepository
                .GetAll().Where(p => (p.PackingCode == input.PackingCode || p.PackingName == input.PackingName) && p.Id != input.Id).FirstOrDefault();

                if (val == null)
                {
                    await _packingRepository.UpdateAsync(pack);
                }
                else
                {
                    throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in PackingCode '" + input.PackingCode + "' or PackingName  '" + input.PackingName + "'...");
                }
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_Packing_Delete)]
        public async Task GetDeletePacking(EntityDto input)
        {
            var packing = _packingRepository.GetAll().Where(c => c.Id == input.Id);
            var f = (from c in packing
                     join r in _QuotationRepository.GetAll() on c.Id equals r.PackingId
                     select r).FirstOrDefault();
            if (f == null)
            {
                await _packingRepository.DeleteAsync(input.Id);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "packing cannot be deleted '");
            }
        }

    }
}
