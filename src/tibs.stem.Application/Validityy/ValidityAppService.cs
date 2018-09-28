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
using tibs.stem.Authorization;
using tibs.stem.Quotations;
using tibs.stem.Validitys;
using tibs.stem.Validityy.Dto;

namespace tibs.stem.Validityy
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_Validity)]
    public class ValidityAppService : stemAppServiceBase, IValidityAppService
    {
        private readonly IRepository<Validity> _ValidityRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IAbpSession _session;
        private readonly IRepository<Quotation> _QuotationRepository;

 
        public ValidityAppService(IRepository<Validity> ValidityRepository, IUnitOfWorkManager unitOfWorkManager, IAbpSession session, IRepository<Quotation> QuotationRepository)
        {
            _ValidityRepository = ValidityRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _session = session;
            _QuotationRepository = QuotationRepository;
        }

        public ListResultDto<ValidityList> GetValidity(GetValidityInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                var query = _ValidityRepository.GetAll()
                 .WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    u =>
                        u.ValidityCode.Contains(input.Filter) ||
                        u.ValidityName.Contains(input.Filter))
                .ToList();

                return new ListResultDto<ValidityList>(query.MapTo<List<ValidityList>>());
            } 
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_Validity_Edit)]
        public async Task<GetValidity> GetValidityForEdit(NullableIdDto input)
        {
            var output = new GetValidity
            {
            };

            var Validity = _ValidityRepository
                .GetAll().Where(p => p.Id == input.Id).FirstOrDefault();
            output.validity = Validity.MapTo<CreateValidityInput>();

            return output;

        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_Validity_Create)]
        public async Task CreateOrUpdateValidity(CreateValidityInput input)
        {
            if (input.Id != 0)
            {
                await UpdateValidity(input);
            }
            else
            {
                await CreateValidity(input);
            }
        }

        public async Task CreateValidity(CreateValidityInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                input.TenantId = (int)_session.TenantId;
                var Validity = input.MapTo<Validity>();
                var val = _ValidityRepository
                 .GetAll().Where(p => p.ValidityCode == input.ValidityCode || p.ValidityName == input.ValidityName).FirstOrDefault();

                if (val == null)
                {
                    await _ValidityRepository.InsertAsync(Validity);
                }
                else
                {
                    throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in ValidityName '" + input.ValidityName + "' or ValidityCode '" + input.ValidityCode + "'...");
                }
            }
        }

        public async Task UpdateValidity(CreateValidityInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                input.TenantId = (int)_session.TenantId;
                var Validity = input.MapTo<Validity>();

                var val = _ValidityRepository
                .GetAll().Where(p => (p.ValidityCode == input.ValidityCode || p.ValidityName == input.ValidityName) && p.Id != input.Id).FirstOrDefault();

                if (val == null)
                {
                    await _ValidityRepository.UpdateAsync(Validity);
                }
                else
                {
                    throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in ValidityName '" + input.ValidityName + "' or ValidityCode '" + input.ValidityCode + "'...");
                }
            }

        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_Validity_Delete)]
        public async Task GetDeleteValidity(EntityDto input)
        {
            var validity = _ValidityRepository.GetAll().Where(c => c.Id == input.Id);
            var v = (from c in validity
                     join r in _QuotationRepository.GetAll() on c.Id equals r.ValidityId
                     select r).FirstOrDefault();
            if (v == null)
            {
                await _ValidityRepository.DeleteAsync(input.Id);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "validity cannot be deleted '");
            }
        }



    }
}
