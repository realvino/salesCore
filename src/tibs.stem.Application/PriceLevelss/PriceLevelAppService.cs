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
using tibs.stem.PriceLevelProducts;
using tibs.stem.PriceLevels;
using tibs.stem.PriceLevelss.Dto;

namespace tibs.stem.PriceLevelss
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_ProductFamily_PriceLevel)]
    public class PriceLevelAppService : stemAppServiceBase, IPriceLevelAppService
    {
        private readonly IRepository<PriceLevel> _priceLevelRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IAbpSession _session;
        private readonly IRepository<PriceLevelProduct> _PriceLevelProductRepository;


        public PriceLevelAppService(IRepository<PriceLevel> priceLevelRepository, IRepository<PriceLevelProduct> PriceLevelProductRepository, IUnitOfWorkManager unitOfWorkManager, IAbpSession session)
        {
            _priceLevelRepository = priceLevelRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _session = session;
            _PriceLevelProductRepository = PriceLevelProductRepository;
        }
        public ListResultDto<PriceLevelList> GetPriceLevel(GetPriceLevelListInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                var query = _priceLevelRepository.GetAll()
                 .WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    u =>
                        u.PriceLevelCode.Contains(input.Filter) ||
                        u.PriceLevelName.Contains(input.Filter))
                .ToList();

                return new ListResultDto<PriceLevelList>(query.MapTo<List<PriceLevelList>>());
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_ProductFamily_PriceLevel_Edit)]
        public async Task<GetPriceLevel> GetPriceLevelForEdit(NullableIdDto input)
        {
            var output = new GetPriceLevel
            {
            };

            var payment = _priceLevelRepository
                .GetAll().Where(p => p.Id == input.Id).FirstOrDefault();
            output.GetPriceLevels = payment.MapTo<PriceLevelCreate>();

            return output;

        }
        [AbpAuthorize(AppPermissions.Pages_Tenant_ProductFamily_PriceLevel_Create)]
        public async Task CreateOrUpdatePriceLevel(PriceLevelCreate input)
        {
            if (input.Id != 0)
            {
                await UpdatePriceLevel(input);
            }
            else
            {
                await CreatePriceLevel(input);
            }
        }
        public async Task CreatePriceLevel(PriceLevelCreate input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                input.TenantId = (int)_session.TenantId;
                var quey = input.MapTo<PriceLevel>();
                var val = _priceLevelRepository
                 .GetAll().Where(p => p.PriceLevelCode == input.PriceLevelCode || p.PriceLevelName == input.PriceLevelName).FirstOrDefault();

                if (val == null)
                {
                    await _priceLevelRepository.InsertAsync(quey);
                }
                else
                {
                    throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in PriceLevelCode '" + input.PriceLevelCode + "' or PriceLevelName '" + input.PriceLevelName + "'...");
                }
            }
        }
        public async Task UpdatePriceLevel(PriceLevelCreate input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                input.TenantId = (int)_session.TenantId;
                var quey = input.MapTo<PriceLevel>();

                var val = _priceLevelRepository
                .GetAll().Where(p => (p.PriceLevelCode == input.PriceLevelCode || p.PriceLevelName == input.PriceLevelName) && p.Id != input.Id).FirstOrDefault();

                if (val == null)
                {
                    await _priceLevelRepository.UpdateAsync(quey);
                }
                else
                {
                    throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in PriceLevelCode '" + input.PriceLevelCode + "' or PriceLevelName '" + input.PriceLevelName + "'...");
                }
            }

        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_ProductFamily_PriceLevel_Delete)]
        public async Task DeletePriceLevel(EntityDto input)
        {
            int price = (from r in _PriceLevelProductRepository.GetAll() where r.PriceLevelId == input.Id select r).Count();
            if (price <= 0)
            {
                await _priceLevelRepository.DeleteAsync(input.Id);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "PriceLevel cannot be deleted");
            }
        }

    }
}
