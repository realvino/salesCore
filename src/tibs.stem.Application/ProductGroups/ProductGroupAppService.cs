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
using tibs.stem.Dto;
using tibs.stem.ProductGroups.Dto;
using tibs.stem.ProductGroups.Exporting;
using tibs.stem.ProductSubGroups;

namespace tibs.stem.ProductGroups
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_ProductFamily_ProductGroup)]
    public class ProductGroupAppService : stemAppServiceBase, IProductGroupAppService
    {
        private readonly IRepository<ProductGroup> _productGroupRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IAbpSession _session;
        private readonly IRepository<ProductSubGroup> _productSubGroupRepository;
        private readonly IProductGroupExcelExporter _productGroupExcelExporter;

        public ProductGroupAppService(IProductGroupExcelExporter productGroupExcelExporter, IRepository<ProductGroup> productGroupRepository, IUnitOfWorkManager unitOfWorkManager, IAbpSession session, IRepository<ProductSubGroup> productSubGroupRepository)
        {
            _productGroupExcelExporter = productGroupExcelExporter;
            _productGroupRepository = productGroupRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _session = session;
            _productSubGroupRepository = productSubGroupRepository;
        }

        public async Task<FileDto> GetProductGroupToExcel()
        {

            var query = _productGroupRepository.GetAll();
            var data = (from r in query
                        select new ProductGroupListDto
                        {

                            Id = r.Id,
                            ProductGroupCode = r.ProductGroupCode,
                            ProductGroupName = r.ProductGroupName,

                        }).ToList();

            var ProductGroupListDtos = data.MapTo<List<ProductGroupListDto>>();

            return _productGroupExcelExporter.ExportToFile(ProductGroupListDtos);
        }

        public ListResultDto<ProductGroupListDto> GetProductGroup(GetProductGroupInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {

                var query = _productGroupRepository.GetAll()
                 .WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    u =>
                        u.ProductGroupCode.Contains(input.Filter) ||
                        u.ProductGroupName.Contains(input.Filter) )
                .ToList();

                return new ListResultDto<ProductGroupListDto>(query.MapTo<List<ProductGroupListDto>>());
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_ProductFamily_ProductGroup_Edit)]
        public async Task<GetProductGroup> GetProductGroupForEdit(NullableIdDto input)
        {
            var output = new GetProductGroup
            {
            };

            var productgroup = _productGroupRepository
                .GetAll().Where(p => p.Id == input.Id).FirstOrDefault();
                output.productGroup = productgroup.MapTo<CreateProductGroupInput>();
                
            return output;

        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_ProductFamily_ProductGroup_Create)]
        public async Task CreateOrUpdateProductGroup(CreateProductGroupInput input)
        {
            if (input.Id != 0)
            {
                await UpdateProductGroup(input);
            }
            else
            {
                await CreateProductGroup(input);
            }
        }

        public async Task CreateProductGroup(CreateProductGroupInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                input.TenantId = (int)_session.TenantId;
                var productgroup = input.MapTo<ProductGroup>();
                var val = _productGroupRepository
                 .GetAll().Where(p => p.ProductGroupName == input.ProductGroupName || p.ProductGroupCode == input.ProductGroupCode).FirstOrDefault();

                if (val == null)
                {
                    await _productGroupRepository.InsertAsync(productgroup);
                }
                else
                {
                    throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in ProductGroupName '" + input.ProductGroupName + "' or ProductGroupCode '" + input.ProductGroupCode + "'...");
                }
            }
        }

        public async Task UpdateProductGroup(CreateProductGroupInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                input.TenantId = (int)_session.TenantId;
                var productGroup = input.MapTo<ProductGroup>();

                var val = _productGroupRepository
                .GetAll().Where(p => (p.ProductGroupName == input.ProductGroupName || p.ProductGroupCode == input.ProductGroupCode) && p.Id != input.Id).FirstOrDefault();

                if (val == null)
                {
                    await _productGroupRepository.UpdateAsync(productGroup);
                }
                else
                {
                    throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in productGroup Name '" + input.ProductGroupName + "' or productGroup Code '" + input.ProductGroupCode + "'...");
                }
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_ProductFamily_ProductGroup_Delete)]
        public async Task GetDeleteProductGroup(EntityDto input)
        {
            var product = _productGroupRepository.GetAll().Where(c => c.Id == input.Id);
            var p = (from c in product
                     join r in _productSubGroupRepository.GetAll() on c.Id equals r.ProductGroupId
                     select r).FirstOrDefault();
            if (p == null)
            {
                await _productGroupRepository.DeleteAsync(input.Id);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "ProductGroup cannot be deleted '");
            }
        }
    }
}
