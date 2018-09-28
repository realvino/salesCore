using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Reasons.Dto;
using tibs.stem.Reasonss;

using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using tibs.stem.Quotations;
using Abp.Runtime.Session;
using Abp.Authorization;
using tibs.stem.Authorization;

namespace tibs.stem.Reasons
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_Reason)]
    public class ReasonAppService : stemAppServiceBase, IReasonAppService
    {
        public readonly IRepository<Reason> _ReasonRepository;
        private readonly IRepository<Quotation> _QuotationRepository;
        private readonly IAbpSession _session;

        readonly IUnitOfWorkManager _unitOfWorkManager;
        public ReasonAppService(IAbpSession session, IRepository<Reason> MilestoneStatusListRepository , IUnitOfWorkManager unitOfWorkManager, IRepository<Quotation> QuotationRepository)
        {
            _session = session;
           _ReasonRepository = MilestoneStatusListRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _QuotationRepository = QuotationRepository;
        }

        public async Task<PagedResultDto<ReasonListDto>> GetReason(GetReasonInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                var query = _ReasonRepository.GetAll()
                  .WhereIf(
                  !input.Filter.IsNullOrEmpty(),
                  p => p.Name.Contains(input.Filter) ||
                      p.Code.Contains(input.Filter)
                  );
                var reason = (from a in query
                              select new ReasonListDto
                              {
                                  Id = a.Id,
                                  Name = a.Name,
                                  Code = a.Code
                              });
                var reasonCount = await reason.CountAsync();
                var reasonlist = await reason
                    .OrderBy(input.Sorting)
                    .PageBy(input)
                    .ToListAsync();

                var reasonlistoutput = reasonlist.MapTo<List<ReasonListDto>>();

                return new PagedResultDto<ReasonListDto>(reasonCount, reasonlistoutput);
            }
            
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_Reason_Edit)]
        public async Task<GetReason> GetReasonForEdit(EntityDto input)
        {
            var output = new GetReason();
            try
            {
                if (input.Id > 0)
                {
                    var query = _ReasonRepository.GetAll().Where(p => p.Id == input.Id).FirstOrDefault();

                    output.Reasons = query.MapTo<CreateReasonInput>();

                }
            }
            catch (Exception obj)
            {
                string ff = obj.Message.ToString();
            }
            return output;

        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_Reason_Create)]
        public async Task CreateOrUpdateReason(CreateReasonInput input)
        {
            
                if (input.Id == 0)
                {
                    await CreateReason(input);
                }
                else
                {
                    await UpdateReason(input);
                }
           
        }

        public virtual async Task CreateReason(CreateReasonInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(AbpSession.TenantId))
            {
                input.TenantId = (int)(AbpSession.TenantId);
                var Reason = input.MapTo<Reason>();
                var query = _ReasonRepository.GetAll().Where(p => p.Code == input.Code || p.Name == input.Name).FirstOrDefault();
                if (query == null)
                {
                    await _ReasonRepository.InsertAsync(Reason);
                }
                else
                {
                    throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Reason ...");
                }
            }

        }

        public virtual async Task UpdateReason(CreateReasonInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(AbpSession.TenantId))
            {
                input.TenantId = (int)(AbpSession.TenantId);
                var Reason = input.MapTo<Reason>();
                var query = _ReasonRepository.GetAll()
                    .Where(p => (p.Code == input.Code || p.Name == input.Name) && p.Id != input.Id).FirstOrDefault();
                if (query == null)
                {
                    await _ReasonRepository.UpdateAsync(Reason);
                }
                else
                {
                    throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Reason ...");

                }
            }

        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Quotation_QuotationMaster_Reason_Delete)]
        public async Task GetDeleteReason(EntityDto input)
        {
            var reason = _ReasonRepository.GetAll().Where(c => c.Id == input.Id);
            var w = (from c in reason
                     join r in _QuotationRepository.GetAll() on c.Id equals r.ReasonId
                     select r).FirstOrDefault();
            if (w == null)
            {
                await _ReasonRepository.DeleteAsync(input.Id);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "reason cannot be deleted '");
            }
        }
    }
}
