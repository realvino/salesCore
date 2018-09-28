using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Dto;
using tibs.stem.PriceLevelProducts;
using tibs.stem.PriceLevels;
using tibs.stem.Products;
using tibs.stem.Productss.Dto;
using tibs.stem.Productss.Exporting;
using tibs.stem.QuotationProducts;
using System.Linq.Dynamic.Core;
using tibs.stem.Authorization;
using Abp.Authorization;

namespace tibs.stem.Productss
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_ProductFamily_Products)]
    public class ProductAppService : stemAppServiceBase, IProductAppService
    {

        public readonly IRepository<Product> _ProductRepository;
        public readonly IRepository<PriceLevel> _PriceLevelRepository;
        public readonly IRepository<PriceLevelProduct> _PriceLevelProductRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IAbpSession _session;
        private readonly IRepository<QuotationProduct> _QuotationProductRepository;
        private readonly IProductListExcelExporter _productListExcelExporter;

        public ProductAppService(IProductListExcelExporter productListExcelExporter, IRepository<PriceLevelProduct> PriceLevelProductRepository, IRepository<QuotationProduct> QuotationProductRepository,IRepository<PriceLevel> PriceLevelRepository, IRepository<Product> ProductRepository, IUnitOfWorkManager unitOfWorkManager, IAbpSession session)
        {
            _productListExcelExporter = productListExcelExporter;
            _PriceLevelRepository = PriceLevelRepository;
            _PriceLevelProductRepository = PriceLevelProductRepository;
            _ProductRepository = ProductRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _session = session;
            _QuotationProductRepository = QuotationProductRepository;
        }
        public async Task<FileDto> GetProductToExcel()
        {

            var query = _ProductRepository.GetAll();
            var data = (from r in query
                        select new ProductListDto
                        {

                            Id = r.Id,
                            ProductCode = r.ProductCode,
                            ProductName = r.ProductName,
                            Description = r.Description,
                            Discontinued = r.Discontinued,
                            ProductSubGroupId = r.ProductSubGroupId,
                            ProductSubGroupName = r.ProductSubGroup.ProductSubGroupName,
                            ProducGroupId = r.ProductSubGroup.ProductGroupId,
                            ProductGroupName = r.ProductSubGroup.productGroups.ProductGroupName

                        });


            var ProductListDtos = data.MapTo<List<ProductListDto>>();

            return _productListExcelExporter.ExportToFile(ProductListDtos);
        }

        public async Task<PagedResultDto<ProductListDto>> GetProduct(GetProductInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                var Product = _ProductRepository.GetAll()

                              .WhereIf(
                               !input.Filter.IsNullOrEmpty(),
                                p => p.ProductCode.Contains(input.Filter) ||
                                p.ProductName.Contains(input.Filter) ||
                                p.ProductSubGroup.ProductSubGroupName.ToString().Contains(input.Filter) ||
                                p.ProductSubGroup.productGroups.ProductGroupName.ToString().Contains(input.Filter)
                               );

                var data = (from r in Product
                            select new ProductListDto
                            {

                                Id = r.Id,
                                ProductCode = r.ProductCode,
                                ProductName = r.ProductName,
                                Description = r.Description,
                                Discontinued = r.Discontinued,
                                ProductSubGroupId = r.ProductSubGroupId,
                                ProductSubGroupName = r.ProductSubGroup.ProductSubGroupName,
                                ProducGroupId = r.ProductSubGroup.ProductGroupId,
                                ProductGroupName = r.ProductSubGroup.productGroups.ProductGroupName,
                                Path = r.Path

                            });

                var Count = await data.CountAsync();
                var datalist = await data
                              .OrderBy(input.Sorting)
                              .PageBy(input)
                              .ToListAsync();

                var ProductListDtos = datalist.MapTo<List<ProductListDto>>();

                return new PagedResultDto<ProductListDto>(Count, ProductListDtos);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_ProductFamily_Products_Edit)]
        public async Task<GetProduct> GetProductForEdit(EntityDto input)
        {
            var output = new GetProduct { };
            var Product = _ProductRepository.GetAll().Where(p => p.Id == input.Id);

            var data = (from r in Product
                        select new ProductListDto
                        {
                            Id = r.Id,
                            ProductCode = r.ProductCode,
                            ProductName = r.ProductName,
                            Description = r.Description,
                            Discontinued = r.Discontinued,
                            ProductSubGroupId = r.ProductSubGroupId,
                            ProductSubGroupName = r.ProductSubGroup.ProductSubGroupName,
                            ProducGroupId = r.ProductSubGroup.ProductGroupId,
                            ProductGroupName = r.ProductSubGroup.productGroups.ProductGroupName,
                            Path = r.Path
                        }).FirstOrDefault();

            output.Products = data.MapTo<ProductListDto>();           
            return output;
            
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_ProductFamily_Products_Create)]
        public async Task CreateOrUpdateProduct(CreateProductInput input)
        {

            if (input.Id == 0)
            {
                await CreateProduct(input);
            }
            else
            {
                await UpdateProduct(input);
            }
        }

        public virtual async Task CreateProduct(CreateProductInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                input.TenantId = (int)_session.TenantId;
                var Product = input.MapTo<Product>();
                var val = _ProductRepository
                  .GetAll().Where(p => p.ProductCode == input.ProductCode || p.ProductName == input.ProductName).FirstOrDefault();
                if (val == null)
                {
                    await _ProductRepository.InsertAsync(Product);
                }
                else
                {
                    throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Product Name '" + input.ProductName + "' or Product Code '" + input.ProductCode + "'...");
                }
            }

        }

        public virtual async Task UpdateProduct(CreateProductInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                input.TenantId = (int)_session.TenantId;
                var Product = input.MapTo<Product>();
                var query = _ProductRepository.GetAll()
                    .Where(p => (p.ProductCode == input.ProductCode || p.ProductName == input.ProductName) && p.Id != input.Id).FirstOrDefault();
                if (query == null)
                {
                    await _ProductRepository.UpdateAsync(Product);
                }
                else
                {
                    throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Product ...");

                }
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_ProductFamily_Products_Delete)]
        public async Task GetDeleteProduct(EntityDto input)
        {
            var product = _ProductRepository.GetAll().Where(c => c.Id == input.Id);
            var p = (from c in product
                     join r in _QuotationProductRepository.GetAll() on c.Id equals r.ProductId
                     select r).FirstOrDefault();
            if (p == null)
            {
                ConnectionAppService db = new ConnectionAppService();
                DataTable ds = new DataTable();
                using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
                {
                    SqlCommand sqlComm = new SqlCommand("Sp_DeleteAllDetail", conn);
                    sqlComm.Parameters.AddWithValue("@Id", input.Id);
                    sqlComm.Parameters.AddWithValue("@TableId", 2);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    sqlComm.ExecuteNonQuery();
                    conn.Close();
                }
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Product cannot be deleted");
            }
        }

        public ListResultDto<PriceLevelProductList> GetProductLevelPriceList(PriceLevelProductListInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                var query = _PriceLevelProductRepository.GetAll().Where(p => p.ProductId == input.ProductId);
                var pricelistdto = from c in query select new PriceLevelProductList { Id = c.Id, Price = c.Price, ProductId = c.ProductId, TenantId = c.TenantId, PriceLevelId = c.PriceLevelId, PriceLevelName = c.PriceLevels.PriceLevelName};
                return new ListResultDto<PriceLevelProductList>(pricelistdto.MapTo<List<PriceLevelProductList>>());
            }
        }

        public async Task<GetPriceLevelProduct> GetPriceLevelProductForEdit(NullableIdDto input)
        {
            var productlist = _PriceLevelProductRepository.GetAll().Where(p => p.ProductId == input.Id).ToList();
            var query = (from c in
                             _PriceLevelRepository.GetAll().Where((r) =>
                                 !(from mp in productlist
                                   where mp.IsDeleted == false
                                   select mp.PriceLevelId).Contains(r.Id))
                         select c).ToList();
            var pricelevel = (from p in query select new PriceLevelInput { Name = p.PriceLevelName, Id = p.Id }).ToArray();
            var output = new GetPriceLevelProduct { PriceLevel = pricelevel };
            return output;
        }

        public async Task CreateOrUpdatePriceLevelProduct(CreatePriceLevelProductInput input)
        {
            if (input.Id == 0)
            {
                await CreatePriceLevelProduct(input);
            }
            else
            {
                await UpdatePriceLevelProduct(input);
            }
        }

        public async Task CreatePriceLevelProduct(CreatePriceLevelProductInput input)
        {
            input.TenantId = (int)_session.TenantId;
            var quey = input.MapTo<PriceLevelProduct>();
            var val = _PriceLevelProductRepository
             .GetAll().Where(p => p.ProductId == input.ProductId && p.PriceLevelId == input.PriceLevelId).FirstOrDefault();

            if (val == null)
            {
                await _PriceLevelProductRepository.InsertAsync(quey);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in PriceLevel");
            }
        }
        public async Task UpdatePriceLevelProduct(CreatePriceLevelProductInput input)
        {
            input.TenantId = (int)_session.TenantId;
            var quey = input.MapTo<PriceLevelProduct>();

            var val = _PriceLevelProductRepository
            .GetAll().Where(p => (p.ProductId == input.ProductId && p.PriceLevelId == input.PriceLevelId) && p.Id != input.Id).FirstOrDefault();

            if (val == null)
            {
                await _PriceLevelProductRepository.UpdateAsync(quey);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in PriceLevel");
            }

        }

        public async Task DeletePriceLevelProduct(EntityDto input)
        {
            await _PriceLevelProductRepository.DeleteAsync(input.Id);
        }
    }
}
