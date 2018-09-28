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
using tibs.stem.MileStones;
using tibs.stem.Quotations;
using tibs.stem.QuotationStatuss;
using tibs.stem.QuotationStatusss;
using tibs.stem.QuotationStatusss.Dto;

namespace tibs.sc.QuotationStatusss
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_Quotation_QuotationStatus)]
    public class QuotationStatusAppService : stemAppServiceBase, IQuotationStatusAppService
    {
        private readonly IRepository<QuotationStatus> _QuotationStatusRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IAbpSession _session;
        private readonly IRepository<Quotation> _QuotationRepository;
        private readonly IRepository<MileStone> _MileStoneRepository;



        public QuotationStatusAppService(IRepository<QuotationStatus> QuotationStatusRepository, IRepository<MileStone> MileStoneRepository, IUnitOfWorkManager unitOfWorkManager, IAbpSession session, IRepository<Quotation> QuotationRepository)
        {
            _QuotationStatusRepository = QuotationStatusRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _session = session;
            _QuotationRepository = QuotationRepository;
            _MileStoneRepository = MileStoneRepository;
        }

        public ListResultDto<QuotationStatusList> GetQuotationStatus(GetQuotationStatusInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                var query = _QuotationStatusRepository.GetAll()
                .WhereIf(
                   !input.Filter.IsNullOrWhiteSpace(),
                   u =>
                       u.QuotationStatusCode.Contains(input.Filter) ||
                       u.QuotationStatusName.Contains(input.Filter))
               .ToList();

                return new ListResultDto<QuotationStatusList>(query.MapTo<List<QuotationStatusList>>());
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Quotation_QuotationStatus_Edit)]
        public async Task<GetQuotationStatus> GetQuotationStatusForEdit(NullableIdDto input)
        {
            var output = new GetQuotationStatus
            {
            };

            var QuotationStatus = _QuotationStatusRepository
                .GetAll().Where(p => p.Id == input.Id).FirstOrDefault();
            output.quotationStatus = QuotationStatus.MapTo<CreateQuotationStatusInput>();
            if(QuotationStatus.MileStoneId > 0)
            {
                var data = _MileStoneRepository.GetAll().Where(p => p.Id == QuotationStatus.MileStoneId).FirstOrDefault();
                var mile = new MilestoneData
                {
                    Id = data.Id,
                    Name = data.Name
                };

                output.Milestone = mile;
            }
            return output;

        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Quotation_QuotationStatus_Create)]
        public async Task CreateOrUpdateQuotationStatus(CreateQuotationStatusInput input)
        {
            if (input.Id != 0)
            {
                await UpdateQuotationStatus(input);
            }
            else
            {
                await CreateQuotationStatus(input);
            }
        }


        public async Task CreateQuotationStatus(CreateQuotationStatusInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                input.TenantId = (int)_session.TenantId;
                var QuotationStatus = input.MapTo<QuotationStatus>();

                var val = _QuotationStatusRepository
                 .GetAll().Where(p => p.QuotationStatusCode == input.QuotationStatusCode || p.QuotationStatusName == input.QuotationStatusName).FirstOrDefault();

                if (input.New)
                {
                    val = _QuotationStatusRepository.GetAll().Where(p => p.QuotationStatusCode == input.QuotationStatusCode || p.QuotationStatusName == input.QuotationStatusName || p.New == true).FirstOrDefault();
                }
                else if (input.Submitted)
                {
                    val = _QuotationStatusRepository.GetAll().Where(p => p.QuotationStatusCode == input.QuotationStatusCode || p.QuotationStatusName == input.QuotationStatusName || p.Submitted == true).FirstOrDefault();
                }
                else if (input.Revised)
                {
                    val = _QuotationStatusRepository.GetAll().Where(p => p.QuotationStatusCode == input.QuotationStatusCode || p.QuotationStatusName == input.QuotationStatusName || p.Revised == true).FirstOrDefault();
                }
                else if (input.Won)
                {
                    val = _QuotationStatusRepository.GetAll().Where(p => p.QuotationStatusCode == input.QuotationStatusCode || p.QuotationStatusName == input.QuotationStatusName || p.Won == true).FirstOrDefault();
                }
                else if (input.Lost)
                {
                    val = _QuotationStatusRepository.GetAll().Where(p => p.QuotationStatusCode == input.QuotationStatusCode || p.QuotationStatusName == input.QuotationStatusName || p.Lost == true).FirstOrDefault();
                }

                if (val == null)
                {
                    await _QuotationStatusRepository.InsertAsync(QuotationStatus);
                }
                else
                {
                    throw new UserFriendlyException("Ooops!", "Duplicate Data Occured...");
                }
            }
        }

        public async Task UpdateQuotationStatus(CreateQuotationStatusInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                input.TenantId = (int)_session.TenantId;
                var QuotationStatus = input.MapTo<QuotationStatus>();

                var val = _QuotationStatusRepository
                .GetAll().Where(p => (p.QuotationStatusCode == input.QuotationStatusCode || p.QuotationStatusName == input.QuotationStatusName) && p.Id != input.Id).FirstOrDefault();

                if (input.New)
                {
                    val = _QuotationStatusRepository.GetAll().Where(p => (p.QuotationStatusCode == input.QuotationStatusCode || p.QuotationStatusName == input.QuotationStatusName || p.New == true) && p.Id != input.Id).FirstOrDefault();
                }
                else if (input.Submitted)
                {
                    val = _QuotationStatusRepository.GetAll().Where(p => (p.QuotationStatusCode == input.QuotationStatusCode || p.QuotationStatusName == input.QuotationStatusName || p.Submitted == true) && p.Id != input.Id).FirstOrDefault();
                }
                else if (input.Revised)
                {
                    val = _QuotationStatusRepository.GetAll().Where(p => (p.QuotationStatusCode == input.QuotationStatusCode || p.QuotationStatusName == input.QuotationStatusName || p.Revised == true) && p.Id != input.Id).FirstOrDefault();
                }
                else if (input.Won)
                {
                    val = _QuotationStatusRepository.GetAll().Where(p => (p.QuotationStatusCode == input.QuotationStatusCode || p.QuotationStatusName == input.QuotationStatusName || p.Won == true) && p.Id != input.Id).FirstOrDefault();
                }
                else if (input.Lost)
                {
                    val = _QuotationStatusRepository.GetAll().Where(p => (p.QuotationStatusCode == input.QuotationStatusCode || p.QuotationStatusName == input.QuotationStatusName || p.Lost == true) && p.Id != input.Id).FirstOrDefault();
                }

                if (val == null)
                {
                    await _QuotationStatusRepository.UpdateAsync(QuotationStatus);
                }
                else
                {
                    throw new UserFriendlyException("Ooops!", "Duplicate Data Occured...");
                }
            }

        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Quotation_QuotationStatus_Delete)]
        public async Task GetDeleteQuotationStatus(EntityDto input)
        {
            var Status = _QuotationStatusRepository.GetAll().Where(c => c.Id == input.Id);
            var q = (from c in Status
                     join r in _QuotationRepository.GetAll() on c.Id equals r.StatusId
                     select r).FirstOrDefault();
            if (q == null)
            {
                await _QuotationStatusRepository.DeleteAsync(input.Id);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "QuotationStatus cannot be deleted '");
            }
        }


    }
}
