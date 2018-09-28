using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using Abp.Linq.Extensions;
using Abp.AutoMapper;
using Abp.UI;
using System.Data.SqlClient;
using tibs.stem.ProductSubGroups.Dto;
using tibs.stem.ProductGroups;
using Abp.Domain.Uow;
using Abp.Runtime.Session;
using tibs.stem.Products;
using tibs.stem.ProductSubGroups.Exporting;
using tibs.stem.Dto;
using Abp.Authorization;
using tibs.stem.Authorization;

namespace tibs.stem.ProductSubGroups
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_ProductFamily_ProductSubgroup)]
    public class ProductSubGroupAppService : stemAppServiceBase, IProductSubGroupAppService
    {
        private readonly IRepository<ProductSubGroup> _productSubGroupRepository;
        private readonly IRepository<ProductGroup> _productGroupRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IAbpSession _session;
        private readonly IRepository<Product> _ProductRepository;
        private readonly IProductSubGroupExcelExporter _productSubGroupExcelExporter;

        public ProductSubGroupAppService(IProductSubGroupExcelExporter productSubGroupExcelExporter, IRepository<ProductSubGroup> productSubGroupRepository, IRepository<Product> ProductRepository,IRepository<ProductGroup> productGroupRepository, IUnitOfWorkManager unitOfWorkManager, IAbpSession session)
        {
            _productSubGroupExcelExporter = productSubGroupExcelExporter;
            _productSubGroupRepository = productSubGroupRepository;
            _productGroupRepository = productGroupRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _session = session;
            _ProductRepository = ProductRepository;
        }
        public async Task<FileDto> GetProductSubGroupToExcel()
        {

            var query = _productSubGroupRepository.GetAll();
            var reg = from r in query
                      select new ProductSubGpListDto
                      {
                          Id = r.Id,
                          ProductSubGroupCode = r.ProductSubGroupCode,
                          ProductSubGroupName = r.ProductSubGroupName,
                          ProductGroupName = r.productGroups.ProductGroupName,
                          ProductGroupId = r.ProductGroupId
                      };


            var ProductSubGpListDtos = reg.MapTo<List<ProductSubGpListDto>>();

            return _productSubGroupExcelExporter.ExportToFile(ProductSubGpListDtos);
        }

        public async Task<PagedResultDto<ProductSubGpListDto>> GetProductSubGroup(GetProductSubGpInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                var query = _productSubGroupRepository.GetAll()
              .Include(u => u.productGroups)
                 .WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    u =>
                        u.ProductSubGroupCode.Contains(input.Filter) ||
                        u.ProductSubGroupName.Contains(input.Filter) ||
                        u.productGroups.ProductGroupName.Contains(input.Filter) ||
                        u.ProductGroupId.ToString().Contains(input.Filter)
                        );

                var reg = (from r in query
                           select new ProductSubGpListDto
                           {
                               Id = r.Id,
                               ProductSubGroupCode = r.ProductSubGroupCode,
                               ProductSubGroupName = r.ProductSubGroupName,
                               ProductGroupName = r.productGroups.ProductGroupName,
                               ProductGroupId = r.ProductGroupId,
                               Path = r.Path
                           });

                var Count = await reg.CountAsync();
                var reglist = await reg
                              .OrderBy(input.Sorting)
                              .PageBy(input)
                              .ToListAsync();

                var ProductSubGpListDtos = reglist.MapTo<List<ProductSubGpListDto>>();

                return new PagedResultDto<ProductSubGpListDto>(Count, ProductSubGpListDtos);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_ProductFamily_ProductSubgroup_Edit)]
        public async Task<GetProductSubGp> GetProductSubGroupForEdit(EntityDto input)
        {
            var output = new GetProductSubGp
            {
            };
           
            var productsubgroup = _productSubGroupRepository
                .GetAll().Where(u => u.Id == input.Id).FirstOrDefault();





            output.productsubGroup = productsubgroup.MapTo<CreateProductSubGpInput>();
            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_ProductFamily_ProductSubgroup_Create)]
        public async Task CreateOrUpdateProductSubGroup(CreateProductSubGpInput input)
        {
            if (input.Id != 0)
            {
                await UpdateProductSubGroup(input);
            }
            else
            {
                await CreateProductSubGroup(input);
            }
        }

        public async Task CreateProductSubGroup(CreateProductSubGpInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                input.TenantId = (int)_session.TenantId;
                var productsubgroup = input.MapTo<ProductSubGroup>();
                var val = _productSubGroupRepository
                 .GetAll().Where(u => u.ProductSubGroupName == input.ProductSubGroupName || u.ProductSubGroupCode == input.ProductSubGroupCode).FirstOrDefault();

                if (val == null)
                {
                    await _productSubGroupRepository.InsertAsync(productsubgroup);
                }
                else
                {
                    throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in ProductSubGroupName '" + input.ProductSubGroupName + "' or ProductSubGroupCode '" + input.ProductSubGroupCode + "'...");
                }
            }
        }

        public async Task UpdateProductSubGroup(CreateProductSubGpInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                input.TenantId = (int)_session.TenantId;
                var productsubgroup = input.MapTo<ProductSubGroup>();


                var val = _productSubGroupRepository
                .GetAll().Where(u => (u.ProductSubGroupName == input.ProductSubGroupName || u.ProductSubGroupCode == input.ProductSubGroupCode) && u.Id != input.Id).FirstOrDefault();

                if (val == null)
                {
                    await _productSubGroupRepository.UpdateAsync(productsubgroup);
                }
                else
                {
                    throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in productSubGroup Name '" + input.ProductSubGroupName + "' or productSubGroup Code '" + input.ProductSubGroupCode + "'...");
                }
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_ProductFamily_ProductSubgroup_Delete)]
        public async Task GetDeleteProductSubGroup(EntityDto input)
        {
            var product = _productSubGroupRepository.GetAll().Where(c => c.Id == input.Id);

            var p = (from c in product
                     join r in _ProductRepository.GetAll() on c.Id equals r.ProductSubGroupId
                     select r).FirstOrDefault();
            if (p == null)
            {
                await _productSubGroupRepository.DeleteAsync(input.Id);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "ProductSubGroup cannot be deleted '");
            }
        }


    }
}
