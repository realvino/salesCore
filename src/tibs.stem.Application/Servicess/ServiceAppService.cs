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
using tibs.stem.Services;
using tibs.stem.Servicess.Dto;

namespace tibs.stem.Servicess
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_ProductFamily_Services)]
    public class ServiceAppService : stemAppServiceBase, IServiceAppService
    {
        private readonly IRepository<Service> _ServiceRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IAbpSession _session;

        public ServiceAppService(IRepository<Service> ServiceRepository, IUnitOfWorkManager unitOfWorkManager, IAbpSession session)
        {
            _ServiceRepository = ServiceRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _session = session;
        }

        public ListResultDto<ServiceList> GetService(GetServiceInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                var query = _ServiceRepository.GetAll()
                .WhereIf(
                   !input.Filter.IsNullOrWhiteSpace(),
                   u =>
                       u.ServiceCode.Contains(input.Filter) ||
                       u.ServiceName.Contains(input.Filter))
               .ToList();

                return new ListResultDto<ServiceList>(query.MapTo<List<ServiceList>>());
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_ProductFamily_Services_Edit)]
        public async Task<GetService> GetServiceForEdit(NullableIdDto input)
        {
            var output = new GetService
            {
            };

            var service = _ServiceRepository
                .GetAll().Where(p => p.Id == input.Id).FirstOrDefault();
            output.services = service.MapTo<CreateServiceInput>();

            return output;

        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_ProductFamily_Services_Create)]
        public async Task CreateOrUpdateService(CreateServiceInput input)
        {
            if (input.Id != 0)
            {
                await UpdateService(input);
            }
            else
            {
                await CreateService(input);
            }
        }

        public async Task CreateService(CreateServiceInput input)
        {
            input.TenantId = (int)_session.TenantId;
            var service = input.MapTo<Service>();
            var val = _ServiceRepository
             .GetAll().Where(p => p.ServiceCode == input.ServiceCode || p.ServiceName == input.ServiceName).FirstOrDefault();

            if (val == null)
            {
                await _ServiceRepository.InsertAsync(service);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in ServiceCode '" + input.ServiceCode + "' or ServiceName '" + input.ServiceName + "'...");
            }
        }

        public async Task UpdateService(CreateServiceInput input)
        {
            input.TenantId = (int)_session.TenantId;
            var service = input.MapTo<Service>();

            var val = _ServiceRepository
            .GetAll().Where(p => (p.ServiceCode == input.ServiceCode || p.ServiceName == input.ServiceName) && p.Id != input.Id).FirstOrDefault();

            if (val == null)
            {
                await _ServiceRepository.UpdateAsync(service);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in ServiceCode '" + input.ServiceCode + "' or ServiceName '" + input.ServiceName + "'...");
            }

        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_ProductFamily_Services_Delete)]
        public async Task DeleteService(EntityDto input)
        {
            await _ServiceRepository.DeleteAsync(input.Id);
        }


    }
}
