using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using Abp.UI;
using System.Data;
using tibs.stem.CustomerCompanys;
using tibs.stem.NewCustomerTypes.Dto;
using Microsoft.EntityFrameworkCore;
using tibs.stem.CustomerTypes.Exporting;
using tibs.stem.Dto;
using Abp.Domain.Uow;
using tibs.stem.Authorization;
using Abp.Authorization;

namespace tibs.stem.NewCustomerTypes
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_Master_CustomerType)]
    public class CustomerTypeAppService : stemAppServiceBase, ICustomerTypeAppService
    {
        private readonly IRepository<CustomerType> _newCustomerTypeRepository;
        private readonly ICustomerTypeExcelExporter _customerTypeExcelExporter;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public CustomerTypeAppService(
            IRepository<CustomerType> newCustomerTypeRepository, 
            ICustomerTypeExcelExporter customerTypeExcelExporter,
            IUnitOfWorkManager unitOfWorkManager)
        {
            _newCustomerTypeRepository = newCustomerTypeRepository;
            _customerTypeExcelExporter = customerTypeExcelExporter;
            _unitOfWorkManager = unitOfWorkManager;
        }
        public async Task<FileDto> GetCustomerTypeExcel()
        {

            var query = _newCustomerTypeRepository.GetAll();
            var inquiry = (from a in query
                           select new NewCustomerTypeListDto
                           {
                               Id = a.Id,
                               Title = a.Title,
                               Company = (bool)a.Company

                           });


            var CustomerTypeListDtos = inquiry.MapTo<List<NewCustomerTypeListDto>>();

            return _customerTypeExcelExporter.ExportToFile(CustomerTypeListDtos);
        }

        public async Task<PagedResultDto<NewCustomerTypeListDto>> GetNewCustomerType(GetNewCustomerTypeInput input)
        {
            var query = _newCustomerTypeRepository.GetAll()
                .WhereIf(
                !input.Filter.IsNullOrEmpty(),
                p => p.Title.Contains(input.Filter) ||
                     p.Company.ToString().Contains(input.Filter)
                );
            var inquiry = (from a in query
                           select new NewCustomerTypeListDto
                           {
                               Id = a.Id,
                               Title = a.Title,
                               Company =(bool) a.Company
                              
                           });

            var inquiryCount = inquiry.Count();

            var inquirylist = await inquiry
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            var inquirylistoutput = inquirylist.MapTo<List<NewCustomerTypeListDto>>();
            return new PagedResultDto<NewCustomerTypeListDto>(
                inquiryCount, inquirylistoutput);
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Master_CustomerType_Edit)]
        public async Task<GetNewCustomerType> GetNewCustomerTypeForEdit(NullableIdDto input)
        {
            var output = new GetNewCustomerType();
            var query = _newCustomerTypeRepository
               .GetAll().Where(p => p.Id == input.Id);
            var inquiry = (from a in query
                           select new NewCustomerTypeListDto
                           {
                               Id = a.Id,
                               Title = a.Title,
                               Company =(bool) a.Company
                           }).FirstOrDefault();


            output = new GetNewCustomerType
            {
                NewCustomerTypes = inquiry
            };


            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Master_CustomerType_Create)]
        public async Task CreateOrUpdateNewCustomerType(NewCustomerTypeInputDto input)
        {
            if (input.Id != 0)
            {
                await UpdateCustomerTypeAsync(input);
            }
            else
            {
                await CreateCustomerTypeAsync(input);
            }
        }

        public virtual async Task CreateCustomerTypeAsync(NewCustomerTypeInputDto input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(AbpSession.TenantId))
            {
                input.TenantId = (int)(AbpSession.TenantId);

                var query = input.MapTo<CustomerType>();


                    var val = _newCustomerTypeRepository
                    .GetAll().Where(p => p.Title == input.Title).FirstOrDefault();
                    if (val == null)
                    {
                        await _newCustomerTypeRepository.InsertAsync(query);
                    }
                    else
                    {
                        throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in CustomerType '" + input.Title + "'...");
                    }

            }
        }

        public virtual async Task UpdateCustomerTypeAsync(NewCustomerTypeInputDto input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(AbpSession.TenantId))
            {
                input.TenantId = (int)(AbpSession.TenantId);
                var query = await _newCustomerTypeRepository.GetAsync(input.Id);
                ObjectMapper.Map(input, query);
                var val = _newCustomerTypeRepository
                    .GetAll().Where(p => p.Title == input.Title && p.Id != input.Id).FirstOrDefault();
                if (val == null)
                {
                    await _newCustomerTypeRepository.UpdateAsync(query);
                }
                else
                {
                    throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in CustomerType '" + input.Title + "'...");
                }
            }

        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Master_CustomerType_Delete)]
        public async Task GetDeleteNewCustomerType(EntityDto input)
        {
                await _newCustomerTypeRepository.DeleteAsync(input.Id);
           
        }



    }
}
