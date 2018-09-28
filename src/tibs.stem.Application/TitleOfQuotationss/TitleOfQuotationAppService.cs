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
using tibs.stem.TitleOfQuotations;
using tibs.stem.TitleOfQuotationss.Dto;

namespace tibs.stem.TitleOfQuotationss
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_TitleOfQuotation)]
    public class TitleOfQuotationAppService : stemAppServiceBase, ITitleOfQuotationAppService
    {
        private readonly IRepository<TitleOfQuotation> _titleOfQuotationRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IAbpSession _session;
        private readonly IRepository<Quotation> _QuotationRepository;
 
        public TitleOfQuotationAppService(IRepository<TitleOfQuotation> titleOfQuotationRepository, IUnitOfWorkManager unitOfWorkManager, IAbpSession session, IRepository<Quotation> QuotationRepository)
        {
            _titleOfQuotationRepository = titleOfQuotationRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _session = session;
            _QuotationRepository = QuotationRepository;
        }

        public ListResultDto<TitleOfQuotationList> GetTitleOfQuotation(GetTitleOfQuotationInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                var query = _titleOfQuotationRepository.GetAll()
                 .WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    u =>
                        u.Code.Contains(input.Filter) ||
                        u.Name.Contains(input.Filter))
                .ToList();

                return new ListResultDto<TitleOfQuotationList>(query.MapTo<List<TitleOfQuotationList>>());
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_TitleOfQuotation_Edit)]
        public async Task<GetTitleOfQuotation> GetTitleOfQuotationForEdit(NullableIdDto input)
        {
            var output = new GetTitleOfQuotation
            {
            };

            var title = _titleOfQuotationRepository
                .GetAll().Where(p => p.Id == input.Id).FirstOrDefault();
            output.title = title.MapTo<TitleOfQuotationInput>();

            return output;

        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_TitleOfQuotation_Create)]
        public async Task CreateOrUpdateTitleOfQuotation(TitleOfQuotationInput input)
        {
            if (input.Id != 0)
            {
                await UpdateTitleOfQuotation(input);
            }
            else
            {
                await CreateTitleOfQuotation(input);
            }
        }

        public async Task CreateTitleOfQuotation(TitleOfQuotationInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                input.TenantId = (int)_session.TenantId;
                var title = input.MapTo<TitleOfQuotation>();
                var val = _titleOfQuotationRepository
                 .GetAll().Where(p => p.Code == input.Code || p.Name == input.Name).FirstOrDefault();

                if (val == null)
                {
                    await _titleOfQuotationRepository.InsertAsync(title);
                }
                else
                {
                    throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Code '" + input.Code + "' orName '" + input.Name + "'...");
                }
            }
        }

        public async Task UpdateTitleOfQuotation(TitleOfQuotationInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                input.TenantId = (int)_session.TenantId;
                var title = input.MapTo<TitleOfQuotation>();

                var val = _titleOfQuotationRepository
                .GetAll().Where(p => (p.Code == input.Code || p.Name == input.Name) && p.Id != input.Id).FirstOrDefault();

                if (val == null)
                {
                    await _titleOfQuotationRepository.UpdateAsync(title);
                }
                else
                {
                    throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Code '" + input.Code + "' or Name  '" + input.Name + "'...");
                }
            }

        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_TitleOfQuotation_Delete)]
        public async Task GetDeleteTitleOfQuotation(EntityDto input)
        {
            var title = _titleOfQuotationRepository.GetAll().Where(c => c.Id == input.Id);
            var t = (from c in title
                     join r in _QuotationRepository.GetAll() on c.Id equals r.QuotationTitleId
                     select r).FirstOrDefault();
            if (t == null)
            {
                await _titleOfQuotationRepository.DeleteAsync(input.Id);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "TitleofQuotation cannot be deleted '");
            }
        }
    }
}
