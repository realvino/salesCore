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
using tibs.stem;
using tibs.stem.CustomerCompanys;
using tibs.NewInfoTypes.Dto;
using tibs.stem.NewInfoTypes.Dto;
using Microsoft.EntityFrameworkCore;
using tibs.stem.InfoTypes.Exporting;
using tibs.stem.Dto;
using Abp.Domain.Uow;
using Abp.Authorization;
using tibs.stem.Authorization;

namespace tibs.stem.NewInfoTypes
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_Master_InfoType)]
    public class InfoTypeAppService : stemAppServiceBase, IInfoTypeAppService
    {
        private readonly IRepository<InfoType> _newInfotypeRepository;
        private readonly IInfoTypeExcelExporter _infoTypeExcelExporter;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public InfoTypeAppService(
            IRepository<InfoType> newInfotypeRepository, 
            IInfoTypeExcelExporter infoTypeExcelExporter,
            IUnitOfWorkManager unitOfWorkManager)
        {
            _newInfotypeRepository = newInfotypeRepository;
            _infoTypeExcelExporter = infoTypeExcelExporter;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task<FileDto> GetInfoTypeToExcel()
        {
            var query = _newInfotypeRepository.GetAll();
            var inquiry = (from a in query
                           select new NewInfoTypeListDto
                           {
                               Id = a.Id,
                               ContactName = a.ContactName,
                               Info = (bool)a.Info

                           });

            var NewInfoTypeListDtos = inquiry.MapTo<List<NewInfoTypeListDto>>();

            return _infoTypeExcelExporter.ExportToFile(NewInfoTypeListDtos);
        }

        public async Task<PagedResultDto<NewInfoTypeListDto>> GetNewInfoType(GetNewInfoTypeInput input)
        {
            var query = _newInfotypeRepository.GetAll()
                .WhereIf(
                !input.Filter.IsNullOrEmpty(),
                p => p.ContactName.Contains(input.Filter) ||
                     p.Info.ToString().Contains(input.Filter)
                );
            var inquiry = (from a in query
                           select new NewInfoTypeListDto
                           {
                               Id = a.Id,
                               ContactName = a.ContactName,
                               Info = (bool)a.Info

                           });

            var inquiryCount = inquiry.Count();

            var inquirylist = await inquiry
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            var inquirylistoutput = inquirylist.MapTo<List<NewInfoTypeListDto>>();
            return new PagedResultDto<NewInfoTypeListDto>(
                inquiryCount, inquirylistoutput);
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Master_InfoType_Edit)]
        public async Task<GetNewInfoType> GetNewInfoTypeForEdit(NullableIdDto input)
        {
            var output = new GetNewInfoType();
            var query = _newInfotypeRepository
               .GetAll().Where(p => p.Id == input.Id);
            var inquiry = (from a in query
                           select new NewInfoTypeListDto
                           {
                               Id = a.Id,
                               ContactName = a.ContactName,
                               Info = (bool)a.Info
                           }).FirstOrDefault();


            output = new GetNewInfoType
            {
                NewInfoTypes = inquiry
            };


            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Master_InfoType_Create)]
        public async Task CreateOrUpdateNewInfoType(NewInfoTypeInputDto input)
        {
            if (input.Id != 0)
            {
                await UpdateNewInfoTypeAsync(input);
            }
            else
            {
                await CreateNewInfoTypeAsync(input);
            }
        }

        public virtual async Task CreateNewInfoTypeAsync(NewInfoTypeInputDto input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(AbpSession.TenantId))
            {
                input.TenantId = (int)(AbpSession.TenantId);

                var query = input.MapTo<InfoType>();

                    var val = _newInfotypeRepository
                     .GetAll().Where(p => p.ContactName == input.ContactName).FirstOrDefault();
                    if (val == null)
                    {
                        await _newInfotypeRepository.InsertAsync(query);
                    }
                    else
                    {
                        throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in InfoType '" + input.ContactName + "'...");
                    }
            }

        }

        public virtual async Task UpdateNewInfoTypeAsync(NewInfoTypeInputDto input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(AbpSession.TenantId))
            {
                input.TenantId = (int)(AbpSession.TenantId);

                var val = _newInfotypeRepository
                 .GetAll().Where(p => p.ContactName == input.ContactName && p.Id != input.Id).FirstOrDefault();
                if (val == null)
                {
                    var query = await _newInfotypeRepository.GetAsync(input.Id);
                    ObjectMapper.Map(input, query);
                    await _newInfotypeRepository.UpdateAsync(query);
                }
                else
                {
                    throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in InfoType '" + input.ContactName + "'...");
                }
            }
            
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Master_InfoType_Delete)]
        public async Task GetDeleteNewInfoType(EntityDto input)
        {
            await _newInfotypeRepository.DeleteAsync(input.Id);
        }


    }
}
